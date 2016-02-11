﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
﻿using AutoMapper;
﻿using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
﻿using Planex.Data;
﻿using Planex.Data.Models;
﻿using Planex.Web.Areas.HR.Models;
using AutoMapper.QueryableExtensions;
﻿using Planex.Services.Skills;
﻿using Planex.Services.Users;
﻿using Planex.Web.Infrastructure.Mappings;
﻿using WebGrease.Css.Extensions;

namespace Planex.Web.Areas.HR.Controllers
{
    public class UsersController : BaseController
    {
        protected IUserService userService;
        protected ISkillService skillService;

        public UsersController(IUserService userService, ISkillService skillService)
        {
            this.userService = userService;
            this.skillService = skillService;
        }

        public ActionResult Index()
        {
            ViewData["roles"] = new List<SelectListItem>();            
            var roles = this.userService.GetRoles();

            foreach (var role in roles)
            {
                (ViewData["roles"] as List<SelectListItem>).Add(
                    new SelectListItem()
                    {
                        Value = role,
                        Text = role
                    });
            }

            return View();
        }

        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.userService.GetAll().To<UserViewModel>();
            return Json(users.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Create([DataSourceRequest]DataSourceRequest request, UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var entity = new User
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PricePerHour = 0,
                };

                this.userService.Add(entity, user.Role);                
                user.Id = entity.Id;
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }
    }
}
