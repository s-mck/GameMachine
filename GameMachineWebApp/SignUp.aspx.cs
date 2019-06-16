using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLibrary.Entities;
using DBLibrary.DAO;
using GamesLibrary;
using System.Web.Security;
using System.Configuration;
using System.Drawing;

namespace GameMachineWebApp
{
    public partial class SignUp : System.Web.UI.Page
    {
        PlayerDAO playerDAO;

        protected void Page_Load(object sender, EventArgs e)
        {
            playerDAO = new PlayerDAO(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
        }

        /// <summary>
        /// Calls DAO to add a new player to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Player player = new Player(txtName.Text, txtEmail.Text);

                playerDAO.AddPlayer(player, txtPass.Text);

                Response.Redirect("~/Login.aspx");
            }
            else
            {
                lblSignupFail.Text = "Please fix all errors before proceeding.";
                lblSignupFail.Font.Size = 18;
                lblSignupFail.Font.Italic = true;
                lblSignupFail.Font.Bold = true;
                lblSignupFail.Visible = true;
            }
        }
    }
}