using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.DAO;
using DBLibrary.Entities;


namespace GamesLibrary.ConnectFour
{
    /// <summary>
    /// Handles the logic and the validation for the game.
    /// </summary>
    public class ConnectFour : Game
    {
        ConnectBot bot;
        ConnectPlayer player;
        GameBoard board;
        bool win = false;

        /// <summary>
        /// Constructor for the connect four class, sets game ID to CON
        /// </summary>
        /// <param name="bot">ConnectBot that the player will play against</param>
        /// <param name="player">ConnectPlayer that the player will use</param>
        /// <param name="board">Board that will keep track of the tiles</param>
        public ConnectFour(ConnectBot bot, ConnectPlayer player, GameBoard board)
        {
            this.bot = bot;
            this.player = player;
            this.board = board;
            GameID = "CON";
        }

        /// <summary>
        /// Sets the board to the default values
        /// </summary>
        public void NewGame()
        {
            board.Reset();
        }

        /// <summary>
        /// Modifies the GameResult to indicate who won the game, push is a tie.
        /// </summary>
        /// <param name="owner">Symbolizes who won the game</param>
        public void EndGame(int owner)
        {
            ResultDAO dao = new ResultDAO(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
            if (owner == 1)
            {
                this.GameResult = DBLibrary.GameResult.PLAYERWIN;
                dao.AddResult(new Result("CON", player.PlayerID, 1));

            }
            else if (owner == 2)
            {
                this.GameResult = DBLibrary.GameResult.AIWIN;
                dao.AddResult(new Result("CON", player.PlayerID, 0));
            }
            else
            {
                this.GameResult = DBLibrary.GameResult.PUSH;
            }
        }

        /// <summary>
        /// Checks if 4 tiles are conencted on the board.
        /// </summary>
        /// <param name="owner">owner of the tiles, will be either 1 or 2</param>
        /// <param name="board">board to check the tiles of</param>
        /// <returns>integer to show who won, returns 2 if a tie</returns>
        public int CheckWin(int owner, GameBoard board)
        {
            bool win = false;
            int count = 0;

            //checking vertical connections
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (board.OpenCells[x, y] == owner)
                    {
                        count++;
                        if (count == 4)
                        {
                            win = true;
                            break;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                if (count == 4)
                    break;
                else
                    count = 0;
            }
            count = 0;
            //checking horizontal connections
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    if (board.OpenCells[x, y] == owner)
                    {
                        count++;
                        if (count == 4)
                        {
                            win = true;
                            break;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                if (count == 4)
                    break;
                else
                    count = 0;
            }

            //checking the diagonals from top left to bottom right
            count = 0;
            for (int i = 0; i < 6; i++)
            {
                if (board.OpenCells[i, i] == owner)
                {
                    count++;
                    if (count == 4)
                    {
                        win = true;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            if (board.OpenCells[0, 1] == owner &&
                board.OpenCells[1, 2] == owner &&
                board.OpenCells[2, 3] == owner &&
                board.OpenCells[3, 4] == owner)
            { win = true; }

            else if (board.OpenCells[0, 2] == owner &&
                board.OpenCells[1, 3] == owner &&
                board.OpenCells[2, 4] == owner &&
                board.OpenCells[3, 5] == owner)
            { win = true; }

            else if (board.OpenCells[0, 3] == owner &&
                board.OpenCells[1, 4] == owner &&
                board.OpenCells[2, 5] == owner &&
                board.OpenCells[3, 6] == owner)
            { win = true; }

            else if (board.OpenCells[1, 0] == owner &&
                board.OpenCells[2, 1] == owner &&
                board.OpenCells[3, 2] == owner &&
                board.OpenCells[4, 3] == owner)
            { win = true; }

            else if (board.OpenCells[1, 2] == owner &&
                board.OpenCells[2, 3] == owner &&
                board.OpenCells[3, 4] == owner &&
                board.OpenCells[4, 5] == owner)
            { win = true; }

            else if (board.OpenCells[1, 3] == owner &&
                board.OpenCells[2, 4] == owner &&
                board.OpenCells[3, 5] == owner &&
                board.OpenCells[4, 6] == owner)
            { win = true; }

            else if (board.OpenCells[2, 0] == owner &&
                board.OpenCells[3, 1] == owner &&
                board.OpenCells[4, 2] == owner &&
                board.OpenCells[5, 3] == owner)
            { win = true; }

            else if (board.OpenCells[2, 1] == owner &&
                board.OpenCells[3, 2] == owner &&
                board.OpenCells[4, 3] == owner &&
                board.OpenCells[5, 4] == owner)
            { win = true; }

            else if (board.OpenCells[2, 2] == owner &&
                board.OpenCells[3, 3] == owner &&
                board.OpenCells[4, 4] == owner &&
                board.OpenCells[5, 5] == owner)
            { win = true; }

            else if (board.OpenCells[2, 3] == owner &&
                board.OpenCells[3, 4] == owner &&
                board.OpenCells[4, 5] == owner &&
                board.OpenCells[5, 6] == owner)
            { win = true; }

            //checking the diagonals from right to left

            else if (board.OpenCells[0, 6] == owner &&
                board.OpenCells[1, 5] == owner &&
                board.OpenCells[2, 4] == owner &&
                board.OpenCells[3, 3] == owner)
            { win = true; }

            else if (board.OpenCells[0, 5] == owner &&
                board.OpenCells[1, 4] == owner &&
                board.OpenCells[2, 3] == owner &&
                board.OpenCells[3, 2] == owner)
            { win = true; }

            else if (board.OpenCells[0, 4] == owner &&
                board.OpenCells[1, 3] == owner &&
                board.OpenCells[2, 2] == owner &&
                board.OpenCells[3, 1] == owner)
            { win = true; }

            else if (board.OpenCells[0, 3] == owner &&
                board.OpenCells[1, 2] == owner &&
                board.OpenCells[2, 1] == owner &&
                board.OpenCells[3, 0] == owner)
            { win = true; }

            else if (board.OpenCells[0, 3] == owner &&
                board.OpenCells[1, 2] == owner &&
                board.OpenCells[2, 1] == owner &&
                board.OpenCells[3, 0] == owner)
            { win = true; }

            else if (board.OpenCells[1, 6] == owner &&
                board.OpenCells[2, 5] == owner &&
                board.OpenCells[3, 4] == owner &&
                board.OpenCells[4, 3] == owner)
            { win = true; }
            else if (board.OpenCells[1, 5] == owner &&
                board.OpenCells[2, 4] == owner &&
                board.OpenCells[3, 3] == owner &&
                board.OpenCells[4, 2] == owner)
            { win = true; }
            else if (board.OpenCells[1, 4] == owner &&
                board.OpenCells[2, 3] == owner &&
                board.OpenCells[3, 2] == owner &&
                board.OpenCells[4, 1] == owner)
            { win = true; }
            else if (board.OpenCells[1, 3] == owner &&
                board.OpenCells[2, 2] == owner &&
                board.OpenCells[3, 1] == owner &&
                board.OpenCells[4, 0] == owner)
            { win = true; }

            else if (board.OpenCells[2, 6] == owner &&
                board.OpenCells[3, 5] == owner &&
                board.OpenCells[4, 4] == owner &&
                board.OpenCells[5, 3] == owner)
            { win = true; }
            else if (board.OpenCells[2, 5] == owner &&
                board.OpenCells[3, 4] == owner &&
                board.OpenCells[4, 3] == owner &&
                board.OpenCells[5, 2] == owner)
            { win = true; }
            else if (board.OpenCells[2, 4] == owner &&
                board.OpenCells[3, 3] == owner &&
                board.OpenCells[4, 2] == owner &&
                board.OpenCells[5, 1] == owner)
            { win = true; }
            else if (board.OpenCells[2, 3] == owner &&
                board.OpenCells[3, 2] == owner &&
                board.OpenCells[4, 1] == owner &&
                board.OpenCells[5, 0] == owner)
            { win = true; }
            count = 0;
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (board.OpenCells[x, y] != 0)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }

            if (count == 42)
            {
                return 2;
            }

            if (win)
            {
                return 1;
            }
            else
                return 0;
        }
    }
}
