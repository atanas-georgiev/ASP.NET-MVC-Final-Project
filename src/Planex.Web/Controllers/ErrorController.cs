using Planex.Services.Users;

namespace Planex.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : BaseController
    {
        public ErrorController(IUserService userService) : base(userService)
        {
        }

        public ActionResult Error()
        {
            return this.View();
        }

        public ActionResult Index()
        {
            return this.RedirectToAction("Error");
        }

        public ActionResult NotFound()
        {
            return this.View();
        }
    }
}