using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLibrary.Entities;
using DBLibrary.DAO;
using System.Configuration;
using System.Web.Security;
using System.Drawing;

namespace GameMachineWebApp
{
    public partial class Login : System.Web.UI.Page
    {
        PlayerDAO playerDAO;

        protected void Page_Load(object sender, EventArgs e)
        {
            playerDAO = new PlayerDAO(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
            
        }

        /// <summary>
        /// Calls DAO method to authenticate the player. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool passMatches = playerDAO.AuthenticatePlayer(txtLoginEmail.Text, txtLoginPass.Text);

            if (passMatches)
            {
                Session["currentEmail"] = txtLoginEmail.Text;
                FormsAuthentication.RedirectFromLoginPage(Session["currentEmail"].ToString(), false);
            }
            else
            {
                lblAuthFail.Text = "Authentication failed. Please try again.";
                lblAuthFail.Font.Size = 18;
                lblAuthFail.Font.Italic = true;
                lblAuthFail.Font.Bold = true;
                lblAuthFail.Visible = true;
            }


        }
    }
}