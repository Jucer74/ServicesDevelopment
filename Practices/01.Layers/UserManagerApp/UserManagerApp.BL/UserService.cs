using System;
using System.Collections.Generic;
using UserManagerApp.Entities;
using UserManagerApp.DAL;

namespace UserManagerApp.BL
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User? GetUserById(int id)
        {
            return _userRepository.GetUsers().Find(u => u.Id == id);
        }

        public List<User> GetUsersByName(string name)
        {
            return _userRepository.GetUsers().FindAll(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }
    }
}
