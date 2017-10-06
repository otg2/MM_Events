using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace MM_Events
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                debug.Text = User.Identity.Name.ToString() + "_" + User.Identity.ToString() + "_" + User.Identity.AuthenticationType.ToString();
                //BindUsersToUserList();
                //BindRolesToList();
                
            }
        }

        private void BindUsersToUserList()
        {
            // Get all of the user accounts 
            MembershipUserCollection users = Membership.GetAllUsers();
            LookForUsers.DataSource = users;
            LookForUsers.DataBind();
        }

        private void BindRolesToList()
        {
            // Get all of the roles 
            string[] roles = Roles.GetAllRoles();
            LookForRoles.DataSource = roles;
            LookForRoles.DataBind();
        }

    }
}