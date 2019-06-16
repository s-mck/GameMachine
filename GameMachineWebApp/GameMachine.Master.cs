using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GameMachineWebApp
{
    public partial class GameMachine : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Displays a welcome message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LblWelcome.Text = "Welcome, " + Context.User.Identity.Name;
                LBLogin.Visible = false;
                LBLogout.Visible = true;
                lblQuit.Text = "Return ";
                LBHome.Visible = true;
            }
            else
            {
                LblWelcome.Text = "Welcome, Guest";
                LBLogin.Visible = true;
                LBLogout.Visible = false;
                LBHome.Visible = false;
            }
        }

        /// <summary>
        /// Signs the current user out, abandons the session, and redirects them to the login page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LBLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            Session.Abandon();
        }

        /// <summary>
        /// Redirects user to the home page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LBHome_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectFromLoginPage(Context.User.Identity.Name, false);
        }
    }
}