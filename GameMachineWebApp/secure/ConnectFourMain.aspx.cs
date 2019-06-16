using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GamesLibrary.ConnectFour;
using DBLibrary.DAO;
using DBLibrary.Entities;
using System.Configuration;

namespace GameMachineWebApp.secure
{
    public partial class ConnectFourMain : System.Web.UI.Page
    {
        GameBoard board = null;
        ConnectPlayer player = null;
        ConnectBot bot = null;
        ConnectFour game = null;
        PlayerDAO pdao;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                board = new GameBoard();
                pdao = new PlayerDAO(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
                string playerName = pdao.GetNamebyEmail(Session["currentEmail"].ToString());
                int pid = pdao.GetIdByEmail(Session["currentEmail"].ToString());
                player = new ConnectPlayer(pid, playerName, Session["currentEmail"].ToString());
                bot = new ConnectBot(null, null);
                game = new ConnectFour(bot, player, board);
                tblBoard.GridLines = GridLines.Both;
                printBoard();
                Session.Add("board", board);
                Session.Add("player", player);
                Session.Add("bot", bot);
                Session.Add("game", game);

            }
            else
            {
                board = (GameBoard)Session["board"];
                player = (ConnectPlayer)Session["player"];
                bot = (ConnectBot)Session["bot"];
                game = (ConnectFour)Session["game"];
            }
        }

        protected void btnCol1_Click(object sender, EventArgs e)
        {
            handleMove(0);
        }

        protected void btnCol2_Click(object sender, EventArgs e)
        {
            handleMove(1);
        }

        protected void btnCol3_Click(object sender, EventArgs e)
        {
            handleMove(2);
        }

        protected void btnCol4_Click(object sender, EventArgs e)
        {
            handleMove(3);
        }

        protected void btnCol5_Click(object sender, EventArgs e)
        {
            handleMove(4);
        }

        protected void btnCol6_Click(object sender, EventArgs e)
        {
            handleMove(5);
        }

        protected void btnCol7_Click(object sender, EventArgs e)
        {
            handleMove(6);
        }

        protected void btnNewGame_Click(object sender, EventArgs e)
        {
            game.NewGame();
            btnCol1.Enabled = true;
            btnCol2.Enabled = true;
            btnCol3.Enabled = true;
            btnCol4.Enabled = true;
            btnCol5.Enabled = true;
            btnCol6.Enabled = true;
            btnCol7.Enabled = true;
            btnCol1.CssClass = "tileBtn";
            btnCol2.CssClass = "tileBtn";
            btnCol3.CssClass = "tileBtn";
            btnCol4.CssClass = "tileBtn";
            btnCol5.CssClass = "tileBtn";
            btnCol6.CssClass = "tileBtn";
            btnCol7.CssClass = "tileBtn";
            lblResult.CssClass = null;
            tblBoard.Rows.Clear();
            printBoard();
            lblResult.Text = "";
        }

        private void checkButtons()
        {
            if (board.OpenCells[0, 0] != board.Open)
            {
                btnCol1.CssClass = "locked";
                btnCol1.Enabled = false;
            }
            if (board.OpenCells[0, 1] != board.Open)
            {
                btnCol2.CssClass = "locked";
                btnCol2.Enabled = false;
            }
            if (board.OpenCells[0, 2] != board.Open)
            {
                btnCol3.CssClass = "locked";
                btnCol3.Enabled = false;
            }
            if (board.OpenCells[0, 3] != board.Open)
            {
                btnCol4.CssClass = "locked";
                btnCol4.Enabled = false;
            }
            if (board.OpenCells[0, 4] != board.Open)
            {
                btnCol5.CssClass = "locked";
                btnCol5.Enabled = false;
            }
            if (board.OpenCells[0, 5] != board.Open)
            {
                btnCol6.CssClass = "locked";
                btnCol6.Enabled = false;
            }
            if (board.OpenCells[0, 6] != board.Open)
            {
                btnCol7.CssClass = "locked";
                btnCol7.Enabled = false;
            }
        }

        private void printBoard()
        {
            tblBoard.Rows.Clear();
            for (int x = 0; x < 6; x++)
            {
                TableRow r = new TableRow();
                r.CssClass = "table";
                for (int y = 0; y < 7; y++)
                {
                    Image temp = new Image();
                    TableCell c = new TableCell();
                    c.CssClass = "table";
                    if (board.OpenCells[x, y] == board.PlayerOwned)
                    {
                        temp.ImageUrl = "../images/tiles/Player.png";

                    }
                    else if (board.OpenCells[x, y] == board.BotOwned)
                    {
                        temp.ImageUrl = "../images/tiles/Bot.png";
                    }
                    else
                    {
                        temp.ImageUrl = "../images/tiles/empty.png";
                    }
                    c.Controls.Add(temp);
                    r.Cells.Add(c);
                }
                tblBoard.Rows.Add(r);
            }
        }

        private void disableButtons()
        {
            btnCol1.Enabled = false;
            btnCol2.Enabled = false;
            btnCol3.Enabled = false;
            btnCol4.Enabled = false;
            btnCol5.Enabled = false;
            btnCol6.Enabled = false;
            btnCol7.Enabled = false;
        }

        private void handleMove(int z)
        {
            for (int i = 5; i > -1; i--)
            {
                if (board.OpenCells[i, z] == board.Open)
                {
                    Chip chip = new Chip(i, z);
                    player.TakeTurn(board, chip);
                    printBoard();
                    break;
                }
            }

            if (game.CheckWin(1, board) == 1)
            {
                game.EndGame(1);
                disableButtons();
                lblResult.CssClass = "msg";
                lblResult.Text = "Player Win";
            }
            else
            {
                bot.TakeTurn(board);
                printBoard();

                if (game.CheckWin(2, board) == 1)
                {
                    game.EndGame(2);
                    disableButtons();
                    lblResult.CssClass = "msg";
                    lblResult.Text = "Bot Win";
                }
                else if (game.CheckWin(2, board) == 2)
                {
                    game.EndGame(3);
                    disableButtons();
                    lblResult.CssClass = "msg";
                    lblResult.Text = "Tie";
                }
                checkButtons();
            }
        }
    }
}