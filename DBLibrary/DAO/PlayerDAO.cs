using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace DBLibrary.DAO
{
    /// <summary>
    /// Used to query the Player table of the GameMachine database.
    /// </summary>
    public class PlayerDAO
    {
        List<Player> players;
        Player player = null;
        string connString;

        /// <summary>
        /// Sets the parameter as the class-level field and instantiates a new list of players.
        /// </summary>
        /// <param name="connString">The connection string for the GameMachine database</param>
        public PlayerDAO(string connString)
        {
            this.connString = connString;
            players = new List<Player>();
        }

        /// <summary>
        /// Hashes player's password, creates a salt, and stores them in the database along with the players
        /// name and email address.
        /// </summary>
        /// <param name="p">The player to be added</param>
        /// <param name="password">The player's chosen password</param>
        public void AddPlayer(Player p, string password)
        {
            RNGCryptoServiceProvider rNG = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[16];
            rNG.GetBytes(saltBytes);

            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            byte[] combined = new byte[saltBytes.Length + passBytes.Length];

            saltBytes.CopyTo(combined, 0);
            passBytes.CopyTo(combined, saltBytes.Length);

            SHA512 sha512 = SHA512.Create();
            byte[] finalHashBytes = sha512.ComputeHash(combined);
            string base64Password = Convert.ToBase64String(finalHashBytes);
            string base64Salt = Convert.ToBase64String(saltBytes);

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into player(playerName, email, basePass, baseSalt) values" +
                "(@name, @email, @pass, @salt)");
            cmd.Parameters.AddWithValue("@name", p.PlayerName);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@pass", base64Password);
            cmd.Parameters.AddWithValue("@salt", base64Salt);

            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();
            UpdatePlayerID(p);
        }

        /// <summary>
        /// Since the database auto generates the ID, this is used to set the id in the class
        /// </summary>
        /// <param name="p">The player to be updated</param>
        private void UpdatePlayerID(Player p)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd2 = new SqlCommand("select playerID from player where email=@email");
            cmd2.Parameters.AddWithValue("@email", p.Email);
            cmd2.Connection = conn;

            SqlDataReader reader = cmd2.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["playerID"]);
                p.PlayerID = id;
            }
            conn.Close();
            reader.Close();
            cmd2.Dispose();

        }

        /// <summary>
        /// Queries the Player table to retrieve all players.
        /// </summary>
        /// <returns>A list of all players in the Player table</returns>
        public List<Player> ReadAllPlayers()
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from player");
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["playerID"]);
                string name = Convert.ToString(reader["playerName"]);
                string email = Convert.ToString(reader["email"]);
                player = new Player(id, name, email);
                players.Add(player);
            }

            conn.Close();
            reader.Close();
            cmd.Dispose();

            return players;
        }

        /// <summary>
        /// Retrieves the full name of the player by providing their email.
        /// </summary>
        /// <param name="email">The player's email address</param>
        /// <returns>The player's full name</returns>
        public string GetNamebyEmail(string email)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select playerName from player where email=@email");
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            string pName = "";
            while (reader.Read())
                pName = Convert.ToString(reader["playerName"]);

            conn.Close();
            reader.Close();
            cmd.Dispose();

            return pName;
        }

        /// <summary>
        /// Retrieves a player's ID by providing their email.
        /// </summary>
        /// <param name="email">The player's email address</param>
        /// <returns>The player's ID</returns>
        public int GetIdByEmail(string email)
        {

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select playerID from player where email=@email");
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            int id = 0;
            if (reader.Read())
                id = Convert.ToInt32(reader["playerID"]);

            conn.Close();
            reader.Close();
            cmd.Dispose();

            return id;
        }

        /// <summary>
        /// Authenticates a player by comparing the hashed password stored to the one entered.
        /// </summary>
        /// <param name="email">The email address entered on the login form</param>
        /// <param name="loginPass">The password entered on the login form</param>
        /// <returns>True if player exists and password matches. False otherwise.</returns>
        public bool AuthenticatePlayer(string email, string loginPass)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from player where email=@email");
            cmd.Parameters.AddWithValue("@email", email);

            cmd.Connection = conn;
            SqlDataReader reader = cmd.ExecuteReader();

            string base64Password;

            if (reader.Read())
            {
                string dSalt = reader["baseSalt"].ToString();
                string dPass = reader["basePass"].ToString();

                byte[] pass = Encoding.UTF8.GetBytes(loginPass);
                byte[] salt = Convert.FromBase64String(dSalt);

                byte[] readCombined = new byte[salt.Length + pass.Length];
                salt.CopyTo(readCombined, 0);
                pass.CopyTo(readCombined, salt.Length);

                SHA512 sha512 = SHA512.Create();
                byte[] finalHashBytes = sha512.ComputeHash(readCombined);
                base64Password = Convert.ToBase64String(finalHashBytes);

                conn.Close();
                reader.Close();
                cmd.Dispose();

                if (base64Password.Equals(dPass))
                    return true;
                else
                    return false;
            }
            conn.Close();
            reader.Close();
            cmd.Dispose();
            return false;
        }
    }
}
