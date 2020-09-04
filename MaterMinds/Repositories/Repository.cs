using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Npgsql;

namespace MaterMinds
{
    public class Repository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["universitetet"].ConnectionString;

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




    }
}
