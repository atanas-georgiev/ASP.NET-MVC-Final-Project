﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Web.Areas.Lead.Models;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class IndexController : Controller
    {
      
       // private GanttTaskService taskService;
       // private GanttDependencyService dependencyService;
       // private GanttAssignmentService assignmentService;
       // private GanttResourceService resourceService;

        public IndexController()
        {
         //   taskService = new GanttTaskService();
         //   dependencyService = new GanttDependencyService();
         //   assignmentService = new GanttAssignmentService();
         //   resourceService = new GanttResourceService();
        }

        protected override void Dispose(bool disposing)
        {
//            taskService.Dispose();
//            dependencyService.Dispose();

            base.Dispose(disposing);
        }

        public virtual ActionResult Index()
        {

            var result = new List<TaskViewModel>()
            {
                new TaskViewModel()
                {
                    TaskID = 1,
                    Title = "My title",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(3),
                    ParentID = null,
                    PercentComplete = 5,
                    OrderId = 1,
                    Expanded = true,
                    Summary = true
                }
            };

            ViewData["tasks"] = result.AsQueryable();
            ViewData["dependencies"] = new List<DependencyViewModel>().AsQueryable();

            return View();
        }

//        public virtual JsonResult DestroyTask([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
//        {
//            if (ModelState.IsValid)
//            {
//                taskService.Delete(task, ModelState);
//            }
//
//            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
//        }

//        public virtual JsonResult CreateTask([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
//        {
//            if (ModelState.IsValid)
//            {
//                taskService.Insert(task, ModelState);
//            }
//
//            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
//        }

//        public virtual JsonResult UpdateTask([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
//        {
//            if (ModelState.IsValid)
//            {
//                taskService.Update(task, ModelState);
//            }
//
//            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
//        }

        public virtual JsonResult ReadDependencies([DataSourceRequest] DataSourceRequest request)
        {

            var result = new List<TaskViewModel>();

            return Json(result.AsQueryable().ToDataSourceResult(request));
        }

//        public virtual JsonResult DestroyDependency([DataSourceRequest] DataSourceRequest request, DependencyViewModel dependency)
//        {
//            if (ModelState.IsValid)
//            {
//                dependencyService.Delete(dependency, ModelState);
//            }
//
//            return Json(new[] { dependency }.ToDataSourceResult(request, ModelState));
//        }
//
//        public virtual JsonResult CreateDependency([DataSourceRequest] DataSourceRequest request, DependencyViewModel dependency)
//        {
//            if (ModelState.IsValid)
//            {
//                dependencyService.Insert(dependency, ModelState);
//            }
//
//            return Json(new[] { dependency }.ToDataSourceResult(request, ModelState));
//        }
//
//        public virtual JsonResult UpdateDependency([DataSourceRequest] DataSourceRequest request, DependencyViewModel dependency)
//        {
//            if (ModelState.IsValid)
//            {
//                dependencyService.Update(dependency, ModelState);
//            }
//
//            return Json(new[] { dependency }.ToDataSourceResult(request, ModelState));
//        }
    }
}