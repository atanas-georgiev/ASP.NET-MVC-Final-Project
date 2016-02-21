namespace Planex.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return this.RedirectToAction("Error");
        }

        public ActionResult NotFound()
        {
            return this.View();
        }

        public ActionResult Error()
        {
            return this.View();
        }
    }
}