
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for UrlGeneratorCollection
/// </summary>
public class UrlGeneratorCollection
{
    // List of parameters control
    private List<ParameterControl> _listOfControlObject;
    // Current request
    private HttpRequest _request;

    // Fake hidden label to add the window to it
    private Label _controlWindowHolder;
    // Small object class of parameters
    private class ParameterControl
    {
        public Control _control;
        public string _parameter;

        public ParameterControl(Control aControl, string aParameter)
        {
            this._control = aControl;
            this._parameter = aParameter;
        }

    }

    // Creates a new urlgeneratorcollection. Accepts the modules controls, ajaxmanager and control to position it to (usually null)
    public UrlGeneratorCollection(ControlCollection _controls, RadAjaxManager aManager, Control aControlPos)
    {
        // Creates a list of parameters controls
        _listOfControlObject = new List<ParameterControl>();

        // Creates a button that generates the data url
        RadButton _urlGenerator = new RadButton();
        _urlGenerator.Text = "Get data url";
        _urlGenerator.ID = "STATIC_URLGENERATOR_BUTTON";
        _urlGenerator.Click += FindUrl;                 // Generates url
        _urlGenerator.RenderMode = RenderMode.Lightweight;
        _urlGenerator.Style.Add("float", "right");
        _urlGenerator.Icon.SecondaryIconCssClass = "rbSearch";
        _urlGenerator.Icon.SecondaryIconRight = 5;

        // Create a new ajax setting
        AjaxSetting _buttonCtrl = new AjaxSetting(_urlGenerator.ID);

        // Fake label that the radwindow is added to
        _controlWindowHolder = new Label();
        _controlWindowHolder.ID = "LABEL_WINDOWHOLDER";

        // Add both the radbutton
        AjaxUpdatedControl _ctrlButton = new AjaxUpdatedControl();
        _ctrlButton.ControlID = _urlGenerator.ID;
        _ctrlButton.UpdatePanelRenderMode = UpdatePanelRenderMode.Block;
        // And the label
        AjaxUpdatedControl _ctrl_Label = new AjaxUpdatedControl();
        _ctrl_Label.ControlID = _controlWindowHolder.ID;
        _ctrl_Label.UpdatePanelRenderMode = UpdatePanelRenderMode.Block;
        // To the AjaxSetting
        _buttonCtrl.UpdatedControls.Add(_ctrlButton);
        _buttonCtrl.UpdatedControls.Add(_ctrl_Label);
        // Now add the ajaxsetting to the modules ajaxcontroller
        aManager.AjaxSettings.Add(_buttonCtrl);

        // Find the controls index in the control collection
        int _indexOfGrid = _controls.IndexOf(aControlPos);

        //If the control is found, anchor before it
        if (_indexOfGrid >= 0)
        {
            _controls.AddAt(_indexOfGrid, _controlWindowHolder);
            _controls.AddAt(_indexOfGrid, _urlGenerator);
        }
        else // if not, add to top
        {
            _controls.AddAt(0, _controlWindowHolder);
            _controls.AddAt(0, _urlGenerator);
        }


    }

    // Adds a control and the custom parameter to the List of parameters controls
    public void AddGridControl(Control aControl, string aParameter)
    {
        _listOfControlObject.Add(new ParameterControl(aControl, aParameter));
    }

