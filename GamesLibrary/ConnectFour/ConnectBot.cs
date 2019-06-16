using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace GamesLibrary.ConnectFour
{
    /// <summary>
    /// Connect 4 Bot, child of the ConnectPlayer, handles the tile placement of the bot.
    /// </summary>
    public class ConnectBot : ConnectPlayer
    {
        /// <summary>
        /// Used for getting a random number 0-7
        /// </summary>
        Random random;

        /// <summary>
        /// Creates an instance of ConnectBot, usually made with null values
        /// </summary>
        /// <param name="id">Integer, user specified</param>
        /// <param name="name">String, user specified</param>
        /// <param name="email">String, user specified</param>
        public ConnectBot(int id, string name, string email) : base(id, name, email)
        {
            random = new Random();
            PlayerID = 0;
        }

        /// <summary>
        /// Creates an instance of ConnectBot, usually made with null values
        /// </summary>
        /// <param name="name">String, user specified</param>
        /// <param name="email">String, user specified</param>
        public ConnectBot(string name, string email) : base(name, email)
        {
            random = new Random();
            PlayerID = 0;
        }

        /// <summary>
        /// Handles the logic for the bot to place a chip, uses the random number to generate a column, then if there 
        /// is an pen cell, it will place the tile there, this loops until a suitable spot is found.
        /// </summary>
        /// <param name="board">Board to place a tile in the cells withing the board </param>
        /// <returns>An integer to show if it worked</returns>
        public int TakeTurn(GameBoard board)
        {
            Chip chip;
            bool validMove = false;

            while (!validMove)
            {
                //generates a random number for the column number 
                int col = random.Next(0, 7);

                //checks all the values in the row and places it in the first open one
                for (int row = 5; row > -1; row--)
                {
                    if (board.OpenCells[row, col] == board.Open)
                    {
                        chip = new Chip(row, col);
                        chip.Owner = 2;
                        PlacedChips.Add(chip);
                        board.OpenCells[chip.XValue, chip.YValue] = board.BotOwned;
                        return 1;
                    }
                }
            }
            return 0;
        }
    }
}
