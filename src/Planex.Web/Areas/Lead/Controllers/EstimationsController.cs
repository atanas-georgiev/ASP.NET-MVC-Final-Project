using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models;
using Planex.Web.Infrastructure.Mappings;
using AutoMapper.QueryableExtensions;
using Planex.Common;
using Planex.Services.Skills;
using Planex.Services.SubTasks;
using Planex.Web.Areas.Lead.Models.SubTask;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class EstimationsController : BaseController
    {
        private readonly ITaskService taskService;        
     //   private readonly IUserService userService;
        private readonly ISubTaskService subTaskService;

        public EstimationsController(IUserService userService, ITaskService taskService, ISkillService skillService, ISubTaskService subTaskService)
            : base(userService)
        {
            this.taskService = taskService;            
        //    this.userService = userService;
            this.subTaskService = subTaskService;
        }


        public ActionResult Projects_Read([DataSourceRequest] DataSourceRequest request)
        {
            var requestedEstimationTasks = taskService.GetAll().To<EstimationRequestedViewModel>();
            return Json(requestedEstimationTasks.ToDataSourceResult(request));
        }

        public ActionResult Requested()
        {
            var requestedEstimationTasks = taskService.GetAll().To<EstimationRequestedViewModel>();
            return View(requestedEstimationTasks);
        }

        public ActionResult Details(string id)
        {
            var intId = int.Parse(id);
            var requestedEstimationTask = taskService.GetAll().Where(x => x.Id == intId).To<EstimationRequestedViewModel>().FirstOrDefault();
            return View(requestedEstimationTask);
        }

        public ActionResult StartEstimation(string id)
        {
            var intId = int.Parse(id);
            taskService.StartEstimation(intId, UserProfileId);
            return RedirectToAction("Edit", new { id = intId } );
        }

        public ActionResult Index(string id)
        {
            var requestedEstimationTasks = taskService.GetAll().Where(x => x.LeadId == UserProfileId).To<EstimationHomeViewModel>();
            return View(requestedEstimationTasks);
        }

        public ActionResult Estimations_Read([DataSourceRequest]DataSourceRequest request)
        {
            var tasks = taskService.GetAll().Where(x => x.LeadId == UserProfileId).To<EstimationHomeViewModel>();
            return Json(tasks.ToDataSourceResult(request));
        }

        public ActionResult Edit(string id)
        {
            Session["mainTaskId"] = id;
            var intId = int.Parse(id);
            var requestedEstimationTask = taskService.GetAll().Where(x => x.Id == intId).To<EstimationEditViewModel>().FirstOrDefault();
            var requestedEstimationTaskSubTasks = subTaskService.GetAll().Where(x => x.MainTaskId == requestedEstimationTask.Id).OrderByDescending(x => x.End);

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

            return View(requestedEstimationTask);
        }

        public ActionResult CreateSubTask()
        {
            return PartialView("_SubTaskAdd", new EstimationEditViewModelSubTask());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubTaskAdd(SubTaskViewModel subtask)
        {
            var parentTask = taskService.GetById(int.Parse(Session["mainTaskId"].ToString()));

            if (ModelState.IsValid)
            {
                int duration;
                int? skillId;

                if (subtask.Duration == null || subtask.SelectedUsers.Count == 0)
                {
                    duration = 0;
                }
                else
                {
                    duration = subtask.Duration.Value/subtask.SelectedUsers.Count;
                }

                if (subtask.SelectedSkill == null)
                {
                    skillId = null;
                }
                else
                {
                    skillId = int.Parse(subtask.SelectedSkill);
                }

                var subTaskDB = new Subtask()
                {
                    Title = subtask.Title,
                    Description = subtask.Description,
                    Duration = duration,
                    MainTaskId = int.Parse(Session["mainTaskId"].ToString()),
                    ParentId = subtask.ParentId,
                    DependencyId = subtask.DependencyId,
                    SkillId = skillId,
                    Start = DateTime.UtcNow,
                    End = DateTime.UtcNow,
                    Price = 0
                };

                if (subtask.SelectedUsers != null)
                {
                    foreach (var user in subtask.SelectedUsers)
                    {
                        var dbuser = userService.GetById(user);
                        subTaskDB.Users.Add(dbuser);
                        subTaskDB.Price += dbuser.PricePerHour / UserConstants.WorkingDays * subTaskDB.Duration;
                    }

                    if (subTaskDB.DependencyId != null)
                    {
                        var depSubTask = subTaskService.GetById((int)subTaskDB.DependencyId);
                        subTaskDB.Start = depSubTask.End.AddDays(1);
                        subTaskDB.End = subTaskDB.Start.AddDays(subTaskDB.Duration);
                    }
                    else if (subTaskDB.ParentId == null)
                    {
                        subTaskDB.Start = parentTask.Start;
                        subTaskDB.End = subTaskDB.Start.AddDays(subTaskDB.Duration);
                    }
                    else
                    {
                        var parentSubTask = subTaskService.GetById((int) subTaskDB.ParentId);
                        subTaskDB.Start = parentSubTask.End.AddDays(1);
                        subTaskDB.End = subTaskDB.Start.AddDays(subTaskDB.Duration);                        
                    }
                }

                subTaskService.Add(subTaskDB);
                subTaskService.AddAttachments(subTaskDB, subtask.UploadedAttachments, System.Web.HttpContext.Current.Server);

                // Sub task date and price
                if (subTaskDB.ParentId != null)
                {
                    var parentSubTask = subTaskService.GetById((int)subTaskDB.ParentId);
                    var parentChildrenByStartDate = subTaskService.GetAll().Where(x => x.ParentId == parentSubTask.Id).OrderBy(x => x.Start);
                    var parentChildrenByEndDate = subTaskService.GetAll().Where(x => x.ParentId == parentSubTask.Id).OrderByDescending(x => x.End);

                    parentSubTask.Start = parentChildrenByStartDate.First().Start;
                    parentSubTask.End = parentChildrenByEndDate.First().End;
                    parentSubTask.Price = parentChildrenByStartDate.Sum(x => x.Price);

                    subTaskService.Update(parentSubTask);
                }

                return Content("Done!");
            }

            return Content("Error!");
        }

        public ActionResult EditSubTask(string id)
        {
            var intId = int.Parse(id);
            var result = subTaskService.GetAll().Where(x => x.Id == intId).To<EstimationEditViewModelSubTask>().FirstOrDefault();
            return PartialView("_SubTaskEdit", result);
        }

        public ActionResult SendApproval()
        {
            var task = taskService.GetById(int.Parse(Session["mainTaskId"].ToString()));
            task.State = TaskStateType.Estimated;
            taskService.Update(task);
            return RedirectToAction("Index");
        }

    }
}