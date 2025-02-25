using UserManagerApp.DAL;
using UserManagerApp.Entities;

namespace UserManagerApp.BL

{
    public class UserManager
    {
        private readonly ICrudService<User> _userService;

        public UserManager(ICrudService<User> userService)
        {
            _userService = userService;
        }

        public void Add(string nombre, string email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                throw new ArgumentException("El correo electrónico es inválido (Debe contener '@' y '.').");
            }

            User newUser = new User
            {
                Id = _userService.GetAll().Count + 1,
                Name = nombre,
                Email = email
            };

            _userService.Add(newUser);
        }



        public List<User> GetAll()
        {
            var users = _userService.GetAll();
            return users;
        }
    }
}