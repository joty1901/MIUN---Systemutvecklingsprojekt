using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Media;
using MaterMinds;
using Npgsql;

namespace MaterMinds
{
    class Repository
    {
        
        private static string connectionString = ConfigurationManager.ConnectionStrings["universitetet"].ConnectionString;

       
        public static IEnumerable<Player> GetDbPlayers()
        {
            string stmt = "SELECT id, nickname FROM player ORDER BY(nickname) ASC";

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

        public static void AddPlayerScore(int player_id, int value)
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
                                        Date = (DateTime)reader["date"],
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

        public static void StartDb()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
            }

        }
    }
}
