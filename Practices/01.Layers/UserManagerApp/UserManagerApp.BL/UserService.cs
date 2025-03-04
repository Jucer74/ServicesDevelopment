// BL/UserService.cs
using UserManagerApp.DAL;
using UserManagerApp.Entities;


namespace UserManagerApp.BL{
    public class UserService{
        private readonly UserRepository _userRepository;

        public UserService(){
            _userRepository = new UserRepository();
        }

        public void AddUser(User user){
            if (_userRepository.GetUsers().Any(u => u.Email == user.Email))
            {
                throw new Exception("User with this email already exist!\n");
            }
            _userRepository.AddUser(user);
            
        }

        public List<User> GetUsers(){
            // Aquí podrías agregar lógica adicional, como filtros o transformaciones
            return _userRepository.GetUsers();
        }
    }
}



            