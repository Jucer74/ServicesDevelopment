// DAL/UserRepository.cs
using System.Collections.Generic;

namespace UserManagementApp.DAL
{
    public class UserRepository
    {
        // Usamos una lista estática para simular una base de datos en memoria.
        private static List<User> _users = new List<User>();

        // Método para agregar un usuario a la "base de datos"
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        // Método para obtener la lista de usuarios
        public List<User> GetUsers()
        {
            return _users;
        }
    }

    // Entity: definición de la clase User que representa a un usuario.
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
