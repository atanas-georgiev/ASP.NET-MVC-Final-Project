namespace Planex.Services.Users
{
    using System.Linq;

    using Planex.Data.Models;

    public interface IUserService
    {
        void Add(User user, string role);

        IQueryable<User> GetAll();

        IQueryable<User> GetAllByRole(string role);

        User GetById(string id);

        string GetRoleName(User user);

        IQueryable<string> GetRoles();

        void SetRoleName(User user, string name);

        void Update(User user);
    }
}