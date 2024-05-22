using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Vick.Core.Interfaces;
using Vick.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;



namespace Vick.Core.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public User GetUser()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<User>("SELECT TOP 1 * FROM Users");
            }
        }

        public User GetUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = email });
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute("UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @Id", user);
                if (rowsAffected == 0)
                {
                    throw new ArgumentException($"User with ID {user.Id} not found.");
                }
            }
        }

        public User CreateUser(string name, string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var newUser = new User
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    RoleId = 2 
                };

                var id = connection.QuerySingle<int>("INSERT INTO Users (Name, Email, Password, RoleId) OUTPUT INSERTED.Id VALUES (@Name, @Email, @Password, @RoleId)", newUser);
                newUser.Id = id;
                return newUser;
            }
        }



        public List<User> GetAllUsers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute("DELETE FROM Users WHERE Id = @Id", new { Id = id });
                if (rowsAffected == 0)
                {
                    throw new ArgumentException($"User with ID {id} not found.");
                }
            }
        }
    }

   
}
