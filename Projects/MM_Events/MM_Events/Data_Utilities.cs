
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for Data_Utilities
/// </summary>
public static class Data_Utilities
{
    // Set a dropdownlist data as users in a requested role
    public static void DropDown_SetUsersInRole(RadDropDownList aDropDown, string aRole, string aDataValueField)
    {
        DataTable _dataView = new DataTable();
        // Query
        string _query = "select displayname as FullName, a.username as UserName, a.UserID from dbo.dnn_Users a join dbo.dnn_UserRoles b on a.UserID=b.UserID ";
        _query += "where b.RoleId = (select roleid from dbo.dnn_roles where rolename='" + aRole + "') order by FullName";

        ConnectionStringSettings _connString = ConfigurationManager.ConnectionStrings["SiteSqlServer"];
        using (SqlConnection con = new SqlConnection(_connString.ToString()))
        {
            con.Open();
            SqlCommand _command = new SqlCommand();
            _command.Connection = con;
            _command.CommandText = _query;
            _command.ExecuteNonQuery();

            SqlDataAdapter sda = new SqlDataAdapter(_command);
            sda.Fill(_dataView);
        }
        // Set the source and bind it
        aDropDown.DataSource = _dataView;
        aDropDown.DataTextField = "FullName";
        // Sometimes the username is a value and sometime the id. Let programmer decide
        aDropDown.DataValueField = aDataValueField;
        aDropDown.DataBind();

        // Add a default selection item
        aDropDown.Items.Insert(0, new DropDownListItem("Select User / None", "-1"));
    }

    // Sets a combobox as all users registred and are not deleted
    public static void ComboBox_GetAllUsers(RadComboBox aComboBox)
    {
        DataTable _dataView = new DataTable();

        string _query = "select UserDisplay, UserName from [Users] where UserRole in('AUDIO','CHEF','CATERING','DECORATION','GRAPHIC_DESIGNER','IT', "
        + " 'PHOTOGRAPHER') order by userrole";


        ConnectionStringSettings _connString = ConfigurationManager.ConnectionStrings["SiteSqlServer"];
        using (SqlConnection con = new SqlConnection(_connString.ToString()))
        {
            con.Open();
            SqlCommand _command = new SqlCommand();
            _command.Connection = con;
            _command.CommandText = _query;
            _command.ExecuteNonQuery();

            SqlDataAdapter sda = new SqlDataAdapter(_command);
            sda.Fill(_dataView);
        }
        // Set datasource and bind it
        aComboBox.DataSource = _dataView;
        aComboBox.DataTextField = "UserDisplay";
        aComboBox.DataValueField = "UserName";
        aComboBox.DataBind();
    }

    
   

    // If a string value is null for input, return the DBnull value else return the value
    public static string InputIsValueOrNull(string aValue)
    {
        return !String.IsNullOrEmpty(aValue) ? aValue : DBNull.Value.ToString();
    }

    // Set a raddropdownlist as companies the user has access to
    public static void SetRadDropDownListAsUserComp(RadDropDownList aDropDownList, string aUserName, string aDefaultText, bool aReadOnly)
    {
        string _query = "select (CODE || ' - ' || NAME) name, CODE from iss_rpt_coda_company where aduser=:ADUSERNAME ";
        // Add the readonly parameter to query
        if (aReadOnly) _query += " and rdonly=0";
        _query += " order by code asc";

        List<String[]> _parameters = new List<string[]>();
        _parameters.Add(new string[] { ":ADUSERNAME", aUserName.ToUpper() });
        DataTable _view = null;// Data_Utilities.getData_OracleQuery_Parameters(_query, "CodaWorldConnectionString", _parameters);

        // Databind the control
        aDropDownList.DataSource = _view;
        aDropDownList.DataTextField = "NAME";
        aDropDownList.DataValueField = "CODE";
        aDropDownList.DataBind();

        // Add a default text to selection object of desired
        string _defaultText = aDefaultText != null ? aDefaultText : "";
        aDropDownList.Items.Insert(0, new DropDownListItem(_defaultText, ""));

    }

    // Returns a Radgrid mastertableview as a DataTable 
    public static DataTable GetGridInfoAsDataTable(RadGrid aRadGrid)
    {
        // Create empty data table
        DataTable dtRecords = new DataTable();
        foreach (GridColumn col in aRadGrid.Columns) // loopts throuch each columns in the radgrid
        {
            DataColumn colString = new DataColumn(col.UniqueName);
            dtRecords.Columns.Add(colString);

        }
        foreach (GridDataItem row in aRadGrid.Items) // loops through each rows in RadGrid
        {
            DataRow dr = dtRecords.NewRow();
            foreach (GridColumn col in aRadGrid.Columns) //loops through each column in RadGrid
                dr[col.UniqueName] = row[col.UniqueName].Text;
            dtRecords.Rows.Add(dr);
        }
        return dtRecords;
    }

    // NOT WORKING
    public static int GetColumnIndexByName(GridView grid, string name)
    {
        for (int i = 0; i < grid.Columns.Count; i++)
        {
            if (grid.Columns[i].HeaderText.ToLower().Trim() == name.ToLower().Trim())
            {
                return i;
            }
        }

        return -1;
    }

    // We got similar functions in utilities
    //Concate multiple strings with a . in between each one
    public static string CombineElements(string[] anArray)
    {
        string _newAcc = String.Empty;
        for (int j = 0; j < anArray.Length; j++)
        {
            _newAcc += anArray[j];
            if (j != anArray.Length - 1) _newAcc += ".";
        }
        return _newAcc;
    }

