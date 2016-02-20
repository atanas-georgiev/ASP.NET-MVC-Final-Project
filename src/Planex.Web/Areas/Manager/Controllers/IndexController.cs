namespace Planex.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;

    using Planex.Services.Users;

    public class IndexController : BaseController
    {
        public IndexController(IUserService userService)
            : base(userService)
        {
        }

        public ActionResult Index()
        {
            return this.RedirectToAction("Index", "Projects");
        }
    }
}