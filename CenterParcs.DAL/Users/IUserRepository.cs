using System.Collections.Generic;

using CenterParcs.Models.Users;

namespace CenterParcs.DAL.Users
{
    public interface IUserRepository
    {
        IList<User> GetAllUsers();

        User GetUserById(string userId);

        User GetUserByUsername(string username);

        void DeleteUser(User user);

        void UpdateUser(User user);
    }
}