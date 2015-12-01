using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planex.Web.Areas.Manager.Controllers
{
    public class IndexController : Controller
    {
        // GET: Manager/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}