using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using MaterMinds.Input;
using Npgsql;

namespace MaterMinds
{
    public class Repository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["universitetet"].ConnectionString;

        #region User
        public static IEnumerable<User> GetUsers()
        {
            string stmt = "select user_id, nickname from users";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                User user = null;
                List<User> users = new List<User>();
                conn.Open();

                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User
                            {
                                Id = (int)reader["user_id"],
                                Nickname = (string)reader["nickname"],
                            };
                            users.Add(user);
                        }
                    }
                }
                return users;
            }
        }


        #endregion

        public static void AddUserWithScore(ScoreBoard score, User user)
        {
            string stmt = "INSERT INTO users(nickname) values(@nickname) returning user_id";
            string stmt2 = "INSERT INTO scoreboard(user_id, score) values(@user_id, @score)";

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
                            command.Parameters.AddWithValue("user_id", userId);
                            command.Parameters.AddWithValue("score", score.Score);
                            command.ExecuteScalar();
                        }
                        trans.Commit();
                    }

                    
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }


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
