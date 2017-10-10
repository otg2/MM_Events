using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using MM_Events.Controls;

namespace MM_Events
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            if(!IsPostBack)
            {

                Utilities.DisableTextBox(EventForm_Name);
                Utilities.DisableTextBox(EventForm_Description);
                Utilities.DisableTextBox(EventForm_From);
                Utilities.DisableTextBox(EventForm_To);

                Utilities.DisableTextBox(EventForm_Comment);
                Utilities.DisableTextBox(EventForm_Budget);
                Utilities.DisableTextBox(EventForm_Guests);
                Utilities.DisableTextBox(EventForm_Type);

                Utilities.DisableTextBox(Task_Feedback);
                Utilities.DisableTextBox(NewEventRequestForm_Feedback);

                Utilities.DisableTextBox(RequestEvent_Name);
                Utilities.DisableTextBox(RequestEvent_Descr);
                Utilities.DisableTextBox(RequestEvent_From);
                Utilities.DisableTextBox(RequestEvent_To);
                Utilities.DisableTextBox(RequestEvent_Comment);
                Utilities.DisableTextBox(RequestEvent_Budget);
                Utilities.DisableTextBox(RequestEvent_Guests);
                Utilities.DisableTextBox(RequestEvent_Type);
                Utilities.DisableTextBox(RequestEvent_EventId);

                Utilities.DisableTextBox(ViewTask_Name);
                Utilities.DisableTextBox(ViewTask_Descr);
                Utilities.DisableTextBox(ViewTask_CreatedInfo);
                Utilities.DisableTextBox(ViewTask_DueDate);
                Utilities.DisableTextBox(ViewTask_Budget);

                Utilities.DisableTextBox(FinanceRequest_Event);
                Utilities.DisableTextBox(FinanceRequest_Name);
                Utilities.DisableTextBox(FinanceRequest_Department);
                Utilities.DisableTextBox(FinanceRequest_Descr);



                Utilities.SetStandardGrid(Radgrid_Events);

                if(User.Identity.IsAuthenticated)
                {
                    AuthDiv.Style.Add("display", "inline");
                    NoAuthDiv.Style.Add("display", "none");

                    InterfaceByUser();

                    //Data_Utilities.ModifyDataBase_Parameters("insert into [Table] ([RoleId], [RoleName]) values (@ROLEID, @ROLENAME)", _parameters);
                }
                else
                {
                    AuthDiv.Style.Add("display", "none");
                    NoAuthDiv.Style.Add("display", "inline");
                }
            }
        }


        private void InterfaceByUser()
        {
            
            // Can User create new client request
            // Refactor here
            List<string[]> _parameters = new List<string[]>();
            _parameters.Add(new string[] { "@UserName", User.Identity.Name.ToUpper() });
            DataTable _table = Data_Utilities.getSQLDataByQuery_Parameters("select userrole from users where Username = @UserName", _parameters);
                    
            string _userRole = _table.Rows[0][0].ToString() ;
            
            // Set the create new event request view
            Div_CreateNewEventRequest.Visible = (_userRole == "SCS" || _userRole == "CS");
            
            // Set the task creation view
            EventForm_Task.Visible = _userRole == "SDM" || _userRole == "PM";
           

            // Populate Tabs list
            List<string[]> _tParameters = new List<string[]>();
            _tParameters.Add(new string[] { "@TaskTeam", _userRole });
            DataTable _taskTable = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Task] where [TaskTeam]=@TaskTeam and [TaskStatus] not in('CLOSED') order by [TaskDueDate] asc", _tParameters);
            if (_taskTable.Rows.Count != 0)
            {
                for (int i = 0; i < _taskTable.Rows.Count; i++)
                {
                    string _status = _taskTable.Rows[i]["TaskStatus"].ToString();

                    RadButton _generatedTask_Button = new RadButton();
                    _generatedTask_Button.RenderMode = RenderMode.Lightweight;
                    _generatedTask_Button.Text = _taskTable.Rows[i]["TaskName"].ToString() + " | "
                        + (DateTime.Parse(_taskTable.Rows[i]["TaskDueDate"].ToString())).ToString("yyyy.MM.dd") + " | " + _status;
                    _generatedTask_Button.Value = _taskTable.Rows[i]["TaskId"].ToString();
                    _generatedTask_Button.AutoPostBack = false;
                    _generatedTask_Button.Width = 350;
                    _generatedTask_Button.Style.Add("margin", "10px 0px");
                    _generatedTask_Button.OnClientClicked = "openTaskForm";
                    _generatedTask_Button.Skin = _status == "IN PROGRESS" ? "Sunset" : "Metro";
                    Div_GeneratedTasks.Controls.Add(_generatedTask_Button);
                }
            }

            // Populate Request list
            List<string[]> _rParameters = new List<string[]>();
            _rParameters.Add(new string[] { "@ReqResp", _userRole });
            DataTable _requestTable = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Request] where [ReqResp]=@ReqResp and [ReqStatus] in('OPEN', 'READY') order by [ReqDate] asc", _rParameters);
            if (_requestTable.Rows.Count != 0)
            {
                for(int i = 0 ; i < _requestTable.Rows.Count; i++)
                {
                    string _reqType = _requestTable.Rows[i]["ReqType"].ToString();
                    string _displayName;
                    if (_reqType == "EVENT" || _reqType == "OUTSOURCE")
                        _displayName = Data_Utilities.getSQLDataByQuery("select EventName from [Events] where EventId=" + _requestTable.Rows[i]["ReqTaskId"].ToString()).Rows[0][0].ToString();
                    else
                        _displayName = Data_Utilities.getSQLDataByQuery("select TaskName from [Task] where TaskId=" + _requestTable.Rows[i]["ReqTaskId"].ToString()).Rows[0][0].ToString();


                    RadButton _generatedRequest_Button = new RadButton();
                    _generatedRequest_Button.RenderMode = RenderMode.Lightweight;
                    _generatedRequest_Button.Text = _displayName + " | " + _requestTable.Rows[i]["ReqType"].ToString() + " | " 
                        + (DateTime.Parse(_requestTable.Rows[i]["ReqDate"].ToString())).ToString("yyyy.MM.dd");
                    _generatedRequest_Button.Value = _requestTable.Rows[i]["ReqId"].ToString(); // var reqtaskid
                    _generatedRequest_Button.AutoPostBack = false;
                    _generatedRequest_Button.Width = 350;
                    _generatedRequest_Button.Style.Add("margin","10px 0px");

                    if (_reqType=="EVENT")
                    // Set different javscripts here
                        _generatedRequest_Button.OnClientClicked = "openRequestForm";
                    else if (_reqType == "FINANCE")
                        // Set different javscripts here
                        _generatedRequest_Button.OnClientClicked = "openRequestForm_Finance";
                    else if (_reqType == "OUTSOURCE")
                        // Set different javscripts here
                        _generatedRequest_Button.OnClientClicked = "openRequestForm_Outsource";

                    Div_GeneratedRequests.Controls.Add(_generatedRequest_Button);

                }
            }
        }

        protected void Radgrid_Events_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid _sender = (RadGrid)sender;

            _sender.DataSource = Data_Utilities.getSQLDataByQuery("select * from [Events] where [EventStatus] in('APPROVED','PENDING_REPORT')");
        }

        protected void Events_AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string[] _arguments = e.Argument.Split(';');
            if(_arguments.Length > 1)
            {
                if (_arguments[0] == "init_Task")
                {

                   // List<string[]> _reqParamenter = new List<string[]>();
                   // _reqParamenter.Add(new string[] { "@reqId", _arguments[1].Trim() });
                   // DataTable _reqInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Requests] where [ReqId]=@ReqId", _reqParamenter);

                    List<string[]> _taskParamenter = new List<string[]>();
                    _taskParamenter.Add(new string[] { "@TaskId", _arguments[1].Trim() });// _reqInfo.Rows[0]["ReqTaskId"].ToString() });
                    DataTable _taskInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Task] where [TaskId]=@TaskId", _taskParamenter);

                    ViewTask_Name.Text = _taskInfo.Rows[0]["TaskName"].ToString();
                    ViewTask_Descr.Text = _taskInfo.Rows[0]["TaskDescr"].ToString();
                    ViewTask_CreatedInfo.Text = _taskInfo.Rows[0]["TaskCreator"].ToString() + " (" + _taskInfo.Rows[0]["TaskDCreated"].ToString() + ")";
                    ViewTask_OpenEventInformation.Text =  "View event" ;
                    ViewTask_OpenEventInformation.Value = _taskInfo.Rows[0]["TaskEventId"].ToString();
                    ViewTask_DueDate.Text = "Do before: " + _taskInfo.Rows[0]["TaskDueDate"].ToString();


                    foreach (GridDataItem item in Radgrid_Events.MasterTableView.Items)
                    {
                        if (item["EventId"].Text == _taskInfo.Rows[0]["TaskEventId"].ToString())
                        {
                            item.Selected = true;
                        }
                    }

                    string _extraBudget = _taskInfo.Rows[0]["TaskExtraBudget"].ToString();
                    double _extraAsDouble = !String.IsNullOrEmpty(_taskInfo.Rows[0]["TaskExtraBudget"].ToString()) ? Convert.ToDouble(_taskInfo.Rows[0]["TaskExtraBudget"].ToString()) : 0;

                    ViewTask_Budget.Text = _taskInfo.Rows[0]["TaskBudget"].ToString();
                    ViewTask_ExtraBudget.Value = _extraAsDouble;
                    ViewTask_ExtraComment.Text = _taskInfo.Rows[0]["TaskExtraComment"].ToString();

                    string _taskStatus = _taskInfo.Rows[0]["TaskStatus"].ToString();
                   

                    if (_taskStatus == "PENDING FINANCIAL REQUEST")
                    {
                        ViewTask_Accept.Text = "Send financial request";
                        ViewTask_Accept.Value = _arguments[1].Trim();
                        ViewTask_Accept.CommandName = "FINANCIAL";
                    }
                    else if (_taskStatus == "IN PROGRESS")
                    {
                        ViewTask_Accept.Text = "Mark task finished";
                        ViewTask_Accept.Value = _arguments[1].Trim();
                        ViewTask_Accept.CommandName = "CLOSE";
                    }
                    else if (_taskStatus == "PENDING")
                    {
                        ViewTask_Accept.Text = "Send task accept";
                        ViewTask_Accept.Value = _arguments[1].Trim();
                        ViewTask_Accept.CommandName = "PENDING";
                    }

                }
                if (_arguments[0] == "init_Request")
                {
                    List<string[]> _eventParamenter = new List<string[]>();
                    _eventParamenter.Add(new string[] { "@EventId", _arguments[1].Trim() });

                    DataTable _eventInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Events] where [EventId]=@EventId", _eventParamenter);
                    DataTable _requestInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Request] where [ReqTaskId]=@EventId and [ReqType]='EVENT'", _eventParamenter);

                    RequestEvent_Name.Text = _eventInfo.Rows[0]["EventName"].ToString();
                    RequestEvent_Descr.Text = _eventInfo.Rows[0]["EventDescr"].ToString();
                    RequestEvent_From.Text = _eventInfo.Rows[0]["EventFrom"].ToString();
                    RequestEvent_To.Text = _eventInfo.Rows[0]["EventTo"].ToString();
                    //RequestEvent_Comment.Text = _eventInfo.Rows[0]["FinancialComment"].ToString();
                    RequestEvent_Guests.Text = _eventInfo.Rows[0]["EventGuests"].ToString();
                    RequestEvent_Type.Text = _eventInfo.Rows[0]["EventType"].ToString();

                    RequestEvent_Budget.Text = _requestInfo.Rows[0]["ReqBudget"].ToString();
                    RequestEvent_EventId.Text = _requestInfo.Rows[0]["ReqId"].ToString();

                    RequestEvent_FM_Budget.Text = _eventInfo.Rows[0]["EventBudget"].ToString();
                    RequestEvent_FM_Comment.Text = _eventInfo.Rows[0]["FinancialComment"].ToString();

                    if(_requestInfo.Rows[0]["ReqResp"].ToString() == "SCS")
                    {
                        RequestEvent_FM_Budget.ReadOnly = true;
                        RequestEvent_FM_Comment.ReadOnly = true;
                    }
                    if(_requestInfo.Rows[0]["ReqResp"].ToString() == "FM")
                    {
                        RequestEvent_Reject.Enabled = false;
                    }
                    if(_requestInfo.Rows[0]["ReqResp"].ToString() == "ADM")
                    {
                        RequestEvent_FM_Budget.ReadOnly = true;
                        RequestEvent_FM_Comment.ReadOnly = true;
                    }
                    // if request is ready, display meeting and hide buttins
                    if(_requestInfo.Rows[0]["ReqStatus"].ToString() == "READY")
                    {
                        RequestEvent_Reject.Visible = false;
                        RequestEvent_Forward.Visible = false;
                        RequestEvent_Meeting.Visible = true;
                    }
                    else
                    {
                        RequestEvent_Reject.Visible = true;
                        RequestEvent_Forward.Visible = true;
                        RequestEvent_Meeting.Visible = false;
                    }
                }
                if (_arguments[0] == "init_RequestFinance")
                {
                    List<string[]> _reqParamenter = new List<string[]>();
                    _reqParamenter.Add(new string[] { "@ReqId", _arguments[1].Trim() });
                    DataTable _requestInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Request] where [ReqId]=@ReqId ", _reqParamenter);

                    List<string[]> _taskParamenter = new List<string[]>();
                    _taskParamenter.Add(new string[] { "@TaskId", _requestInfo.Rows[0]["ReqTaskId"].ToString() });
                    DataTable _taskInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Task] where [TaskId]=@TaskId ", _taskParamenter);
                    
                    List<string[]> _eventParamenter = new List<string[]>();
                    _eventParamenter.Add(new string[] { "@EventId", _taskInfo.Rows[0]["TaskEventId"].ToString() });
                    DataTable _eventInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Events] where [EventId]=@EventId", _eventParamenter);

                    foreach (GridDataItem item in Radgrid_Events.MasterTableView.Items)
                    {
                        if (item["EventId"].Text == _taskInfo.Rows[0]["TaskEventId"].ToString())
                        {
                            item.Selected = true;
                        }
                    }

                    FinanceRequest_Event.Text = _eventInfo.Rows[0]["EventName"].ToString();
                    FinanceRequest_ViewEvent.Value = _taskInfo.Rows[0]["TaskEventId"].ToString();
                    FinanceRequest_Name.Text =  _taskInfo.Rows[0]["TaskName"].ToString();
                    FinanceRequest_Department.Text =  _taskInfo.Rows[0]["TaskStatusMsg"].ToString();
                    FinanceRequest_Descr.Text = _taskInfo.Rows[0]["TaskExtraComment"].ToString() + " | " + _taskInfo.Rows[0]["TaskDescr"].ToString();
                    FinanceRequest_Original.Text = _taskInfo.Rows[0]["TaskBudget"].ToString();
                    FinanceRequest_Extra.Text = _taskInfo.Rows[0]["TaskExtraBudget"].ToString();

                }
                if (_arguments[0] == "init_RequestOutsource")
                {
                    List<string[]> _reqParamenter = new List<string[]>();
                    _reqParamenter.Add(new string[] { "@ReqId", _arguments[1].Trim() });
                    DataTable _requestInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Request] where [ReqId]=@ReqId ", _reqParamenter);

                    //List<string[]> _taskParamenter = new List<string[]>();
                    //_taskParamenter.Add(new string[] { "@TaskId", _requestInfo.Rows[0]["ReqTaskId"].ToString() });
                    //DataTable _taskInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Task] where [TaskId]=@TaskId ", _taskParamenter);
                    
                    List<string[]> _eventParamenter = new List<string[]>();
                    _eventParamenter.Add(new string[] { "@EventId", _requestInfo.Rows[0]["ReqTaskId"].ToString() });
                    DataTable _eventInfo = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Events] where [EventId]=@EventId", _eventParamenter);

                    foreach (GridDataItem item in Radgrid_Events.MasterTableView.Items)
                    {
                        if (item["EventId"].Text == _requestInfo.Rows[0]["ReqTaskId"].ToString())
                        {
                            item.Selected = true;
                        }
                    }

                    OutsourceRequest_Name.Text = "NAME OF REQUEST";// _taskInfo.Rows[0]["TaskName"].ToString();
                    OutsourceRequest_Subteam.SelectedValue = "CHEF";// _taskInfo.Rows[0]["TaskStatusMsg"].ToString();
                    OutsourceRequest_Descr.Text = _requestInfo.Rows[0]["ReqDescr"].ToString();

                    OutsourceRequest_Regarding.Text = _eventInfo.Rows[0]["EventName"].ToString();
                    OutsourceRequest_ViewEvent.Value = _eventInfo.Rows[0]["EventId"].ToString();
                }
            }

            else
            {
                if (e.Argument == "init_Event")
                {
                    GridDataItem _selectedEvent = Radgrid_Events.MasterTableView.GetSelectedItems()[0];

                    EventForm_Name.Text = _selectedEvent["EventName"].Text;
                    EventForm_Description.Text = _selectedEvent["EventDescr"].Text;
                    EventForm_From.Text = _selectedEvent["EventFrom"].Text;
                    EventForm_To.Text = _selectedEvent["EventTo"].Text;

                    EventForm_Comment.Text = _selectedEvent["FinancialComment"].Text;
                    EventForm_Budget.Text = _selectedEvent["EventBudget"].Text;
                    EventForm_Guests.Text = _selectedEvent["EventGuests"].Text;
                    EventForm_Type.Text = _selectedEvent["EventType"].Text;
                }
                if (e.Argument == "init_NewRequest")
                {

                }
            }
        }

        protected void Task_AddTaskToEvent_Click(object sender, EventArgs e)
        {

            //Task_Feedback.Text = (Task_DueDate.SelectedDate.Value).ToString("dd.MM.yyyy"); return;
            GridDataItem _selectedEvent = Radgrid_Events.MasterTableView.GetSelectedItems()[0];
            string _eventId = _selectedEvent["EventId"].Text;

            List<string[]> _parameters = new List<string[]>();
            _parameters.Add(new string[] { "@TaskCreator", User.Identity.Name.ToUpper() });
            _parameters.Add(new string[] { "@TaskTeam", Task_Subteams.SelectedValue });
            _parameters.Add(new string[] { "@TaskStatusMsg", Task_Subteams.SelectedValue });
            _parameters.Add(new string[] { "@TaskEventId", _eventId });
            _parameters.Add(new string[] { "@TaskBudget", Task_Budget.Value.ToString() });
            _parameters.Add(new string[] { "@TaskDueDate",  (Task_DueDate.SelectedDate.Value).ToString("yyyy.MM.dd")  });
            _parameters.Add(new string[] { "@TaskDCreated", (DateTime.Today).ToString("yyyy.MM.dd") });//"GETDATE()" });
            _parameters.Add(new string[] { "@TaskStatus", "PENDING" });
            _parameters.Add(new string[] { "@TaskName", Task_Name.Text });
            _parameters.Add(new string[] { "@TaskDescr", Task_Descr.Text });
            string _createTaskQuery = "insert into [Task] ([TaskCreator], [TaskTeam], [TaskStatusMsg], [TaskEventId], [TaskBudget], [TaskDueDate], [TaskDCreated], [TaskStatus], [TaskName], [TaskDescr]) " +
                " values (@TaskCreator, @TaskTeam, @TaskStatusMsg, @TaskEventId, @TaskBudget, @TaskDueDate, @TaskDCreated, @TaskStatus, @TaskName, @TaskDescr)";


            Data_Utilities.ModifyDataBase_Parameters(_createTaskQuery, _parameters);

            Task_Name.Text = "";
            Task_Descr.Text= "";
            Task_Subteams.ClearSelection();
            Task_DueDate.SelectedDate = null;
            Task_Budget.Value = 0;
           //Task_DueDate.
            Task_Feedback.Text = "Task successfuly created";
        }

        protected void NewEventRequestForm_SendNewRequest_Click(object sender, EventArgs e)
        {
            string _description = "DESCR: " + NewEventRequestForm_Descr.Text + ". EMAIL: " + NewEventRequestForm_Email.Text + ". NUMBER: " + NewEventRequestForm_Number.Text;

            
            List<string[]> _parameters = new List<string[]>();
            _parameters.Add(new string[] { "@EventName", NewEventRequestForm_Name.Text });
            _parameters.Add(new string[] { "@EventDescr", _description });
            _parameters.Add(new string[] { "@EventFrom", (NewEventRequestForm_DateFrom.SelectedDate.Value).ToString("yyyy.MM.dd") });
            
            _parameters.Add(new string[] { "@EventTo",  (NewEventRequestForm_DateTo.SelectedDate.Value).ToString("yyyy.MM.dd")  });
            _parameters.Add(new string[] { "@EventBudget", "0"});
            _parameters.Add(new string[] { "@EventType", NewEventRequestForm_Type.SelectedValue });
            _parameters.Add(new string[] { "@EventGuests", NewEventRequestForm_Guests.Value.ToString() });
            _parameters.Add(new string[] { "@EventStatus", "PENDING" });
            string _createEventQuery = "insert into [Events] ([EventName], [EventDescr], [EventFrom], [EventTo], [EventBudget], [EventType], [EventGuests], [EventStatus]) " +
                " values (@EventName, @EventDescr, @EventFrom, @EventTo, @EventBudget, @EventType, @eventGuests, @EventStatus)";


            Data_Utilities.ModifyDataBase_Parameters(_createEventQuery, _parameters);

            string _getNewEventId = Data_Utilities.getSQLDataByQuery("select max(EventId) from [Events]").Rows[0][0].ToString();

            _parameters.Clear();
            _parameters.Add(new string[] { "@ReqType", "EVENT" });
            _parameters.Add(new string[] { "@ReqResp", "SCS" });
            _parameters.Add(new string[] { "@ReqDescr", _description });
            _parameters.Add(new string[] { "@ReqDate", (DateTime.Today).ToString("yyyy.MM.dd") });
            _parameters.Add(new string[] { "@ReqTaskId", _getNewEventId });
            _parameters.Add(new string[] { "@ReqStatus", "OPEN" });
            _parameters.Add(new string[] { "@ReqBudget", NewEventRequestForm_Budget.Value.ToString() });

            string _createRequestQuery = "insert into [Request] ([ReqType], [ReqResp], [ReqDescr], [ReqDate], [ReqTaskId], [ReqStatus], [ReqBudget]) " +
                " values (@ReqType, @ReqResp, @ReqDescr, @ReqDate, @ReqTaskId, @ReqStatus, @ReqBudget)";

            Data_Utilities.ModifyDataBase_Parameters(_createRequestQuery, _parameters);

            NewEventRequestForm_Feedback.Text = "New event request created. Hope the SCS approves :O";

        }

        protected void RequestEvent_Reject_Click(object sender, EventArgs e)
        {
            InterfaceByUser();
        }

        protected void RequestEvent_Forward_Click(object sender, EventArgs e)
        {
            if(RequestEvent_FM_Budget.ReadOnly)
                EventRequestControl.SubmitRequest(Convert.ToInt32(RequestEvent_EventId.Text));
            else
            {
                EventRequestControl.SubmitRequest(Convert.ToInt32(RequestEvent_EventId.Text),
                    Convert.ToDecimal(RequestEvent_FM_Budget.Value),
                    RequestEvent_FM_Comment.Text);
            }

            InterfaceByUser();
        }

        protected void RequestEvent_BookMeeting_Click(object sender, EventArgs e)
        {
            EventRequestControl.SubmitRequest(Convert.ToInt32(RequestEvent_EventId.Text));
            InterfaceByUser();
            Radgrid_Events.Rebind();
        }

        protected void ViewTask_Accept_Click(object sender, EventArgs e)
        {
            RadButton _sender = (RadButton)sender;
            if (_sender.CommandName == "FINANCIAL")
            {
                List<string[]> _parameters = new List<string[]>();
                _parameters.Add(new string[] { "@ReqType", "FINANCE" });
                _parameters.Add(new string[] { "@ReqResp", "FM" });
                _parameters.Add(new string[] { "@ReqDescr", ""});
                _parameters.Add(new string[] { "@ReqDate", (DateTime.Today).ToString("yyyy.MM.dd") });
                _parameters.Add(new string[] { "@ReqTaskId", _sender.Value });
                _parameters.Add(new string[] { "@ReqStatus", "OPEN" });
                _parameters.Add(new string[] { "@ReqBudget", "" });

                string _createRequestQuery = "insert into [Request] ([ReqType], [ReqResp], [ReqDescr], [ReqDate], [ReqTaskId], [ReqStatus], [ReqBudget]) " +
                    " values (@ReqType, @ReqResp, @ReqDescr, @ReqDate, @ReqTaskId, @ReqStatus, @ReqBudget)";

                Data_Utilities.ModifyDataBase_Parameters(_createRequestQuery, _parameters);

                TaskControl.SubmitTask(Convert.ToInt32(_sender.Value), Convert.ToDecimal(ViewTask_ExtraBudget.Value), ViewTask_ExtraComment.Text);
            }

            if (_sender.CommandName == "CLOSE")
                TaskControl.SubmitTask(Convert.ToInt32(_sender.Value), Convert.ToDecimal(ViewTask_ExtraBudget.Value), ViewTask_ExtraComment.Text);

            if (_sender.CommandName == "PENDING")
                TaskControl.SubmitTask(Convert.ToInt32(_sender.Value), Convert.ToDecimal(ViewTask_ExtraBudget.Value), ViewTask_ExtraComment.Text);
            
            InterfaceByUser();
        }

        protected void FinanceRequest_Click(object sender, EventArgs e)
        {
            RadButton _sender = (RadButton)sender;


            bool _accept = _sender.Value == "true";
        }

        protected void OutsourceRequest_Send_Click(object sender, EventArgs e)
        {

        }



    }
}