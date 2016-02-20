namespace Planex.Services.Users
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Security;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Planex.Data;
    using Planex.Data.Common;
    using Planex.Data.Models;

    public class UserService : IUserService
    {
        private DbContext context;

        private IRepository<Image, int> images;

        RoleManager<IdentityRole> roleManager;

        private UserManager<User> userManager;

        private IRepository<User, string> users;

        public UserService(DbContext context, IRepository<User> users, IRepository<Image, int> images)
        {
            this.users = users;
            this.context = context;
            this.images = images;
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            this.roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        public void Add(User user, string role)
        {
            user.UserName = user.Email;
            user.CreatedOn = DateTime.UtcNow;
            this.userManager.Create(user, "changeme");
            user.IntId = int.Parse(user.Id.GetHashCode().ToString());
            user.ResetPassword = true;
            this.users.Update(user);
            this.userManager.AddToRole(user.Id, role);
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public IQueryable<User> GetAllByRole(string role)
        {
            var r = this.roleManager.FindByName(role);
            var userIds = r.Users.Select(u => u.UserId);
            var res = this.users.All().Where(u => userIds.Contains(u.Id));
            return res;
        }

        public User GetById(string id)
        {
            return this.users.GetById(id);
        }

        public string GetRoleName(User user)
        {
            var result = this.userManager.GetRoles(user.Id).FirstOrDefault();
            return result;
        }

        public string GetRoleNameById(string roleId)
        {
            var result = this.roleManager.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefault();
            return result;
        }

        public string GetRoleIdByName(string roleName)
        {
            return string.Empty;//this.roleManager.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefault();
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            var result = this.roleManager.Roles;
            return result.AsQueryable();
        }

        public void SetRoleName(User user, string name)
        {
            // todo: remove try catch
            try
            {
                this.userManager.RemoveFromRole(user.Id, this.GetRoleName(user));
            }
            catch (Exception)
            {
            }

            this.userManager.AddToRole(user.Id, name);
            this.users.Update(user);
        }

        public void Update(User user)
        {
            this.users.Update(user);
        }

        public void UpdatePassword(User user, string password)
        {
            user.UserName = user.Email;
            this.userManager.ChangePassword(user.Id, "changeme", password);
            this.users.Update(user);
        }
    }
}