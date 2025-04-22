using UserManagerApp.DAL;
using UserManagerApp.Entities;

namespace UserManagerApp.BL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public void AddUser(User user)
        {
            var existingUsers = _userRepository.GetUsers();
            foreach (var existingUser in existingUsers)
            {
                if (existingUser.Name == user.Name && existingUser.Email == user.Email)
                {
                    Console.WriteLine("Error: Ya existe un usuario con el mismo nombre y email.");
                    return;
                }
            }

            _userRepository.AddUser(user);
            Console.WriteLine("Usuario añadido");
        }

        public List<User> GetUsers()
        {

            return _userRepository.GetUsers();
        }
    }
}