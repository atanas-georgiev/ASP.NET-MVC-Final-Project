namespace Planex.Web.Areas.HR.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Services.Skills;
    using Planex.Services.Users;
    using Planex.Web.Areas.HR.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class UsersController : BaseController
    {
        protected ISkillService skillService;

        protected IUserService userService;

        public UsersController(IUserService userService, ISkillService skillService)
        {
            this.userService = userService;
            this.skillService = skillService;
        }

        public ActionResult Index()
        {
            this.ViewData["roles"] = new List<SelectListItem>();
            var roles = this.userService.GetRoles();

            foreach (var role in roles)
            {
                (this.ViewData["roles"] as List<SelectListItem>).Add(new SelectListItem() { Value = role, Text = role });
            }

            this.ViewData["SelectedIndex"] = 0;

            return this.View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Create([DataSourceRequest] DataSourceRequest request, UserViewModel user)
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

                this.userService.Add(entity, user.Role);
                user.Id = entity.Id;
            }

            return this.Json(new[] { user }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult Users_Read([DataSourceRequest] DataSourceRequest request)
        {
            var users = this.userService.GetAll().To<UserViewModel>();
            return this.Json(users.ToDataSourceResult(request));
        }
    }
}