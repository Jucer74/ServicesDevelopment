// BL/UserService.cs
using UserManagementApp.DAL;
using System.Collections.Generic;

namespace UserManagementApp.BL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public bool AddUser(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
            {
                return false;
            }
            _userRepository.AddUser(user);
            return true;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public bool UpdateUser(int id, string newName, string newEmail)
        {
            var user = _userRepository.GetUsers().Find(u => u.Id == id);
            if (user != null)
            {
                user.Name = newName;
                user.Email = newEmail;
                return true;
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            var user = _userRepository.GetUsers().Find(u => u.Id == id);
            if (user != null)
            {
                _userRepository.GetUsers().Remove(user);
                return true;
            }
            return false;
        }
    }
}
