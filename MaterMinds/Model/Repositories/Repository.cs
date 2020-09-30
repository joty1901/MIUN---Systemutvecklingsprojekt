using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using Npgsql;

namespace MaterMinds
{
    static class Repository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["universitetet"].ConnectionString;

        /// <summary>
        /// Get's all the players that are in the database in table player and returnes a ObservableCollection of players
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Player> GetDbPlayers()
        {
            string stmt = "SELECT id, nickname FROM player ORDER BY(nickname) ASC";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = null;
                ObservableCollection<Player> players = new ObservableCollection<Player>();
                conn.Open();
                using (var trans = conn.BeginTransaction())
                    try
                    {
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    player = new Player
                                    {
                                        Id = (int)reader["id"],
                                        Nickname = (string)reader["nickname"],
                                    };
                                    players.Add(player);
                                }
                            }
                        }
                        trans.Commit();
                        return players;

                    }
                    catch (PostgresException)
                    {
                        trans.Rollback();
                        throw;
                    }

            }
        }

        /// <summary>
        /// Adds a score to the table score in the database with associated player_id
        /// </summary>
        public static void AddScoreWithPlayerId(int player_id, int value)
        {
            string stmt = "INSERT INTO score(player_id, value) values(@player_id, @value)";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {                     
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            command.Parameters.AddWithValue("player_id", player_id);
                            command.Parameters.AddWithValue("value", value);
                            command.ExecuteScalar();
                        }
                        trans.Commit();
                    }
                    catch (PostgresException)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Get's the ten best scores from table score and returns them in order by the highest score
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Score> GetTopTenHigscore()
        {
            string stmt = "SELECT player.nickname, score.value, score.date from player INNER JOIN score ON score.player_id = player.id ORDER BY score.value DESC LIMIT 10";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Score score = null;
                List<Score> highscores = new List<Score>();
                conn.Open();
                using (var trans = conn.BeginTransaction())
                    try
                    {
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    score = new Score
                                    {
                                        Nickname = reader["nickname"].ToString(),
                                        Value = (int)reader["value"],
                                        Date = Convert.ToString((DateTime)reader["date"])

                                    };
                                    highscores.Add(score);
                                }
                            }
                        }
                        trans.Commit();
                        return highscores;

                    }
                    catch (PostgresException)
                    {
                        trans.Rollback();
                        throw;
                    }
            }
        }

        /// <summary>
        /// Get's the most frequent players by counting the player_id and returns a list of Score
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Score> GetTopFrequentPlayers()
        {
            string stmt = "SELECT nickname, COUNT(player_id)::int FROM score INNER JOIN player ON player.id=player_id GROUP BY nickname ORDER BY COUNT DESC LIMIT 10";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Score score = null;
                List<Score> highscores = new List<Score>();
                conn.Open();
                using (var trans = conn.BeginTransaction())
                    try
                    {
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    score = new Score
                                    {
                                        Nickname = reader["nickname"].ToString(),
                                        Value = (int)reader["count"]
                                    };
                                    highscores.Add(score);
                                }
                            }
                        }
                        trans.Commit();
                        return highscores;

                    }
                    catch (PostgresException)
                    {
                        trans.Rollback();
                        throw;
                    }
            }
        }

        /// <summary>
        /// Addsd a created player to the database
        /// </summary>
        /// <returns></returns>
        public static int AddPlayer(string nickname)
        {
            string stmt = "INSERT INTO player(nickname) values(@nickname) returning id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    int id;
                    try
                    {
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            command.Parameters.AddWithValue("nickname", nickname);
                            id = (int)command.ExecuteScalar();
                        }
                        trans.Commit();
                        return id;

                    }
                    catch (PostgresException exm)
                    {
                        trans.Rollback();
                        throw exm;
                    }
                }
            }
        }

        /// <summary>
        /// Method that's being used in the start of the application to minimize delay in buttonclicks when the game is up and running
        /// </summary>
        public static void StartDb()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    throw;
                }
            }

        }
    }
}
 