namespace Planex.Web.Areas.Lead.Controllers
{
    using System.Web.Mvc;

    public class ProjectsController : Controller
    {
        public ActionResult Edit(string id)
        {
            this.Session["ProjectId"] = id;

            return this.View();
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}