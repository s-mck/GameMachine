using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLibrary.DAO;
using DBLibrary.Entities;

namespace GameMachineWebApp.secure
{
    public partial class Home : System.Web.UI.Page
    {
        PlayerDAO playerDAO;
        ResultDAO resultDAO;

        /// <summary>
        /// Uses DAO to retrieve current users ID and email, then uses those to retrieve a list of results
        /// for that user. Displays the user's name and number of wins for each game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            playerDAO = new PlayerDAO(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
            resultDAO = new ResultDAO(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);

            string playerName = playerDAO.GetNamebyEmail(Context.User.Identity.Name);
            lblWelcomeMsg.Text = $"Welcome back, {playerName}!";

            int playerID = playerDAO.GetIdByEmail(Context.User.Identity.Name);
            List<Result> allResults = resultDAO.GetAllResultsByID(playerID);

            int bCount = 0, cCount = 0, allCount = 0;

            foreach(Result r in allResults)
            {
                if (r.HumanWin == 1)
                {
                    allCount += 1;

                    if (r.GameID == "BLA")
                        bCount += 1;
                    else if (r.GameID == "CON")
                        cCount += 1;
                }
            }
            lblBWins.Text = $"Blackjack Wins: {bCount}";
            lblCWins.Text = $"Connect Four Wins: {cCount}";
            lblAllWins.Text = $"You have {allCount} total wins!";
        }

        /// <summary>
        /// Redirects to Blackjack page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPlayBLA_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/secure/BlackjackMain.aspx");
        }

        /// <summary>
        /// Redirects to ConnectFour page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPlayCON_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/secure/ConnectFourMain.aspx");
        }
    }
}