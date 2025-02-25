using UserManagerApp.DAL;
using UserManagerApp.Entities;
namespace UserManagerApp.BL
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public void AddUser(User user)
        {
            if (userRepository.GetUserById(user.Id) != null)
            {
                throw new ArgumentException("El usuario ya existe.");
            }
            userRepository.AddUser(user);
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public void DeleteUser(User user)
        {
            userRepository.DeleteUser(user);
        }

        public void UpdateUser(User user)
        {
            userRepository.UpdateUser(user);
        }

        public User GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }

        
    }
}
