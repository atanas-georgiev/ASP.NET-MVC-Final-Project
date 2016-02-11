﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
﻿using Planex.Data;
﻿using Planex.Data.Models;
﻿using Planex.Web.Areas.HR.Models;
using AutoMapper.QueryableExtensions;
﻿using Planex.Services.Users;
﻿using WebGrease.Css.Extensions;

namespace Planex.Web.Areas.HR.Controllers
{
    public class UsersController : Controller
    {
        IUserService userService = new UserService();
        
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
            DataSourceResult result = this.userService.GetAll().ProjectTo<UserViewModel>().ToDataSourceResult(request);                
            return Json(result);
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Update([DataSourceRequest]DataSourceRequest request, UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var entity = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
//                    PricePerHour = user.PricePerHour,
//                    Skills = user.Skills
                };

          //      db.Users.Attach(entity);
         //       db.Entry(entity).State = EntityState.Modified;
        //        db.SaveChanges();
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var entity = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
//                    PricePerHour = user.PricePerHour,
//                    Skills = user.Skills
                };

//                db.Users.Attach(entity);
//                db.Users.Remove(entity);
//                db.SaveChanges();
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
           // db.Dispose();
            base.Dispose(disposing);
        }
    }
}
