using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            Session["mainTaskId"] = id;
//            var intId = int.Parse(id);
//            var result = taskService.GetAll().Where(x => x.Id == intId).To<ProjectDetailsViewModel>().FirstOrDefault();
            return View();
        }
    }
}