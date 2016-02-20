namespace Planex.Services.Users
{
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Planex.Data.Models;

    public interface IUserService
    {
        void Add(User user, string role);

        IQueryable<User> GetAll();

        IQueryable<User> GetAllByRole(string role);

        User GetById(string id);

        string GetRoleName(User user);

        string GetRoleNameById(string roleId);

        string GetRoleIdByName(string roleName);

        IQueryable<IdentityRole> GetRoles();

        void SetRoleName(User user, string name);

        void Update(User user);

        void UpdatePassword(User user, string password);
    }
}