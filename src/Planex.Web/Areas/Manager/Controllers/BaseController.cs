namespace Planex.Web.Areas.Manager.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Planex.Data.Models;
    using Planex.Services.Users;

    public class BaseController : Controller
    {
        public BaseController(IUserService userService)
        {
            this.UserService = userService;
        }

        protected User UserProfile { get; private set; }

        protected IUserService UserService { get; set; }

        protected override IAsyncResult BeginExecute(
            RequestContext requestContext, 
            AsyncCallback callback, 
            object state)
        {
            this.UserProfile =
                this.UserService.GetAll()
                    .FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}