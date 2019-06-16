using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary.Entities
{
    /// <summary>
    /// Base class for Blackjack and ConnectFour.
    /// </summary>
    public class Game
    {
        string gameID;
        string name;
        GameResult gameResult;

        public string GameID {
            get { return gameID; }
            set
            {
                if (value == "CON" || value == "BLA")
                    gameID = value;
                else
                    throw new ArgumentException("Invalid game ID");
            }
        }
        public string Name {
            get { return name; }
            set
            {
                if (value == "Blackjack" || value == "Connect Four")
                    name = value;
                else
                    throw new ArgumentException("Invalid name");
            }
        }

        public GameResult GameResult {
            get { return gameResult; }
            set {
                if (value.GetType() == typeof(GameResult))
                    gameResult = value;
                else
                    throw new ArgumentException("Invalid data type");
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Game() { }

        /// <summary>
        /// Sets the GameID property using the parameter.
        /// </summary>
        /// <param name="id">The gameID to be used</param>
        public Game(string id)
        {
            GameID = id;
        }



    }
}