    // Finds (Generates) the url based on selected components that hold a value
    protected void FindUrl(object sender, EventArgs e)
    {
        // First, a little bit of problem solving
        RadButton _sender = (RadButton)sender;

        // When the radwindow is created it creates an overlay that is supposed to close the radwindow when it is clicked
        // It doesnt
        // Add a custom javascript to page to handle that 
        string _vv_javascript_modal =
        "setTimeout(function () {var _vv_ovarlay = $telerik.getElementByClassName(document, 'TelerikModalOverlay');_vv_ovarlay.onclick = function () {GetRadWindowManager().closeActiveWindow();var scrollAmount = window.pageYOffset;setTimeout(function () { window.scrollTo(0, scrollAmount); }, 10);}}, 510);";

        ScriptManager.RegisterClientScriptBlock(_sender.Page, this.GetType(),
          "_VV_DATA_MODALCLOSE", _vv_javascript_modal, true);


        // Create two different divs, one for url and one for buttons
        HtmlGenericControl _messageObject = new HtmlGenericControl("div");
        _messageObject.Style.Add("background-color", "#bcdaff");
        _messageObject.Style.Add("text-align", "center");
        _messageObject.Style.Add("height", "100%");

        HtmlGenericControl _divText = new HtmlGenericControl("div");
        _divText.Style.Add("padding", "30px 0px");
        _divText.Style.Add("font-weight", "bold");
        _divText.Style.Add("font-size", "large");

        // The textbox that holds the dataurl
        RadTextBox _textBox = new RadTextBox();
        _textBox.Width = 700;
        _textBox.Height = 150;
        _textBox.Columns = 100;
        _textBox.Rows = 5;
        _textBox.CssClass = "js-copytextarea";
        _textBox.RenderMode = RenderMode.Lightweight;
        _textBox.TextMode = InputMode.MultiLine;
        _textBox.Wrap = true;
        _textBox.Font.Bold = true;
        _textBox.Font.Size = 15;

        
        Utilities.DisableTextBox(_textBox);
        // GENERATE URL
        // Is the url secure?
        string _protocol = HttpContext.Current.Request.IsSecureConnection ? "https://" : "http://";

        // Find out the host url
       // PortalSettings portalSettings = PortalController.Instance.GetCurrentPortalSettings();
        string _hostUrl =
            HttpContext.Current.Request.Url.Authority + // Url basic
            HttpContext.Current.Request.ApplicationPath + "/";// + // application path
            //portalSettings.ActiveTab.TabPath.Remove(0, 2).ToLower(); // taburl starts with // - remove it

        // Get all parameters and the value
        string _dataUrl = GetDataUrl();
        // Genereate the data url and add it to textbox
        _textBox.Text = _protocol + _hostUrl + "?" + _dataUrl;

        HtmlGenericControl _divButton = new HtmlGenericControl("div");

        // Create a button to copy the url when clicked
        RadButton _button = new RadButton();
        _button.Width = 200;
        _button.Height = 40;
        _button.Text = "Copy url";
        _button.AutoPostBack = false;
        //_button.RenderMode = RenderMode.Lightweight; // For some reason this button jumps out of the radwindow. No idea why

        // Create a custom cleint side function for that and add it to the button
        string _copyFunction = "function(){var copyTextarea = document.querySelector('.js-copytextarea');copyTextarea.select();try {var successful = document.execCommand('copy');var msg = successful ? 'successful' : 'unsuccessful';console.log('Copying text command was ' + msg);} catch (err) {console.log('Oops, unable to copy');}}";
        _button.OnClientClicked = _copyFunction;

        // Create a button to create a task with the selected data url
        RadButton _button_Task = new RadButton();
        _button_Task.Enabled = true;
        _button_Task.Width = 200;
        _button_Task.Height = 40;
        _button_Task.Text = "Send as task";
        _button_Task.AutoPostBack = false;
        _button_Task.Style.Add("margin-left", "10px");

        // Save the data url as a session to fetch it in the task module (TaskManager.ascx.cs)
        HttpContext.Current.Session["VV_DATA_TASK"] = _textBox.Text;

        // Add a function to open the task manager client side with the DNN popUp setting
        // This function can also be found in sortable navigation
        string _openTaskFunction = "function(){var _hrefLoc = window.location.host;if (_hrefLoc.indexOf('local', 0) == -1) _hrefLoc += '/vinnuvakur';_hrefLoc += '/taskmanager?popUp=true&tabValue=55&VV_DATA=true';;dnnModal.show(window.location.protocol + '//' + _hrefLoc, true, 490, 900, false, '');setTimeout(function () {$('.ui-widget-overlay').click(function () {$(this).remove();$('#iPopUp').remove();});}, 10);}";
        _button_Task.OnClientClicked = _openTaskFunction;

        // Add all controls to divs and such
        _divText.Controls.Add(_textBox);
        _divButton.Controls.Add(_button);
        _divButton.Controls.Add(_button_Task);

        _messageObject.Controls.Add(_divText);
        _messageObject.Controls.Add(_divButton);

        // Create the radwindow on clicked
        RadWindow _windowView = new RadWindow();
        _windowView.ID = "_VV_URLCOLLECTION_WINDOW";
        _windowView.Title = "Data Url";
        //Add new control to RadWindow's Content template
        _windowView.ContentContainer.Controls.Add(_messageObject);
        // Radwindow settings
        _windowView.VisibleOnPageLoad = true;
        _windowView.Modal = true;
        _windowView.Width = 800;
        _windowView.Height = 450;
        _windowView.Shortcuts.Add("Close", "Esc");

        _controlWindowHolder.Controls.Add(_windowView);
    }

