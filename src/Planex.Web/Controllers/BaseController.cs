namespace Planex.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Services.Users;

    public abstract class BaseController : Controller
    {
        protected readonly IUserService userService;

        protected BaseController(IUserService userService)
        {
            this.userService = userService;
        }

        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(
            RequestContext requestContext, 
            AsyncCallback callback, 
            object state)
        {
            this.UserProfile =
                this.userService.GetAll()
                    .FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}