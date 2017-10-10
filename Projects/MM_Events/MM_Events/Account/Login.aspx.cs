using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using MM_Events.Models;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace MM_Events.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Password.Text = "hallo1";

            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            Login_Name(UserName.Text);
        }

        protected void LogIn_Extra(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Login_Name(btn.Text.Split('-')[0].Trim());
        }

        private void Login_Name(string aName)
        {
            if (true)
            {
                // Validate the user password
                var manager = new UserManager();
                ApplicationUser user = manager.Find(aName, "hallo1"); // Password.Text
                if (user != null)
                {
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
        }

        protected void Login_Telerik_Click(object sender, EventArgs e)
        {
            RadButton _sender = (RadButton) sender;
            Login_Name(_sender.Value);
        }
    }
}