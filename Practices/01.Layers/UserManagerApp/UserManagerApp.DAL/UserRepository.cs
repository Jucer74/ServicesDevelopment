﻿
using System.Collections.Generic;
using UserManagerApp.Entities;
namespace UserManagerApp.DAL
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public List<User> GetUsers()
        {
            return _users;
        }
    }
}