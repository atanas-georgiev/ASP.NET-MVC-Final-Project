using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Planex.Data.Models;
using Planex.Services.Users;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService userService;

        public BaseController(IUserService userService)
        {
            this.userService = userService;
        }

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            UserProfile =
                userService.GetAll().FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}