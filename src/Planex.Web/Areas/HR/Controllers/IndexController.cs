namespace Planex.Web.Areas.HR.Controllers
{
    using Planex.Services.Users;

    public class IndexController : BaseController
    {
        public IndexController(IUserService userService)
            : base(userService)
        {
        }
    }
}