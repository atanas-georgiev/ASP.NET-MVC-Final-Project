using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Planex.Data.Models;
using Planex.Services.Images;
using Planex.Services.Skills;
using Planex.Services.Users;
using Planex.Web.Areas.HR.Models;

namespace Planex.Web.Areas.HR.Controllers
{
    public class UserDetailsController : BaseController
    {

        protected IUserService userService;
        protected ISkillService skillService;
        protected IImageService imageService;

        public UserDetailsController(IUserService userService, ISkillService skillService, IImageService imageService)
        {
            this.userService = userService;
            this.skillService = skillService;
            this.imageService = imageService;
        }

        public ActionResult Index(string id)
        {

            var skills = skillService.GetAll().Select(s => s.Name).ToList();
            ViewData["skills"] = skills;

            var dataModel = userService.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<UserEditViewModel>(dataModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

                if (user.Skills != null)
                {
                    foreach (string skill in user.Skills)
                    {
                        var dbSkill = skillService.GetAll().FirstOrDefault(x => x.Name == skill);
                        entity.Skills.Add(dbSkill);
                    }
                }

                if (user.UploadedImage != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        user.UploadedImage.InputStream.CopyTo(memory);
                        var content = memory.GetBuffer();

                        entity.Image = new Image
                        {
                            Content = content,
                            FileExtension = user.UploadedImage.FileName.Split(new[] { '.' }).Last()
                        };
                    }
                }

                userService.Update(entity);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Image(int id)
        {
            var image = this.imageService.GetById(id);
            if (image == null)
            {
                return Content("");
               // throw new HttpException(404, "Image not found");
            }            

            return File(image.Content, "image/" + image.FileExtension);
        }
    }
}