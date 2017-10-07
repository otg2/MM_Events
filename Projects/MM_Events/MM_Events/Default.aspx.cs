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
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("se-SE");
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
            DataTable _taskTable = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Task] where [TaskTeam]=@TaskTeam order by [TaskDueDate] asc", _tParameters);
            if (_taskTable.Rows.Count != 0)
            {
                for (int i = 0; i < _taskTable.Rows.Count; i++)
                {
                    RadButton _generatedTask_Button = new RadButton();
                    _generatedTask_Button.RenderMode = RenderMode.Lightweight;
                    _generatedTask_Button.Text = _taskTable.Rows[i]["TaskName"].ToString() + " | " + _taskTable.Rows[i]["TaskDueDate"].ToString();
                    _generatedTask_Button.Value = _taskTable.Rows[i]["TaskId"].ToString();
                    _generatedTask_Button.AutoPostBack = false;
                    _generatedTask_Button.OnClientClicked = "openTaskForm";
                    Div_GeneratedTasks.Controls.Add(_generatedTask_Button);
                }
            }

            // Populate Request list
            List<string[]> _rParameters = new List<string[]>();
            _rParameters.Add(new string[] { "@ReqResp", _userRole });
            DataTable _requestTable = Data_Utilities.getSQLDataByQuery_Parameters("select * from [Request] where [ReqResp]=@ReqResp and [ReqStatus]='OPEN' order by [ReqDate] asc", _rParameters);
            if (_requestTable.Rows.Count != 0)
            {
                for(int i = 0 ; i < _requestTable.Rows.Count; i++)
                {
                    RadButton _generatedRequest_Button = new RadButton();
                    _generatedRequest_Button.RenderMode = RenderMode.Lightweight;
                    _generatedRequest_Button.Text = _requestTable.Rows[i]["ReqType"].ToString() + " | " + _requestTable.Rows[i]["ReqDate"].ToString();
                    _generatedRequest_Button.Value = _requestTable.Rows[i]["ReqTaskId"].ToString();
                    _generatedRequest_Button.AutoPostBack = false;
                    _generatedRequest_Button.OnClientClicked = "openRequestForm";
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
                    ViewTask_Name.Text = "hneta" ;
                    ViewTask_Descr.Text = "pizza";
                    ViewTask_CreatedInfo.Text = "ottar date bla";
                    ViewTask_OpenEventInformation.Text =  "test";
                    ViewTask_Budget.Text = "5000";
                    ViewTask_ExtraBudget.Value = 0;
                    ViewTask_ExtraComment.Text = "";
                    ViewTask_Accept.Value = "-1";

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
                    RequestEvent_Comment.Text = _eventInfo.Rows[0]["FinancialComment"].ToString();
                    RequestEvent_Budget.Text = _eventInfo.Rows[0]["EventBudget"].ToString();
                    RequestEvent_Guests.Text = _eventInfo.Rows[0]["EventGuests"].ToString();
                    RequestEvent_Type.Text = _eventInfo.Rows[0]["EventType"].ToString();

                    RequestEvent_EventId.Text = _requestInfo.Rows[0]["ReqId"].ToString();

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

                    EventForm_Comment.Text = _selectedEvent["EventComment"].Text;
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
            _parameters.Add(new string[] { "@TaskEventId", _eventId });
            _parameters.Add(new string[] { "@TaskBudget", Task_Budget.Value.ToString() });
            _parameters.Add(new string[] { "@TaskDueDate",  (Task_DueDate.SelectedDate.Value).ToString("yyyy.MM.dd")  });
            _parameters.Add(new string[] { "@TaskDCreated", (DateTime.Today).ToString("yyyy.MM.dd") });//"GETDATE()" });
            _parameters.Add(new string[] { "@TaskStatus", "NEWTASK" });
            _parameters.Add(new string[] { "@TaskName", Task_Name.Text });
            _parameters.Add(new string[] { "@TaskDescr", Task_Descr.Text });
            string _createTaskQuery = "insert into [Task] ([TaskCreator], [TaskTeam], [TaskEventId], [TaskBudget], [TaskDueDate], [TaskDCreated], [TaskStatus], [TaskName], [TaskDescr]) " +
                " values (@TaskCreator, @TaskTeam, @TaskEventId, @TaskBudget, @TaskDueDate, @TaskDCreated, @TaskStatus, @TaskName, @TaskDescr)";


            Data_Utilities.ModifyDataBase_Parameters(_createTaskQuery, _parameters);

            Task_Name.Text = "";
            Task_Descr.Text= "";
            Task_Subteams.SelectedIndex = 0;
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
            _parameters.Add(new string[] { "@EventBudget", NewEventRequestForm_Budget.Value.ToString()});
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
            string _createRequestQuery = "insert into [Request] ([ReqType], [ReqResp], [ReqDescr], [ReqDate], [ReqTaskId], [ReqStatus]) " +
                " values (@ReqType, @ReqResp, @ReqDescr, @ReqDate, @ReqTaskId, @ReqStatus)";

            Data_Utilities.ModifyDataBase_Parameters(_createRequestQuery, _parameters);

            NewEventRequestForm_Feedback.Text = "New event request created. Hope the SCS approves :O";

        }

        protected void RequestEvent_Reject_Click(object sender, EventArgs e)
        {
            InterfaceByUser();
        }

        protected void RequestEvent_Forward_Click(object sender, EventArgs e)
        {
            Event_Request_Control.SubmitRequest(Convert.ToInt32(RequestEvent_EventId.Text));

            InterfaceByUser();
        }



    }
}