    // Genereates the data url and returns the querystring as string
    private string GetDataUrl()
    {
        // Starts with the querystring to let DNN nkow it is sending data
        string _queryUrl = "VV_DATA=true";

        // Get all objects in list
        for (int i = 0; i < _listOfControlObject.Count; i++)
        {
            // For all controls, get the current value
            string _value = GetControlValue(_listOfControlObject[i]._control);
            // If there is a value, add the parameter and the value to the url
            if (_value != null)
                _queryUrl += "&" + _listOfControlObject[i]._parameter + "=" + _value;
        }

        return _queryUrl;
    }

    // Sometimes databinding happens after urlgeneration. This is a helper function that can be used to force-refresh control and the parameter
    public void RefreshParameterControl(string aParameter)
    {
        // Loop through all
        for (int i = 0; i < _listOfControlObject.Count; i++)
        {
            // if it is found
            if (_listOfControlObject[i]._parameter == aParameter)
            {
                // Get the value in the query string
                string _valueFromQuery = _request.QueryString[_listOfControlObject[i]._parameter];
                // Set it
                SetControlObjectByValue(_listOfControlObject[i]._control, _valueFromQuery);
            }
        }
    }

    // Returns the parametersvalue
    public string GetParameter(string aParameter)
    {
        string _returnValue = String.Empty;

        for (int i = 0; i < _listOfControlObject.Count; i++)
        {
            // if parameter is found
            if (_listOfControlObject[i]._parameter == aParameter)
                // get the value from the control the parameter is supposed to hold
                return GetControlValue(_listOfControlObject[i]._control);
        }

        return _returnValue;
    }

    // Gets the value used by the control
    private string GetControlValue(Control aControl)
    {
        string _value = null;

        //Find the control type
        string _type = aControl.GetType().Name;
        if (_type == "RadTextBox")
        {
            RadTextBox _box = (RadTextBox)aControl;
            if (!String.IsNullOrEmpty(_box.Text))
                _value = _box.Text;
        }
        else if (_type == "RadDropDownList")
        {
            RadDropDownList _list = (RadDropDownList)aControl;
            if (_list.SelectedIndex != 0)
                _value = _list.SelectedValue;
        }
        else if (_type == "RadNumericTextBox")
        {
            RadNumericTextBox _numeric = (RadNumericTextBox)aControl;
            if (!String.IsNullOrEmpty(_numeric.Value.ToString()))
                _value = _numeric.Value.ToString();
        }
        else if (_type == "RadSearchBox")
        {
            RadSearchBox _search = (RadSearchBox)aControl;
            if (!String.IsNullOrEmpty(_search.Text))
            {
                _value = _search.Text;
                if (_search.SearchContext.Items.Count != 0)
                    _value += "^" + _search.SearchContext.SelectedIndex;
            }
        }
        //TODO: Multichecklist - what do
        // Comboboxes can be hard. this might need rework in later future
        else if (_type == "RadComboBox")
        {
            RadComboBox _combo = (RadComboBox)aControl;
            // if the combobox includes checkboxes
            if (_combo.CheckBoxes)
            {
                foreach (RadComboBoxItem _item in _combo.Items)
                {
                    // return the value as 1 or 0 ( checked / unchecked )
                    _value += _item.Checked ? "1" : "0";
                }
            }

            else if (!String.IsNullOrEmpty(_combo.SelectedValue))
                _value = _combo.SelectedValue;
        }
        else if (_type == "RadButton")
        {
            RadButton _button = (RadButton)aControl;

            // TODO: Set all checks here
            if (_button.ToggleType == ButtonToggleType.CustomToggle)
            {
                _value = _button.SelectedToggleStateIndex.ToString();
            }

            // For everything else
            else
            {
                _value = _button.Checked ? "1" : "0";
            }


        }
        else if (_type == "RadDatePicker")
        {
            RadDatePicker _date = (RadDatePicker)aControl;
            // Use icelandic dates
            if (_date.SelectedDate != null)
                _value = ((DateTime)_date.SelectedDate).ToString("dd.MM.yyyy");
        }
        else if (_type == "RadTabStrip")
        {
            RadTabStrip _tabstrip = (RadTabStrip)aControl;
            _value = _tabstrip.SelectedIndex.ToString();
        }

        return _value;
    }

