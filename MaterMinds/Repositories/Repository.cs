using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Npgsql;

namespace MaterMinds
{
    class Repository
    {
        
        private static string connectionString = ConfigurationManager.ConnectionStrings["universitetet"].ConnectionString;

       
        public static IEnumerable<Player> GetDbPlayers()
        {
            string stmt = "select id, nickname from player";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Player player = null;
                List<Player> players = new List<Player>();
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

        public static void AddPlayerScore(int playerId, int score)
        {
            string stmt = "INSERT INTO score(player_id, score) values(@playerId, @score)";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {                     
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            command.Parameters.AddWithValue("player_id", playerId);
                            command.Parameters.AddWithValue("score", score);
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

        public static IEnumerable<Score> GetUserHighscore(Player player)
        {
            string stmt = "select value from score where player_id =@id order by value desc limit 10";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Score score = null;
                List<Score> scoreboard = new List<Score>();
                conn.Open();
                using (var trans = conn.BeginTransaction())
                    try
                    {
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            command.Parameters.AddWithValue("id", player.Id);

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    score = new Score
                                    {
                                        Value = (int)reader["value"]
                                    };
                                    scoreboard.Add(score);
                                }
                            }
                        }
                        trans.Commit();
                        return scoreboard;
                    }
                    catch (PostgresException)
                    {
                        trans.Rollback();
                        throw;
                    }

            }
        }

        public static IEnumerable<string> GetTopTenHigscore()
        {
            string stmt = "SELECT player.nickname, score.value from player INNER JOIN score ON score.player_id = player.id ORDER BY score.value DESC LIMIT 10";

            using (var conn = new NpgsqlConnection(connectionString))
            {
               
                List<string> highscores = new List<string>();
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
                                    string s;
                                    {
                                        s = reader["nickname"].ToString();
                                        s += "...............";
                                        s += reader["value"].ToString();
                                    };
                                    highscores.Add(s);

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

        public static void AddPlayer(string  nickname)
        {
            string stmt = "INSERT INTO player(nickname) values(@nickname) returning id";

            using (var conn = new NpgsqlConnection(connectionString))
            {

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    //command.Parameters.AddWithValue("player_id", Id);
                    command.Parameters.AddWithValue("nickname", nickname);
                    int id = (int)command.ExecuteScalar();
                }
            }
        }

    }
}
