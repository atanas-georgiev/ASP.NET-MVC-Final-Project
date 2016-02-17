using System.Web.Mvc;

namespace Planex.Web.Areas.Manager.Controllers
{
    public class IndexController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Projects");
        }
    }
}