using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Media;
using MaterMinds.Model;
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

        public static IEnumerable<Highscore> GetTopTenHigscore()
        {
            string stmt = "SELECT player.nickname, score.value from player INNER JOIN score ON score.player_id = player.id ORDER BY score.value DESC LIMIT 10";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                Highscore h = null;
                List<Highscore> highscores = new List<Highscore>();
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
                                    h = new Highscore

                                    {
                                        Nickname = reader["nickname"].ToString(),
                                        Value = (int)reader["value"]
                                    };
                                    highscores.Add(h);
                                    

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

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("nickname", nickname);
                    int id = (int)command.ExecuteScalar();

                    return id;
                }
            }
        }

        public static void StartDb()
        {
            //using (var conn = new NpgsqlConnection(connectionString))
            //{
            //    conn.Open();
            //}

        }

    }
}
