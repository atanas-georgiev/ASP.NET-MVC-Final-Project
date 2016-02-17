using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Planex.Data.Models;
using Planex.Services.Users;

namespace Planex.Web.Areas.Worker.Controllers
{
    public class BaseController : Controller
    {
        protected IUserService userService;

        public BaseController(IUserService userService)
        {
            this.userService = userService;
        }

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.UserProfile = this.userService.GetAll().FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}