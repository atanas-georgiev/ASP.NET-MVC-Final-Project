namespace Planex.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;

    public class IndexController : Controller
    {
        public ActionResult Index()
        {
            return this.RedirectToAction("Index", "Projects");
        }
    }
}