    // Set the controls by the value of the parameters
    private void SetControlObjectByValue(Control aControl, string aValue)
    {
        string _type = aControl.GetType().Name;
        if (_type == "RadTextBox")
        {
            RadTextBox _box = (RadTextBox)aControl;
            _box.Text = aValue;
        }
        else if (_type == "RadDropDownList")
        {
            RadDropDownList _list = (RadDropDownList)aControl;
            DropDownListItem _item = _list.FindItemByValue(aValue);
            int _selectedIndex = 0;
            if (_item != null) _selectedIndex = _item.Index;
            _list.SelectedIndex = _selectedIndex;
        }
        else if (_type == "RadNumericTextBox")
        {
            RadNumericTextBox _numeric = (RadNumericTextBox)aControl;
            _numeric.Value = Convert.ToDouble(aValue);
        }
        else if (_type == "RadSearchBox")
        {
            RadSearchBox _search = (RadSearchBox)aControl;
            if (aValue.Contains('^'))
            {
                string[] _splitForContext = aValue.Split('^');
                _search.Text = _splitForContext[0].Trim();
                int _selectedCtx = Convert.ToInt32(_splitForContext[1].Trim());
                if (_selectedCtx != -1) _search.SearchContext.Items[_selectedCtx].Selected = true;
            }
            else _search.Text = aValue;
        }
        else if (_type == "RadComboBox")
        {
            RadComboBox _combo = (RadComboBox)aControl;
            if (_combo.CheckBoxes)
            {
                for (int i = 0; i < _combo.Items.Count; i++)
                {
                    _combo.Items[i].Checked = aValue[i] == '1';
                }
            }
            else
            {
                RadComboBoxItem _item = _combo.FindItemByValue(aValue);
                if (_item != null) _item.Selected = true;
                else _combo.Text = aValue;
            }
        }
        else if (_type == "RadButton")
        {
            RadButton _button = (RadButton)aControl;

            // TODO: Set all checks here
            if (_button.ToggleType == ButtonToggleType.CustomToggle)
            {
                _button.SelectedToggleStateIndex = Convert.ToInt32(aValue);
            }

            // For everything else
            else
            {
                _button.Checked = aValue == "1";

            }

        }
        else if (_type == "RadDatePicker")
        {
            RadDatePicker _date = (RadDatePicker)aControl;
            _date.SelectedDate = DateTime.Parse(aValue);
        }
        else if (_type == "RadTabStrip")
        {
            RadTabStrip _tabstrip = (RadTabStrip)aControl;
            int _index = Convert.ToInt32(aValue);
            _tabstrip.SelectedIndex = _index;
            _tabstrip.MultiPage.SelectedIndex = _index;

        }
    }

    // Used to check (usually in init function of module) if the url contains data (VV_DATA=true)
    // Programmer can decide when to check for dataurl by himself by simply calling _urlCollector.IsSendingData(Request)
    // It returns a true when called and VV_DATA is true, so the user can also add extra steps to the function if he wants to e.g.
    // if(_urlCollector.IsSendingData(Request))
    // { // DoStuff.. }
    public bool IsSendingData(HttpRequest aRequest)
    {
        // Set class request
        _request = aRequest;

        // Check for VV_DATA
        string _dataUrl = _request.QueryString["VV_DATA"] as string;
        // If the value is true
        if (_dataUrl == "true")
        {
            // Loop through all list object
            for (int i = 0; i < _listOfControlObject.Count; i++)
            {
                //Get the value from the query string
                string _valueFromQuery = aRequest.QueryString[_listOfControlObject[i]._parameter];
                // If there is a value then set the control
                if (!String.IsNullOrEmpty(_valueFromQuery))
                    SetControlObjectByValue(_listOfControlObject[i]._control, _valueFromQuery);

            }

            return true;
        }

        return false;
    }

}