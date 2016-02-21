namespace Planex.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Planex.Data.Models;
    using Planex.Services.Users;

    public abstract class BaseController : Controller
    {
        protected BaseController(IUserService userService)
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

            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (this.UserProfile != null)
                {
                    this.ViewBag.Theme = this.UserProfile.Theme;
                }
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}