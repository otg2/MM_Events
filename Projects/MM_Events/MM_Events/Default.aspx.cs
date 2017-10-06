using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace MM_Events
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Utilities.SetStandardGrid(Radgrid_Events);

                if(User.Identity.IsAuthenticated)
                {
                    debug.Text = User.Identity.Name.ToUpper();
                   // "select * from userinfo where username =" + ;
                    List<string[]> _parameters = new List<string[]>();
                    _parameters.Add(new String[] { "@ROLEID", "99" });
                    _parameters.Add(new String[] { "@ROLENAME", "gurk" });


                    //Data_Utilities.ModifyDataBase_Parameters("insert into [Table] ([RoleId], [RoleName]) values (@ROLEID, @ROLENAME)", _parameters);
                }
            }
        }

        protected void Radgrid_Events_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid _sender = (RadGrid)sender;

            _sender.DataSource = Data_Utilities.getSQLDataByQuery("select * from [Events]");
        }

        private DataTable createDataTable()
        {
            DataTable _dataTable = new DataTable();

            /*for(int i = 0 ; i < 45; i++)
            {
                //_dataTable.Rows.Add(generateRandomRow());
                DataRow _dataRow = _dataTable.NewRow();

                _dataRow["EventId"] = generateRandom_EventId();
                _dataRow["EventName"] = "test";// generateRandom_EventName();
                _dataRow["EventDesc"] = "test";// generateRandom_EventDesc();

            }
            */
            return _dataTable;
        } 
        /*
        public DataRow generateRandomRow()
        {
            ////DataRow _dataRow = new DataRow();

           // _dataRow["EventId"] = generateRandom_EventId();
           // _dataRow["EventName"] = "test";// generateRandom_EventName();
           // _dataRow["EventDesc"] = "test";// generateRandom_EventDesc();
        }*/

        private string generateRandom_EventId()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 200);
            return randomNumber.ToString();
        }

        private string generateRandom_EventName()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, _listOfEvents.Length);
            return _listOfEvents[randomNumber];
        }

        private string generateRandom_EventDesc()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, _listOfDesc.Length);
            return _listOfDesc[randomNumber];
        }

        string[] _listOfEvents = new string[] {  };
        string[] _listOfDesc = new string[] { };

        protected void RadButton_AddAction_Click(object sender, EventArgs e)
        {
            debug.Text = "blabla";
            LookForRoles.Items.Add(new DropDownListItem("bla", "gg"));
        }



    }
}