
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for Utilities
/// </summary>
public static class Utilities
{
    /* ------ SETTINGS METHODS -------*/
    // Changes a radgrid to a predefined settings
    public static void SetStandardGrid(RadGrid aGrid)
    {
        // Set filtering functions
        aGrid.AllowPaging = true;
        aGrid.PageSize = 25;
        //aGrid.PagerStyle.PageSizeControlType = // TODO : Set this
        aGrid.AllowSorting = true;
        aGrid.AllowFilteringByColumn = false;
        aGrid.MasterTableView.ToolTip = "Double click to open (or select and press enter)";

        // Set grid visualization
        aGrid.SelectedItemStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("Black");
        aGrid.ShowGroupPanel = false;

        // Set Client Settings
        aGrid.ClientSettings.AllowDragToGroup = false;
        aGrid.ClientSettings.AllowColumnsReorder = false;
        aGrid.ClientSettings.EnableRowHoverStyle = true;
        aGrid.ClientSettings.EnableAlternatingItems = false;
        aGrid.ClientSettings.AllowKeyboardNavigation = true;
        aGrid.ClientSettings.Selecting.AllowRowSelect = true;
        aGrid.ClientSettings.Scrolling.AllowScroll = true;
        aGrid.ClientSettings.Scrolling.UseStaticHeaders = true;
        aGrid.ClientSettings.Scrolling.EnableVirtualScrollPaging = false;
        aGrid.ClientSettings.Resizing.AllowColumnResize = true;
    }


    // Sets textbox to readonly and display mode
    public static void DisableTextBox(RadTextBox aBox)
    {
        aBox.ReadOnly = true;
        aBox.ReadOnlyStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("Transparent");
        aBox.ReadOnlyStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
        aBox.Font.Italic = true;
        //aBox.HoveredStyle.st["cursor"] = "pointer";
        aBox.HoveredStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
        aBox.HoveredStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("Transparent");
    }

    // Sets numeric textbox to readonly and display mode
    public static void ReadOnlyStyle_NumbericBox(RadNumericTextBox aNumericBox)
    {
        aNumericBox.ReadOnly = true;
        aNumericBox.ReadOnlyStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("Transparent");
        aNumericBox.ReadOnlyStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
        //aNumericBox.ReadOnlyStyle.HorizontalAlign = HorizontalAlign.Center;
        aNumericBox.Font.Italic = true;

        aNumericBox.HoveredStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
        aNumericBox.HoveredStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("Transparent");
    }

    // TODO : Add more functions and dynamics to the utilities class
    // Not in use
    public static void ResizeGrid(RadGrid aGrid, SqlDataSource aSource)
    {
        int itemCount = (((DataView)aSource.Select(DataSourceSelectArguments.Empty)).Count);
        if (itemCount < 20)
        {
            aGrid.ClientSettings.Scrolling.AllowScroll = false;
            aGrid.Height = 60 + itemCount * 35;
        }
        else
        {
            aGrid.ClientSettings.Scrolling.AllowScroll = true;
            aGrid.Height = 550;
        }
    }

   

    // Returns an array of size aSize full of empty strings
    public static string[] EmptyArray(int aSize)
    {
        string[] _newArray = new string[aSize];
        for (int i = 0; i < aSize; i++) _newArray[i] = String.Empty;
        return _newArray;
    }

    // NOTE: Not in use. Was made to compare query strings but never worked as intended
    public static string ResolveUrl(string relativeUrl)
    {
        if (relativeUrl == null) throw new ArgumentNullException("relativeUrl");

        if (relativeUrl.Length == 0 || relativeUrl[0] == '/' || relativeUrl[0] == '\\')
            return relativeUrl;

        int idxOfScheme = relativeUrl.IndexOf(@"://", StringComparison.Ordinal);
        if (idxOfScheme != -1)
        {
            int idxOfQM = relativeUrl.IndexOf('?');
            if (idxOfQM == -1 || idxOfQM > idxOfScheme) return relativeUrl;
        }

        StringBuilder sbUrl = new StringBuilder();
        sbUrl.Append(HttpRuntime.AppDomainAppVirtualPath);
        if (sbUrl.Length == 0 || sbUrl[sbUrl.Length - 1] != '/') sbUrl.Append('/');

        // found question mark already? query string, do not touch!
        bool foundQM = false;
        bool foundSlash; // the latest char was a slash?
        if (relativeUrl.Length > 1
            && relativeUrl[0] == '~'
            && (relativeUrl[1] == '/' || relativeUrl[1] == '\\'))
        {
            relativeUrl = relativeUrl.Substring(2);
            foundSlash = true;
        }
        else foundSlash = false;
        foreach (char c in relativeUrl)
        {
            if (!foundQM)
            {
                if (c == '?') foundQM = true;
                else
                {
                    if (c == '/' || c == '\\')
                    {
                        if (foundSlash) continue;
                        else
                        {
                            sbUrl.Append('/');
                            foundSlash = true;
                            continue;
                        }
                    }
                    else if (foundSlash) foundSlash = false;
                }
            }
            sbUrl.Append(c);
        }

        return sbUrl.ToString();
    }


    // Returns array of strings as a simple string with AND between values
    public static string GetFilterWithAnds(string[] aStringArray)
    {
        string newFilter = String.Empty;
        for (int j = 0; j < aStringArray.Length; j++)
        {
            if (aStringArray[j].Length != 0)
            {
                if (newFilter.Length == 0) newFilter += aStringArray[j];
                else newFilter += " AND " + aStringArray[j];
            }
        }
        return newFilter;
    }

    // Rebinds a radgrid and highlights the row based on the value to look for and the field
    // Used after adding, updating or deleting from a grid
    public static void HighlightRowByValue_Rebind(RadGrid aGrid, string aUniqueName, string aValue)
    {
        GridDataItemCollection _items = aGrid.MasterTableView.Items;
        aGrid.Rebind();
        for (int i = 0; i < _items.Count; i++)
        {
            GridDataItem _itemToCheck = _items[i];
            if (_itemToCheck[aUniqueName].Text == aValue)
            {
                aGrid.MasterTableView.Items[i].Selected = true;
                break;
            }
        }
    }
    // Check if the email is valid
    // NOTE: Add this to utilities and use in more modules
    public static bool EmailIsValid(string emailaddress)
    {
        if (String.IsNullOrEmpty(emailaddress)) return false;

        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    /* // TODO: Write this function. Currently only used in one module
    public void HighlightRowByMultiValues_Rebind(RadGrid aGrid, string[] aUniqueNames, string[] aValues)
    {
        GridDataItemCollection _items = aGrid.MasterTableView.Items;
        aGrid.Rebind();
        for (int i = 0; i < _items.Count; i++)
        {
            GridDataItem _itemToCheck = _items[i];
            if (_itemToCheck["RULE_ID"].Text == aId)
            {
                aGrid.MasterTableView.Items[i].Selected = true;
                break;
            }
        }
    */

    /* ------ DEBUGGING METHODS -------*/
    // Returns array of strings as a simple string with and open space between values
    public static string getArrayAsString(string[] anArray)
    {
        string aHolder = String.Empty;
        for (int i = 0; i < anArray.Length; i++)
        {
            aHolder += anArray[i] + " ";
        }
        return aHolder;
    }

    // Returns array of strings as a simple string with comma between values
    public static string concattedArrayWithComma(string[] anArray)
    {
        string aHolder = String.Empty;
        for (int i = 0; i < anArray.Length; i++)
        {
            aHolder += anArray[i];
            if (i < anArray.Length - 1) aHolder += ", ";
        }
        return aHolder;
    }
}