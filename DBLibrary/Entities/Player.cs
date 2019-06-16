using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary.Entities
{
    /// <summary>
    /// Base class for ConnectPlayer and BJPlayer.
    /// </summary>
    public class Player
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Used primarily by the DAO when retrieving a player from the database.
        /// </summary>
        /// <param name="id">The player's ID</param>
        /// <param name="name">The player's name</param>
        /// <param name="email">The player's email</param>
        public Player(int id, string name, string email)
        {
            PlayerID = id;
            PlayerName = name;
            Email = email;
        }

        // used in form before adding record to database
        /// <summary>
        /// Used primarily in the form, since the ID is automatically generated in the database it isn't
        /// necessary here.
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="email">The player's email</param>
        public Player(string name, string email)
        {
            PlayerName = name;
            Email = email;
        }

    }
}
