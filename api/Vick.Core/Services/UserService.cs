using System;
using System.Collections.Generic;
using Vick.Core.Interfaces;
using Vick.Core.Models;

namespace Vick.Core.Services
{
    public class UserService : IUserService
    {
        private User _activeUser;
        private List<User> _users = new List<User>();
        private readonly Random _random = new Random();
        private int _lastUserId = 0;

        public UserService()
        {
            _activeUser = GetUser() ?? CreateTestUser();
        }

        public User GetUser()
        {
            return _activeUser;
        }

        private User CreateTestUser()
        {
            string testUserName = "Test User";
            string testUserEmail = "test@test.test";
            string testUserPassword = "test";
            string testUserPhoneNumber = "1234567890";
            List<TimeSlot> testUserTimeSlots = new List<TimeSlot>();

            User testUser = new User
            {
                Id = GenerateUserId(),
                Name = testUserName,
                Email = testUserEmail,
                Password = testUserPassword,
                PhoneNumber = testUserPhoneNumber,
                TimeSlots = testUserTimeSlots
            };

            _users.Add(testUser);

            return testUser;
        }

        public User GetUser(int id)
        {
            return _users.Find(x => x.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return _users.Find(x => x.Email == email);
        }

        public void UpdateUser(User user)
        {
            var index = _users.FindIndex(x => x.Id == user.Id);
            _users[index] = user;
        }

        public User CreateUser(string name, string email, string password, string phoneNumber, List<TimeSlot> timeSlots)
        {
            var newUser = new User
            {
                Id = GenerateUserId(),
                Name = name,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                TimeSlots = timeSlots
            };
            _users.Add(newUser);
            return newUser;
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public int GenerateUserId()
        {
            int newId;
            do
            {
                newId = _random.Next(1000, 10000);
            } while (newId == _lastUserId);

            _lastUserId = newId;
            return newId;
        }

        public void DeleteUser(int id)
        {
            var userToRemove = _users.Find(x => x.Id == id);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
            }
        }
    }
}
