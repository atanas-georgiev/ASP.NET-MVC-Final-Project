using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planex.Data.Models;
using Planex.Services.Skills;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Manager.Models;

namespace Planex.Web.Areas.Manager.Controllers
{
    public class TasksController : BaseController
    {
        private ITaskService taskService;

        public TasksController(IUserService userService, ITaskService taskService)
            : base(userService)
        {
            this.taskService = taskService;
        }

        public ActionResult Index()
        {
            return Content("Home");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbTask = new MainTask()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Manager = UserProfile,
                    Priority = model.Priority,
                    State = TaskStateType.Draft,
                    Start = model.Start
                };

                taskService.Add(dbTask);
                taskService.AddAttachments(dbTask, model.UploadedAttachments, System.Web.HttpContext.Current.Server);

                return RedirectToAction("Index");
            }

            return Create(model);
        }
    }
}