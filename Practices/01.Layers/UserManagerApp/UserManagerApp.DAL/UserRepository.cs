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

        public List<User> GetAllUsers()
        {
            return users;
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public User GetUser(int id)
        {
            return new User();
        }

        public void UpdateUser(User user)
        {
            var existingUser = GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
            }
        }
        public User GetUserById(int id)
        {
            return users.Find(u => u.Id == id);
        }

    }
}