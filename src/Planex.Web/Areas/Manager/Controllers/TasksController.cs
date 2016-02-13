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
        private IUserService userService;
        private ITaskService taskService;

        public TasksController(IUserService userService, ITaskService taskService)
            : base(userService)
        {
            this.userService = userService;
            this.taskService = taskService;
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
                    State = TaskStateType.Draft
                };

                taskService.Add(dbTask);
                taskService.AddAttachments(dbTask, model.UploadedAttachments, System.Web.HttpContext.Current.Server);


                //                var entity = userService.GetById(user.Id);
                //                entity.FirstName = user.FirstName;
                //                entity.LastName = user.LastName;
                //                entity.PricePerHour = user.PricePerHour;
                //                entity.Skills.Clear();
                //
                //                if (user.Skills != null)
                //                {
                //                    foreach (string skill in user.Skills)
                //                    {
                //                        var dbSkill = skillService.GetAll().FirstOrDefault(x => x.Name == skill);
                //                        entity.Skills.Add(dbSkill);
                //                    }
                //                }
                //
                //                if (user.UploadedImage != null)
                //                {
                //                    using (var memory = new MemoryStream())
                //                    {
                //                        user.UploadedImage.InputStream.CopyTo(memory);
                //                        var content = memory.GetBuffer();
                //
                //                        entity.Image = new Image
                //                        {
                //                            Content = content,
                //                            FileExtension = user.UploadedImage.FileName.Split(new[] { '.' }).Last()
                //                        };
                //                    }
                //                }
                //
                //                userService.Update(entity);
                //                userService.SetRoleName(entity, user.Role);
            }

            return RedirectToAction("Index");
        }
    }
}