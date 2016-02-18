namespace Planex.Web.Areas.HR.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Services.Images;
    using Planex.Services.Skills;
    using Planex.Services.Users;
    using Planex.Web.Areas.HR.Models;

    public class UserDetailsController : BaseController
    {
        protected IImageService imageService;

        protected ISkillService skillService;

        protected IUserService userService;

        public UserDetailsController(IUserService userService, ISkillService skillService, IImageService imageService)
        {
            this.userService = userService;
            this.skillService = skillService;
            this.imageService = imageService;
        }

        public ActionResult Image(int id)
        {
            var image = this.imageService.GetById(id);
            if (image == null)
            {
                return this.Content(string.Empty);

                // throw new HttpException(404, "Image not found");
            }

            return this.File(image.Content, "image/" + image.FileExtension);
        }

        public ActionResult Index(string id)
        {
            var skills = this.skillService.GetAll().Select(s => s.Name).ToList();
            this.ViewData["skills"] = skills;

            this.ViewData["roles"] = new List<SelectListItem>();
            var roles = this.userService.GetRoles();

            foreach (var role in roles)
            {
                (this.ViewData["roles"] as List<SelectListItem>).Add(new SelectListItem() { Value = role, Text = role });
            }

            var dataModel = this.userService.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<UserEditViewModel>(dataModel);
            this.ViewData["SelectedIndex"] = roles.ToList().IndexOf(this.userService.GetRoleName(dataModel)) + 1;

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserEditViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.userService.GetById(user.Id);
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Salary = user.PricePerHour;
                entity.Skills.Clear();

                if (user.Skills != null)
                {
                    foreach (string skill in user.Skills)
                    {
                        var dbSkill = this.skillService.GetAll().FirstOrDefault(x => x.Name == skill);
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
                                               FileExtension =
                                                   user.UploadedImage.FileName.Split(new[] { '.' }).Last()
                                           };
                    }
                }

                this.userService.Update(entity);
                this.userService.SetRoleName(entity, user.Role);
            }

            return this.RedirectToAction("Index");
        }
    }
}