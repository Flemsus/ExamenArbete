using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Vick.Core.Interfaces;
using Vick.Core.Models;

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
                var user = connection.QueryFirstOrDefault<User>("SELECT TOP 1 * FROM Users");
                if (user != null)
                {
                    user.UserArt = GetArtworksByUserId(user.Id);
                }
                return user;
            }
        }

        public User GetUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var user = connection.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
                if (user != null)
                {
                    user.UserArt = GetArtworksByUserId(id);
                }
                return user;
            }
        }


        public User GetUserByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var user = connection.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = email });
                if (user != null)
                {
                  
                    user.UserArt = GetArtworksByUserId(user.Id);
                }
                return user;
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
                var users = connection.Query<User>("SELECT * FROM Users").ToList();
                foreach (var user in users)
                {
                    user.UserArt = GetArtworksByUserId(user.Id);
                }
                return users;
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



        public List<Artwork> GetAllArtworks()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Artwork>("SELECT * FROM Artworks").ToList();
            }
        }

        public Artwork AddArtwork(Artwork artwork)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var id = connection.QuerySingle<int>(
                    "INSERT INTO Artworks (ImageUrl, ArtName, ArtCreatorId, ArtDescription, ArtPrice) OUTPUT INSERTED.Id VALUES (@ImageUrl, @ArtName, @ArtCreatorId, @ArtDescription, @ArtPrice)",
                    artwork);
                artwork.Id = id;
                return artwork;
            }
        }


        private List<Artwork> GetArtworksByUserId(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Artwork>("SELECT * FROM Artworks WHERE ArtCreatorId = @UserId", new { UserId = userId }).ToList();
            }
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute(
                    "UPDATE Artworks SET ArtName = @ArtName, ArtDescription = @ArtDescription, ArtPrice = @ArtPrice WHERE Id = @Id",
                    new
                    {
                        artwork.Id,
                        artwork.ArtName,
                        artwork.ArtDescription,
                        artwork.ArtPrice
                    });
                return rowsAffected > 0;
            }
        }

        public bool DeleteArtwork(int artworkId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute("DELETE FROM Artworks WHERE Id = @Id", new { Id = artworkId });
                return rowsAffected > 0;
            }
        }



    }
}
