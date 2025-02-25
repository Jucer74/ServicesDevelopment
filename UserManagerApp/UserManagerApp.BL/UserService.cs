// BL/UserService.cs
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
            // Aquí podrías agregar validaciones o lógica adicional
            _userRepository.AddUser(user);
        }

        public List<User> GetUsers()
        {
            // Aquí podrías agregar lógica adicional, como filtros o transformaciones
            return _userRepository.GetUsers();
        }
    }
}