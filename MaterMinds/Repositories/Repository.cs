using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Media;
using MaterMinds.Input;
using Npgsql;

namespace MaterMinds
{
    public class Repository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["universitetet"].ConnectionString;

       
        public static IEnumerable<User> GetPlayers()
        {
            string stmt = "select id, nickname from player";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                User user = null;
                List<User> users = new List<User>();
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
                                    user = new User
                                    {
                                        Id = (int)reader["id"],
                                        Nickname = (string)reader["nickname"],
                                    };
                                    users.Add(user);
                                }
                            }
                        }
                        trans.Commit();
                        return users;

                    }
                    catch (PostgresException)
                    {
                        trans.Rollback();
                        throw;
                    }

            }
        }



        public static void AddUserWithScore(Score score, User user)
        {
            string stmt = "INSERT INTO player(nickname) values(@nickname) returning id";
            string stmt2 = "INSERT INTO score(player_id, score) values(@player_id, @score)";

            int userId;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var command = new NpgsqlCommand(stmt, conn))
                        {
                            //command.Parameters.AddWithValue("user_id", user.Id);
                            command.Parameters.AddWithValue("nickname", user.Nickname);
                            userId = (int)command.ExecuteScalar();
                        }

                        using (var command = new NpgsqlCommand(stmt2, conn))
                        {
                            command.Parameters.AddWithValue("id", userId);
                            command.Parameters.AddWithValue("score", score.Value);
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

        public static IEnumerable<Score> GetUserHighscore(User u)
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
                            command.Parameters.AddWithValue("id", u.Id);

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

        public static IEnumerable<Score> GetTopTen()
        {

            string stmt = "select value,player_id from score order by value desc limit 10";

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
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    score = new Score
                                    {
                                        Value = (int)reader["value"],
                                        UserId = (int)reader["player_id"]
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

        //public static int AddUser(User user)
        //{
        //    string stmt = "INSERT INTO users(nickname) values(@nickname) returning id";

        //    using (var conn = new NpgsqlConnection(connectionString))
        //    {
        //        using (var command = new NpgsqlCommand(stmt, conn))
        //        {
        //            conn.Open();
        //            command.Parameters.AddWithValue("user_id", user.Id);
        //            command.Parameters.AddWithValue("nickname", user.Nickname);
        //            int id = (int)command.ExecuteScalar();
        //            user.Id = id;
        //            return id;
        //        }
        //    }
        //}

    }
}
