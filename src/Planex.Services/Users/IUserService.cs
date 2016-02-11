using System.Linq;
using Planex.Data.Models;

namespace Planex.Services.Users
{
    public interface IUserService
    {
        IQueryable<string> GetRoles();
        IQueryable<User> GetAll();
        void Add(User user, string role);
    }
}
