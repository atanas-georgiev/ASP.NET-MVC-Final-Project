using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Planex.Data.Models;
using Planex.Services.Skills;
using Planex.Services.Users;
using Planex.Web.App_Start;
using Planex.Web.Areas.HR.Models;

namespace Planex.Web.Areas.HR.Controllers
{
    public class UserDetailsController : Controller
    {
        IUserService userService = new UserService();
        ISkillService skillService = new SkillService();

        public ActionResult Index(string id)
        {

            var skills = skillService.GetAll().Select(s => s.Name).ToList();
            ViewData["skills"] = skills;

            var dataModel = userService.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<UserEditViewModel>(dataModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(UserEditViewModel user)
        {
            if (ModelState.IsValid)
            {
                var entity = userService.GetById(user.Id);
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.PricePerHour = user.PricePerHour;
                // todo: avatar
                entity.Skills.Clear();

                foreach (string skill in user.Skills)
                {
                    var dbSkill = skillService.GetAll().FirstOrDefault(x => x.Name == skill);
                    entity.Skills.Add(dbSkill);
                }

                userService.Update(entity);
            }

            return RedirectToAction("Index");
        }
    }
}