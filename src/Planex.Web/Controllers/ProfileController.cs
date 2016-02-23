using Planex.Web.App_LocalResources;
using Planex.Web.Infrastructure.Extensions;
using Planex.Web.Infrastructure.Notifications.Toastr;

namespace Planex.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Services.Images;
    using Planex.Services.Skills;
    using Planex.Services.Users;
    using Planex.Web.Models.Profile;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IImageService imageService;

        private readonly ISkillService skillService;

        public ProfileController(IUserService userService, ISkillService skillService, IImageService imageService)
            : base(userService)
        {
            this.skillService = skillService;
            this.imageService = imageService;
        }

        public ActionResult Image(int id)
        {
            var image = this.imageService.GetById(id);

            if (image == null)
            {
                return this.Content(string.Empty);
            }

            return this.File(image.Content, "image/" + image.FileExtension);
        }

        public ActionResult Index()
        {
            var skills = this.skillService.GetAll().Select(s => s.Name).ToList();
            this.ViewData["skills"] = skills;

            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<UserViewModel>(this.UserProfile);
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.UserService.GetById(this.UserProfile.Id);
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Theme = user.Theme;
                entity.Skills.Clear();

                if (user.Skills != null)
                {
                    foreach (string skill in user.Skills)
                    {
                        var dbskill = this.skillService.GetAll().FirstOrDefault(x => x.Name == skill);
                        entity.Skills.Add(dbskill);
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

                this.UserService.Update(entity);
                this.AddToastMessage("", NotificationMessages.ProfileUpdated, ToastType.Success);
                return this.RedirectToAction("Index");
            }

            return this.View(user);
        }
    }
}