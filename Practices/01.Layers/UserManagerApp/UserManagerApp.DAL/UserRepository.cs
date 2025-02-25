using UserManagerApp.Entities;

namespace UserManagerApp.DAL
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        public void UpdateUser(User userNewData)
        {
            _users.Find(us => us.Id == userNewData.Id).UpdateUser(userNewData);
        }

        public void DeleteUser(int idUserToDelete)
        {
            User user = _users.Where(us => us.Id == idUserToDelete).FirstOrDefault();
            _users.Remove(user);
        }


    }
}