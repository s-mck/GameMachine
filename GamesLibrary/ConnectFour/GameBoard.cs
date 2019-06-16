using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesLibrary.ConnectFour
{
    /// <summary>
    /// Handles the logic for the gameboard
    /// </summary>
    public class GameBoard
    {
        const int open = 0;
        const int playerOwned = 1;
        const int botOwned = 2;
        int[,] openCells = new int[6, 7];

        /// <summary>
        /// Returns a constant value of 0
        /// </summary>
        public int Open { get { return open; } }
        /// <summary>
        /// Returns a constant value of 1
        /// </summary>
        public int PlayerOwned { get { return playerOwned; } }
        /// <summary>
        /// Returns a constant value of 2
        /// </summary>
        public int BotOwned { get { return botOwned; } }

        /// <summary>
        /// returns the array of that status of the cells
        /// </summary>
        public int[,] OpenCells {get { return openCells; }}

        /// <summary>
        /// Constructor, sets all the board values to open
        /// </summary>
        public GameBoard()
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 7; y++)
                    openCells[x, y] = open;
            }
        }

        /// <summary>
        /// Sets all the board values to open
        /// </summary>
        public void Reset()
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 7; y++)
                    openCells[x, y] = open;
            }
        }
    }
}

