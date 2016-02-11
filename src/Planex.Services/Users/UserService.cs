using System;
using System.Collections.Generic;
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
        private PlanexDbContext context;
        private IRepository<User> users;
        private UserManager<User> userManager;

        public UserService()
        {
            this.context = new PlanexDbContext();
            this.users = new GenericRepository<User>(context);
            this.userManager = new UserManager<User>(new UserStore<User>(context));
        }

        public IQueryable<string> GetRoles()
        {
            var result = new List<string>();
            result.Add("Manager");
            result.Add("HR");
            result.Add("Lead");
            result.Add("Worker");

            return result.AsQueryable();
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public void Add(User user, string role)
        {                        
            user.UserName = user.Email;
            this.userManager.Create(user, string.Empty);
            this.users.Add(user);
            this.userManager.AddToRole(user.Id, role);            
        }
    }
}