    // Returns a gridview based on query and connection string
    


    // Same as above, but works for SQL databases
    public static DataTable getSQLDataByQuery(string aQuery)
    {
        DataTable _dataView = new DataTable();
        ConnectionStringSettings _connString = ConfigurationManager.ConnectionStrings["SqlDataBase"];
        using (SqlConnection con = new SqlConnection(_connString.ToString()))
        {
            con.Open();
            SqlCommand _command = new SqlCommand();
            _command.Connection = con;
            _command.CommandText = aQuery;
            _command.ExecuteNonQuery();

            SqlDataAdapter sda = new SqlDataAdapter(_command);
            sda.Fill(_dataView);
            con.Close();
        }
        return _dataView;
    }

    // Same as above, but works for SQL databases
    public static DataTable getSQLDataByQuery_Parameters(string aQuery, List<String[]> aParameters)
    {
        DataTable _dataView = new DataTable();
        ConnectionStringSettings _connString = ConfigurationManager.ConnectionStrings["SqlDataBase"];
        using (SqlConnection con = new SqlConnection(_connString.ToString()))
        {
            con.Open();
            SqlCommand _command = new SqlCommand();
            _command.Connection = con;
            _command.CommandText = aQuery;
            for (int i = 0; i < aParameters.Count; i++) _command.Parameters.AddWithValue(aParameters[i][0], aParameters[i][1]);

            SqlDataAdapter sda = new SqlDataAdapter(_command);
            sda.Fill(_dataView);
            con.Close();
        }
        return _dataView;
    }

    public static void ModifyDataBase_Parameters(string aQuery, List<String[]> aParameters)
    {
        ConnectionStringSettings _connString = ConfigurationManager.ConnectionStrings["SqlDataBase"];
        using (SqlConnection con = new SqlConnection(_connString.ToString()))
        {
            con.Open();
            SqlCommand _command = new SqlCommand();
            _command.Connection = con;

            // Set values
            for (int i = 0; i < aParameters.Count; i++) _command.Parameters.AddWithValue(aParameters[i][0], aParameters[i][1]);

            _command.CommandText = aQuery;
            _command.ExecuteNonQuery();


            }
    }

    // Returns a gridview of users that have access to a selected tab
    public static DataTable getUsersWithAccessToTab(int aTabId)
    {
        DataTable _dataView = new DataTable();
        ConnectionStringSettings _connString = ConfigurationManager.ConnectionStrings["SiteSqlServer"];
        using (SqlConnection con = new SqlConnection(_connString.ToString()))
        {
            con.Open();
            SqlCommand _command = new SqlCommand();

            string _query = "select distinct ur.userid, u.displayname, upper(u.username) adname from dbo.dnn_userroles ur ";
            _query += "inner join dbo.dnn_users u on u.userid=ur.userid ";
            _query += "where ur.roleid in ";
            _query += "( ";
            _query += "select tp.roleid from dbo.dnn_tabpermission tp  ";
            _query += "inner join dbo.dnn_roles r on r.roleid=tp.roleid  ";
            _query += "where tp.tabid=@pTabId and r.roleid != 0 ";
            _query += ") ";
            _query += "or u.userid in ";
            _query += "( ";
            _query += "select tp.userid from dbo.dnn_tabpermission tp ";
            _query += "where tp.tabid=@pTabId  ";
            _query += ") ";
            _query += "order by u.displayname ";

            _command.Connection = con;
            _command.CommandText = _query;
            _command.Parameters.Add("@pTabId", SqlDbType.Int);
            _command.Parameters["@pTabId"].Value = aTabId;
            _command.ExecuteNonQuery();

            SqlDataAdapter sda = new SqlDataAdapter(_command);
            sda.Fill(_dataView);
            con.Close();
        }
        return _dataView;
    }


    // Used to modify the body content of emails.
    // Turns all image snapshots to alternitive view, so they can be sent in email
    // If the image is c/p from computer (which the control cannot handle) a "error" image is used

    //Used to fetch a task for a given id
    public static DataRow getRequest(int id)
    {
        var _parameters = new List<string[]>
        {
            new string[] { "@ReqId", id.ToString()}
        };

        DataTable _table = getSQLDataByQuery_Parameters("select * from Request where ReqId = @ReqId", _parameters);

        return _table.Rows[0];
    }

    public static void setResponsibleForRequest(int requestId, string sendTo)
    {
        var _parameters = new List<string[]>
        {
            new string[] { "@ReqId", requestId.ToString() },
            new string[] { "@ReqResp", sendTo}
        };

        DataTable _table = getSQLDataByQuery_Parameters("UPDATE Request SET ReqResp = @ReqResp where ReqId = @ReqId", _parameters);
    }

    public static void setEventStatusToAccepted(int requestId)
    {
        var _parameters = new List<string[]>
        {
            new string[] { "@ReqId", requestId.ToString() }
        };

        var query = "UPDATE Events";
        query += "SET EventStatus = 'APPROVED'";
        query += "WHERE EventId = (SELECT ReqTaskId";
        query += "FROM Request";
        query += "WHERE ReqId = @ReqId)";

        DataTable _table = getSQLDataByQuery_Parameters(query, _parameters);
    }

    public static void setEventRequestToClosed(int requestId)
    {
        var _parameters = new List<string[]>
        {
            new string[] { "@ReqId", requestId.ToString() }
        };

        var query = "UPDATE Request";
        query += "SET ReqStatus = 'CLOSED'";
        query += "WHERE ReqId = @ReqId";

        DataTable _table = getSQLDataByQuery_Parameters(query, _parameters);
    }
}