// BL/UserService.cs
using System.Collections.Generic;
using UserManagementApp.DAL;

namespace UserManagementApp.BL
{
    public class UserService
    {
        // Instancia de UserRepository para interactuar con la capa de datos.
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        // Método para agregar un usuario, donde podrías incluir validaciones o lógica adicional.
        public void AddUser(User user)
        {
            // Por ejemplo, podríamos verificar que el email tenga un formato correcto antes de agregar el usuario.
            _userRepository.AddUser(user);
        }

        // Método para obtener la lista de usuarios. Aquí podrías filtrar o transformar los datos si fuera necesario.
        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}
