//using UserManagerApp.UserManagerApp.DAL;
using UserManagerApp.Entities;
using UserManagerApp.DAL;

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

        public string UpdateUser(User userNewData)
        {
            User user = GetUsers().Where(us => us.Id == userNewData.Id).FirstOrDefault();
            if(user != null)
            {
                Console.WriteLine(user.Name);
                _userRepository.UpdateUser(userNewData);
                return "Usuario actualizado";
            }
            return "El usuario a modificar no existe";
        }

        public string DeteleUser(int idUserToDelete)
        {
            User user = GetUsers().Where(us => us.Id == idUserToDelete).FirstOrDefault();
            if(user != null)
            {
                _userRepository.DeleteUser(idUserToDelete);
                return "Usuario eliminado";
            }
            
            return "El usuario a eliminar no existe";
        }
    }
}
