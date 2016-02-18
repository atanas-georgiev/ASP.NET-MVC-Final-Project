namespace Planex.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Services.Cache;
    using Planex.Services.Messages;
    using Planex.Services.Projects;
    using Planex.Services.Skills;
    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Infrastructure.Mappings;
    using Planex.Web.Models.Home;
    using Planex.Web.Models.Messages;

    public class JsonDataController : BaseController
    {
        private readonly IMessageService messageService;

        private readonly IProjectService projectService;

        private readonly ISkillService skillService;

        private readonly ITaskService subTaskService;

        private readonly ICacheService cacheService;

        public JsonDataController(
            IUserService userService,
            ISkillService skillService,
            ITaskService subTaskService,
            IProjectService projectService,
            IMessageService messageService,
            ICacheService cacheService)
            : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.projectService = projectService;
            this.messageService = messageService;
            this.cacheService = cacheService;
        }

        public ActionResult GetAllUsers([DataSourceRequest] DataSourceRequest request)
        {
            var users =
                this.userService.GetAll()
                    .Where(x => x.Id != this.UserProfile.Id)
                    .Select(x => new { id = x.Id, name = x.FirstName + " " + x.LastName });
            return this.Json(users, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ReadMessages([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.messageService.GetAll()
                .Where(x => x.To.Id == this.UserProfile.Id)
                .To<MessageViewModel>();
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ReadUnreadMessages([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.messageService.GetAll()
                .Where(x => x.To.Id == this.UserProfile.Id && x.IsRead == false)
                .OrderByDescending(x => x.Date)
                .To<MessageViewModel>();
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ReadAllProjects([DataSourceRequest] DataSourceRequest request)
        {
            var projectsPublic =
                            this.cacheService.Get(
                                "categories",
                                () =>
                                    {
                                        var projects = this.projectService.GetAll().Where(x => x.State >= TaskStateType.Started);
                                        var result = this.subTaskService.GetAll().Where(x => x.Project.State >= TaskStateType.Started).To<ProjectHomeViewModel>().ToList();

                                        foreach (var project in projects)
                                        {
                                            result.Add(
                                                new ProjectHomeViewModel()
                                                {
                                                    Title = project.Title,
                                                    Start = project.Start,
                                                    End = project.End,
                                                    HasChildren = project.Subtasks.Any(),
                                                    ParentId = null,
                                                    Id = project.Id + 100000,
                                                    PercentComplete = project.PercentComplete * 100
                                                });
                                        }

                                        return result;
                                    },
                                10 * 60);           

            return this.Json(projectsPublic.AsQueryable().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}