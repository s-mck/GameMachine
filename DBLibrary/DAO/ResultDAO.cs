using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace DBLibrary.DAO
{
    /// <summary>
    /// Used to query the Result table in the GameMachine database.
    /// </summary>
    public class ResultDAO
    {
        string connString;
        Result result;

        /// <summary>
        /// Sets the parameter as the class-level field.
        /// </summary>
        /// <param name="connString">The connection string for the GameMachine database</param>
        public ResultDAO(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// Adds a new result record to the database.
        /// </summary>
        /// <param name="r">The result to be added</param>
        public void AddResult(Result r)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into result values(@gameID, @playerID, @humanWin)");
            cmd.Parameters.AddWithValue("@gameID", r.GameID);
            cmd.Parameters.AddWithValue("@playerID", r.PlayerID);
            cmd.Parameters.AddWithValue("@humanWin", r.HumanWin);

            cmd.Connection = conn;
            cmd.ExecuteNonQuery();

            conn.Close();
            cmd.Dispose();
        }

        /// <summary>
        /// Retrieves all records for a specific player using their playerID.
        /// </summary>
        /// <param name="playerID">The ID of the player to be searched</param>
        /// <returns>A list of results specific to that player</returns>
        public List<Result> GetAllResultsByID(int playerID)
        {
            List<Result> results = new List<Result>();

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from result where playerID=@pid");
            cmd.Parameters.AddWithValue("@pid", playerID);
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int rid = Convert.ToInt32(reader["resultID"]);
                int pid = Convert.ToInt32(reader["playerID"]);
                string gid = Convert.ToString(reader["gameID"]);
                int hWin = Convert.ToInt32(reader["humanWin"]);
 
                result = new Result(rid, gid, pid, hWin);
                results.Add(result);
            }
            conn.Close();
            reader.Close();
            cmd.Dispose();

            return results;
        }


    }
}
