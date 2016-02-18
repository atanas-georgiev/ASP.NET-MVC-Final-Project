namespace Planex.Web.Areas.HR.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Services.Skills;
    using Planex.Services.Users;
    using Planex.Web.Areas.HR.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class SkillsController : BaseController
    {
        protected ISkillService skillService;

        protected IUserService userService;

        public SkillsController(IUserService userService, ISkillService skillService)
        {
            this.userService = userService;
            this.skillService = skillService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Skills_Create([DataSourceRequest] DataSourceRequest request, SkillViewModel skill)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new Skill { Name = skill.Name };

                this.skillService.Add(entity);
                skill.Id = entity.Id;
            }

            return this.Json(new[] { skill }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Skills_Destroy([DataSourceRequest] DataSourceRequest request, SkillViewModel skill)
        {
            if (this.ModelState.IsValid)
            {
                this.skillService.Delete(skill.Id);
            }

            return this.Json(new[] { skill }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult Skills_Read([DataSourceRequest] DataSourceRequest request)
        {
            var skills = this.skillService.GetAll().To<SkillViewModel>();
            return this.Json(skills.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Skills_Update([DataSourceRequest] DataSourceRequest request, SkillViewModel skill)
        {
            if (this.ModelState.IsValid)
            {
                this.skillService.UpdateName(skill.Id, skill.Name);
            }

            return this.Json(new[] { skill }.ToDataSourceResult(request, this.ModelState));
        }
    }
}