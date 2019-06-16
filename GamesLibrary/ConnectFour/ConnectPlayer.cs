using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace GamesLibrary.ConnectFour
{
    /// <summary>
    /// Handles the logic for the player's move
    /// </summary>
    public class ConnectPlayer : Player
    {
        List<Chip> placedChips;

        /// <summary>
        /// Returns the user's placed Chips
        /// </summary>
        public List<Chip> PlacedChips
        {
            get { return placedChips; }
        }

        /// <summary>
        /// Constructor for the ConnectPlayer class, initializes the placedChips
        /// </summary>
        /// <param name="id">Player ID</param>
        /// <param name="name">Player Name</param>
        /// <param name="email">Player Email</param>
        public ConnectPlayer(int id, string name, string email) : base(id, name, email)
        {
            placedChips = new List<Chip>();
        }

        /// <summary>
        /// Constructor for the ConnectPlayer class, initializes the placedChips
        /// </summary>
        /// <param name="name">Player Name</param>
        /// <param name="email">Player Email</param>
        public ConnectPlayer(string name, string email) : base(name, email)
        {
            placedChips = new List<Chip>();
        }

        /// <summary>
        /// Handles the users move. Modifies the gameboard and adds the chip to the placed chips.
        /// </summary>
        /// <param name="board">board object of the connectfour object</param>
        /// <param name="chip">chip that will be placed</param>
        /// <returns>1 or 0 if the tile was placed</returns>
        public int TakeTurn(GameBoard board, Chip chip)
        {
            if (board.OpenCells[chip.XValue, chip.YValue] == board.Open)
            {
                chip.Owner = 1;
                placedChips.Add(chip);
                board.OpenCells[chip.XValue, chip.YValue] = board.PlayerOwned;
                return 1;
            }
            // The space selected is not open
            else
                return 0;
        }
    }
}