using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Worker.Models.Assignments;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Worker.Controllers
{
    public class AssignmentsController : BaseController
    {
        private readonly ITaskService subTaskService;

        public AssignmentsController(IUserService userService, ITaskService subTaskService) : base(userService)
        {
            this.subTaskService = subTaskService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            var intId = int.Parse(id);
            var task = subTaskService.GetAll().Where(x => x.Id == intId).To<AssignmentViewModel>().FirstOrDefault();
            return View(task);
        }
    }
}