using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLibrary.DAO;
using DBLibrary.Entities;
using GamesLibrary.Blackjack;
using System.Drawing;

namespace GameMachineWebApp.secure
{
    public partial class BlackjackMain : System.Web.UI.Page
    {
        Blackjack blackjack;
        PlayerDAO playerDAO;
        Deck deck;
        BJPlayer human;
        Dealer dealer;
        int hitCounter = 3;

        /// <summary>
        /// On initial page load only, show default blue cards face down and disable hit/stand buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            playerDAO = new PlayerDAO(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);

            if (!IsPostBack)
            {
                ViewState["hitCounter"] = hitCounter;

                dCard1.ImageUrl = "../images/cards/Card_Blue_Ver.png";
                dCard2.ImageUrl = "../images/cards/Card_Blue_Ver.png";
                pCard1.ImageUrl = "../images/cards/Card_Blue_Ver.png";
                pCard2.ImageUrl = "../images/cards/Card_Blue_Ver.png";
                pPanel.Visible = true;
                dPanel.Visible = true;
                btnHit.Enabled = false;
                btnStand.Enabled = false;
            }
        }

        /// <summary>
        /// Enables hit/stand buttons, shows default blue for dealers 2nd card, deals new hand to both players, and
        /// updates the images associated with the cards dealt. Shows the score.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewGame_Click(object sender, EventArgs e)
        {
            btnHit.Enabled = true;
            btnStand.Enabled = true;
            lblResultMsg.Visible = false;
            pCard3.Visible = false;
            pCard4.Visible = false;
            pCard5.Visible = false;

            dCard2.ImageUrl = "../images/cards/Card_Blue_Ver.png";
            dCard3.Visible = false;
            dCard4.Visible = false;
            dCard5.Visible = false;

            deck = new Deck();
            Session["deck"] = deck;

            dealer = new Dealer("AI", null);
            Session["dealer"] = dealer;

            string playerName = playerDAO.GetNamebyEmail(Session["currentEmail"].ToString());
            int pid = playerDAO.GetIdByEmail(Session["currentEmail"].ToString());

            human = new BJPlayer(pid, playerName, Session["currentEmail"].ToString());
            Session["human"] = human;

            blackjack = new Blackjack(deck, dealer, human);
            Session["game"] = blackjack;

            ViewState["hitCounter"] = hitCounter;

            human.PlayerHand = blackjack.DealNewHand();
            human.PlayerHand.CheckAceAfterHit(human.PlayerHand.CardsInHand.Last());

            dealer.PlayerHand = blackjack.DealNewHand();
            dealer.PlayerHand.CheckAceAfterHit(dealer.PlayerHand.CardsInHand.Last());

            pCard1.ImageUrl = human.PlayerHand.CardsInHand.First().Image;
            pCard2.ImageUrl = human.PlayerHand.CardsInHand.ElementAt(1).Image;

            dCard1.ImageUrl = dealer.PlayerHand.CardsInHand.First().Image;

            pPanel.Visible = true;
            dPanel.Visible = true;

            dScore.Text = dealer.PlayerHand.CardsInHand.First().Number.ToString();
            pScore.Text = human.PlayerHand.HandValue.ToString();
        }

        /// <summary>
        /// Calls Hit method and updates card image. Updates score. Calls CheckWin method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHit_Click(object sender, EventArgs e)
        {
            hitCounter = (int)ViewState["hitCounter"];

            pPanel.Visible = true;
            human = (BJPlayer)Session["human"];
            deck = (Deck)Session["deck"];
            blackjack = (Blackjack)Session["game"];

            if (human.Hit(deck) == 1)
            {
                string imagePath = human.PlayerHand.CardsInHand.Last().Image;

                switch (hitCounter)
                {
                    case 3:
                        pCard3.ImageUrl = imagePath;
                        hitCounter++;
                        pCard3.Visible = true;
                        break;
                    case 4:
                        pCard4.ImageUrl = imagePath;
                        hitCounter++;
                        pCard4.Visible = true;
                        break;
                    case 5:
                        pCard5.ImageUrl = imagePath;
                        hitCounter++;
                        pCard5.Visible = true;
                        break;
                    default:
                        Response.Write("default case hit");
                        break;
                }

                ViewState["hitCounter"] = hitCounter;
            }
            else
            {
                lblResultMsg.Text = "You can't hit anymore!";
                lblResultMsg.Visible = true;
            }
            pScore.Text = human.PlayerHand.HandValue.ToString();

            string message = blackjack.CheckWin(human.PlayerID);

            if (message != null)
            {
                lblResultMsg.Text = message;
                lblResultMsg.Visible = true;
                btnHit.Enabled = false;
                btnStand.Enabled = false;
            }
        }

        /// <summary>
        /// Will trigger the dealer to call their Hit method. Updates score and image(s). Calls CheckWin method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStand_Click(object sender, EventArgs e)
        {
            deck = (Deck)Session["deck"];
            dealer = (Dealer)Session["dealer"];
            blackjack = (Blackjack)Session["game"];
            human = (BJPlayer)Session["human"];
            human.Stand = true;
            human.TurnComplete = true;

            dealer.Hit(deck);
            dScore.Text = dealer.PlayerHand.HandValue.ToString();

            dCard2.ImageUrl = dealer.PlayerHand.CardsInHand.ElementAt(1).Image;

            if (dealer.PlayerHand.CardsInHand.Count > 2)
            {
                dCard3.ImageUrl = dealer.PlayerHand.CardsInHand.ElementAt(2).Image;
                dCard3.Visible = true;
            }

            if (dealer.PlayerHand.CardsInHand.Count > 3)
            {
                dCard4.ImageUrl = dealer.PlayerHand.CardsInHand.ElementAt(3).Image;
                dCard4.Visible = true;
            }

            if (dealer.PlayerHand.CardsInHand.Count > 4)
            {
                dCard5.ImageUrl = dealer.PlayerHand.CardsInHand.ElementAt(4).Image;
                dCard5.Visible = true;
            }

            string message = blackjack.CheckWin(human.PlayerID);
            lblResultMsg.Text = message;
            lblResultMsg.Visible = true;
            btnHit.Enabled = false;
            btnStand.Enabled = false;

        }
    }
}