using System.Linq;
using Planex.Data.Models;

namespace Planex.Services.Users
{
    public interface IUserService
    {
        IQueryable<string> GetRoles();
        IQueryable<User> GetAll();
        User GetById(string id);
        void Add(User user, string role);
        void Update(User user);
        string GetRoleName(User user);
        void SetRoleName(User user, string name);
    }
}
