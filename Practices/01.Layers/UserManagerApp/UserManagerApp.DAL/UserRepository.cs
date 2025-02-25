using System.Collections.Generic;
using UserManagerApp.Entities;

namespace UserManagerApp.DAL
{
    public class UserRepository
    {
        private List<User> users = new List<User>();

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public bool DeleteUser(int id) //logica para eliminar usuario
        {
            var user = users.Find(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
                return true;
            }
            return false;
        }
    }
}
