namespace Planex.Web.Areas.HR.Controllers
{
    using System.Web.Mvc;

    using Planex.Services.Users;

    public class SkillsController : BaseController
    {
        public SkillsController(IUserService userService)
            : base(userService)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}