
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
    // If a string value is null for input, return the DBnull value else return the value
    public static string InputIsValueOrNull(string aValue)
    {
        return !String.IsNullOrEmpty(aValue) ? aValue : DBNull.Value.ToString();
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

        ModifyDataBase_Parameters("UPDATE Request SET ReqResp = @ReqResp where ReqId = @ReqId", _parameters);
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

        ModifyDataBase_Parameters(query, _parameters);
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

        ModifyDataBase_Parameters(query, _parameters);
    }
}