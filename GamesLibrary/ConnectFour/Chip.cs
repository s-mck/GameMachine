using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace GamesLibrary.ConnectFour
{
    /// <summary>
    /// Chip class, used to keep track of the players or the bots tiles on the board
    /// </summary>
    public class Chip
    {
        /// <summary>
        /// x value on the 2d array, hold 0-6
        /// </summary>
        int xValue;
        /// <summary>
        /// y value on the 2d array holds 0-6
        /// </summary>
        int yValue;
        /// <summary>
        /// owner of the tile, will be either one or two depending on who placed the tile, used for validation in other classes
        /// </summary>
        int owner; // either 1 or 2: 1 is Bot, 2 is Human, 0 has no owner yet

        /// <summary>
        /// property for the xValue, used to set and retrieve it, the set is private because it doesnt need to be set otuside the class
        /// throws an exception if not in valid range
        /// </summary>
        public int XValue
        {
            get { return xValue; }
            private set
            {
                if (value > -1 && value < 7)
                    xValue = value;
                else
                    throw new Exception("Not a valid row.");
            }
        }
        /// <summary>
        /// property for the yValue, used to set and retrieve it, the set is private because it doesnt need to be set otuside the class
        /// throws an exception if not in valid range
        /// </summary>
        public int YValue
        {
            get { return yValue; }
            private set
            {
                if (value > -1 && value < 7)
                    yValue = value;
                else
                    throw new Exception("Not a valid column.");
            }
        }
        /// <summary>
        /// Property for the owner, the value entered must be equal to one or two to indicate who placed the tile.
        /// </summary>
        public int Owner
        {
            get { return owner; }
            set
            {
                if (value == 1 || value == 2)
                    owner = value;
                else
                    throw new Exception("Not a valid tile owner, must be one or two.");
            }
        }

        /// <summary>
        /// Constructor for the chip class, sets the x and y value to a user specified value, owner defaults to 0 to represent no owner
        /// </summary>
        /// <param name="x">Integer, must be between 0-6</param>
        /// <param name="y">Integer, must be between 0-6</param>
        public Chip(int x, int y)
        {
            XValue = x;
            YValue = y;
            owner = 0;
        }

        /// <summary>
        /// Compares the x and y values of a chip to see if they are in the same location
        /// </summary>
        /// <param name="chip">A Chip object to pull the x and y values from</param>
        /// <returns>Returns true if the chips are equal, false if they are not</returns>
        public bool Equals(Chip chip)
        {
            if (xValue == chip.XValue && yValue == chip.YValue)
                return true;

            return false;
        }
    }
}
