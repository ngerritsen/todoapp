using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using TodoApp.Models;

namespace TodoApp.Persistence
{
    public class MysqlTodoStore : ITodoStore
    {
        private readonly string _connectionString;

        public MysqlTodoStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Todo> GetAll()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                
                var cmd = new MySqlCommand("SELECT * FROM todos ORDER BY timestamp", conn);
                var todos = new List<Todo>();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todos.Add(ParseTodo(reader));
                    }

                    return todos;
                }
            }
        }

        public void Add(string description)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                var cmd = new MySqlCommand("INSERT INTO todos (id, description) VALUES (@Id, @Description)", conn);

                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@Description", description);

                cmd.ExecuteNonQuery();
            }
        }

        public void Toggle(Guid id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                var cmd = new MySqlCommand("UPDATE todos SET is_completed = NOT is_completed WHERE id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", id.ToString());

                cmd.ExecuteNonQuery();
            }
        }

        public void Clear()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                var cmd = new MySqlCommand("DELETE FROM todos WHERE is_completed = true", conn);

                cmd.ExecuteNonQuery();
            }
        }

        private static Todo ParseTodo(IDataRecord reader)
        {
            return new Todo
            {
                Id = Guid.Parse(reader["id"].ToString()),
                IsCompleted = (bool) reader["is_completed"],
                Description = reader["description"].ToString()
            };
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
