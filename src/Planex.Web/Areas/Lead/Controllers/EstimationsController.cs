using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models;
using Planex.Web.Infrastructure.Mappings;
using AutoMapper.QueryableExtensions;
using Planex.Common;
using Planex.Services.Projects;
using Planex.Services.Skills;
using Planex.Services.Tasks;
using Planex.Web.Areas.Lead.Models.Estimation;
using Vereyon.Web;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class EstimationsController : BaseController
    {
        private readonly IProjectService taskService;   
        private readonly ITaskService subTaskService;

        public EstimationsController(IUserService userService, IProjectService taskService, ISkillService skillService, ITaskService subTaskService)
            : base(userService)
        {
            this.taskService = taskService;      
            this.subTaskService = subTaskService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            Session["ProjectId"] = id;
            var intId = int.Parse(id);
            var requestedEstimationTask = taskService.GetAll().Where(x => x.Id == intId).To<EstimationEditViewModel>().FirstOrDefault();
            var requestedEstimationTaskSubTasks = subTaskService.GetAll().Where(x => x.ProjectId == requestedEstimationTask.Id).OrderByDescending(x => x.End);

            if (requestedEstimationTaskSubTasks.ToList().Count != 0)
            {
                requestedEstimationTask.End = requestedEstimationTaskSubTasks.First().End;
                requestedEstimationTask.Price = requestedEstimationTaskSubTasks.Where(x => x.ParentId == null).Sum(x => x.Price); 
            }
            else
            {
                requestedEstimationTask.End = requestedEstimationTask.Start;
                requestedEstimationTask.Price = 0;
            }

            var sanitizer = HtmlSanitizer.SimpleHtml5DocumentSanitizer();
            requestedEstimationTask.Description = sanitizer.Sanitize(requestedEstimationTask.Description);

            return View(requestedEstimationTask);
        }

        public ActionResult SendForApproval()
        {
            var task = taskService.GetById(int.Parse(Session["ProjectId"].ToString()));
            task.State = TaskStateType.Estimated;
            taskService.Update(task);
            return RedirectToAction("Index");
        }

    }
}