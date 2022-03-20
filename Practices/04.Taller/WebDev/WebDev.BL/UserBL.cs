using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDev.DataAccess;
using WebDev.Services.Entities;

namespace WebDev.BL
{
    public class UserBL
    {
        private readonly UserDA _userDA;
        private string apiUsersUrl;

        public UserBL(UserDA userDA)
        {
            _userDA = userDA;
        }

        public UserBL(string apiUsersUrl)
        {
            this.apiUsersUrl = apiUsersUrl;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            return await _userDA.GetUsers();
        }

        public async Task<UserDto> GetUserById(int id)
        {
            return await _userDA.GetUserById(id);
        }

        public async Task<UserDto> AddUser(UserDto user)
        {
            return await _userDA.AddUser(user);
        }

        public async Task<UserDto> UpdateUser(UserDto user)
        {
            return await _userDA.UpdateUser(user);
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            return await _userDA.DeleteUser(id);
        }
    }
}