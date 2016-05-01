using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using CenterParcs.DAL.DbContexts;
using CenterParcs.Models.Users;

namespace CenterParcs.DAL.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly CenterParcsDbContext _centerParcsDbContext;

        public UserRepository(CenterParcsDbContext centerParcsDbContext)
        {
            _centerParcsDbContext = centerParcsDbContext;
        }

        public IList<User> GetAllUsers()
        {
            var dbQuery = _centerParcsDbContext.Users;

            return dbQuery.ToList();
        }

        public User GetUserByUsername(string username)
        {
            var dbQuery = _centerParcsDbContext.Users.Where(u => u.UserName == username);

            return dbQuery.FirstOrDefault();
        }

        public User GetUserById(string userId)
        {
            var dbQuery = _centerParcsDbContext.Users.Where(u => u.Id == userId);

            return dbQuery.FirstOrDefault();
        }

        public void DeleteUser(User user)
        {
            _centerParcsDbContext.Entry(user).State = EntityState.Deleted;

            _centerParcsDbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _centerParcsDbContext.Entry(user).State = EntityState.Modified;

            _centerParcsDbContext.SaveChanges();
        }
    }
}
