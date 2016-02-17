using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Planex.Data.Models;
using Planex.Services.Users;

namespace Planex.Web.Areas.Lead.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IUserService userService;

        protected User UserProfile { get; private set; }

        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();

        protected BaseController(IUserService userService)
        {
            this.userService = userService;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            UserProfile = userService.GetAll().FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);               
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}