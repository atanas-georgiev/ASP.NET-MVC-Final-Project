using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Planex.Data;
using Planex.Data.Models;

namespace Planex.Services.Users
{
    public class UserService : IUserService
    {
        private DbContext context;
        private IRepository<User> users;
        private IRepository<Image> images;
        private UserManager<User> userManager;
        RoleManager<IdentityRole> roleManager;

        public UserService(DbContext context, IRepository<User> users, IRepository<Image> images)
        {
            this.context = context;
            this.users = users;
            this.images = images;
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            this.roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        public IQueryable<string> GetRoles()
        {
            var result = roleManager.Roles.Select(x => x.Name);            
            return result.AsQueryable();
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public User GetById(string id)
        {
            return this.users.GetById(id);
        }

        public void Add(User user, string role)
        {                        
            user.UserName = user.Email;
            this.userManager.Create(user, "changeme");
            this.users.Add(user);
            user.IntId = user.Id.GetHashCode();
            this.users.Update(user);
            this.userManager.AddToRole(user.Id, role);            
        }

        public void Update(User user)
        {
            this.users.Update(user);
        }

        public string GetRoleName(User user)
        {
            var result = this.userManager.GetRoles(user.Id).FirstOrDefault();
            return result;
        }

        public void SetRoleName(User user, string name)
        {
            // todo: remove try catch
            try
            {
                this.userManager.RemoveFromRole(user.Id, GetRoleName(user));

            }
            catch (Exception)
            {

            }
            this.userManager.AddToRole(user.Id, name);
            this.users.Update(user);
        }
    }
}
