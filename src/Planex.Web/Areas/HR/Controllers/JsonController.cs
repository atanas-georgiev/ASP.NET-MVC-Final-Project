namespace Planex.Web.Areas.HR.Controllers
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Mvc;
    using System.Web.Security;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Services.Skills;
    using Planex.Services.Users;
    using Planex.Web.Areas.HR.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class JsonController : BaseController
    {
        private readonly ISkillService skillService;

        public JsonController(IUserService userService, ISkillService skillService)
            : base(userService)
        {
            this.skillService = skillService;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SkillsCreate([DataSourceRequest] DataSourceRequest request, SkillViewModel skill)
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
        public ActionResult SkillsDestroy([DataSourceRequest] DataSourceRequest request, SkillViewModel skill)
        {
            if (this.ModelState.IsValid)
            {
                this.skillService.Delete(skill.Id);
            }

            return this.Json(new[] { skill }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult SkillsRead([DataSourceRequest] DataSourceRequest request)
        {
            var skills = this.skillService.GetAll().To<SkillViewModel>();
            return this.Json(skills.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SkillsUpdate([DataSourceRequest] DataSourceRequest request, SkillViewModel skill)
        {
            if (this.ModelState.IsValid)
            {
                this.skillService.UpdateName(skill.Id, skill.Name);
            }

            return this.Json(new[] { skill }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UsersCreate([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new User
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Salary = 0,                    
                };

                this.UserService.Add(entity, user.RoleId);
                user.Id = entity.Id;
            }

            var resultData = new[] { user };
            return Json(resultData.AsQueryable().ToDataSourceResult(request, ModelState));
        }

        public ActionResult UsersRead([DataSourceRequest] DataSourceRequest request)
        {
            var users = this.UserService.GetAll().Where(x => x.FirstName != "System" && x.LastName != "Message").To<UserViewModel>().ToList();
            
            foreach (var user in users)
            {
                 user.Role = this.UserService.GetRoleNameById(user.RoleId);
            }

            return this.Json(users.ToDataSourceResult(request));
        }

        public ActionResult GetAllRoles([DataSourceRequest] DataSourceRequest request)
        {
            var roles = this.UserService.GetRoles();            
            return this.Json(roles, JsonRequestBehavior.AllowGet);
        }
    }
}