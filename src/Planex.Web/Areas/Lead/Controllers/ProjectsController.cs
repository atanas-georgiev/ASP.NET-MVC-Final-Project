namespace Planex.Web.Areas.Lead.Controllers
{
    using System.Web.Mvc;

    public class ProjectsController : Controller
    {
        public ActionResult Details(string id)
        {
            this.Session["ProjectId"] = id;

            // var intId = int.Parse(id);
            // var result = taskService.GetAll().Where(x => x.Id == intId).To<ProjectDetailsViewModel>().FirstOrDefault();
            return this.View();
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}