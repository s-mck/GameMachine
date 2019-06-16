﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary.Entities
{
    /// <summary>
    /// Serves as class to populate result records from the database.
    /// </summary>
    public class Result
    {
        public int ResultID { get; set; }
        public string GameID { get; set; }
        public int PlayerID { get; set; }
        public int HumanWin { get; set; }

        /// <summary>
        /// Used by form as resultID is auto generated by the database.
        /// </summary>
        /// <param name="gameID">The ID of the game</param>
        /// <param name="playerID">The playerID associated to this result</param>
        /// <param name="humanWin">1 if the player won the game, 0 if they lost.</param>
        public Result(string gameID, int playerID, int humanWin)
        {
            GameID = gameID;
            PlayerID = playerID;
            HumanWin = humanWin;
        }

        /// <summary>
        /// Used by the DAO when retrieving records (with IDs) from the database
        /// </summary>
        /// <param name="resultID">The id of the result, generated by the database</param>
        /// <param name="gameID">The ID of the game</param>
        /// <param name="playerID">The playerID associated to this result</param>
        /// <param name="humanWin">1 if the player won the game, 0 if they lost.</param>
        public Result(int resultID, string gameID, int playerID, int humanWin)
        {
            ResultID = resultID;
            GameID = gameID;
            PlayerID = playerID;
            HumanWin = humanWin;
        }


    }
}