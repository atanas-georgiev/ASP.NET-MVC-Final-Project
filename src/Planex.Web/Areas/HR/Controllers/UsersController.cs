namespace Planex.Web.Areas.HR.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Services.Images;
    using Planex.Services.Skills;
    using Planex.Services.Users;
    using Planex.Web.Areas.HR.Models;

    public class UsersController : BaseController
    {
        private readonly IImageService imageService;

        private readonly ISkillService skillService;

        public UsersController(IUserService userService, ISkillService skillService, IImageService imageService)
            : base(userService)
        {
            this.skillService = skillService;
            this.imageService = imageService;
        }

        public ActionResult Details(string id)
        {
            var skills = this.skillService.GetAll().Select(s => s.Name).ToList();
            this.ViewData["skills"] = skills;

            var dataModel = this.UserService.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<UserEditViewModel>(dataModel);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(UserEditViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.UserService.GetById(user.Id);
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Salary = user.Salary;
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

                this.UserService.Update(entity);
                this.UserService.SetRoleName(entity, user.RoleId);
                return this.RedirectToAction("Index");
            }

            return this.View(user);
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
            return this.View();
        }
    }
}