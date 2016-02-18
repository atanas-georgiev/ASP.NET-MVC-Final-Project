namespace Planex.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Services.Cache;
    using Planex.Services.Messages;
    using Planex.Services.Users;
    using Planex.Web.Models.Home;

    public class HomeController : BaseController
    {
        private readonly IMessageService messageService;        

        public HomeController(IUserService userService, IMessageService messageService)
            : base(userService)
        {
            this.messageService = messageService;
        }

        public ActionResult Index()
        {
            var homeModel = new HomeViewModel();

            if (this.HttpContext.User.Identity.IsAuthenticated)
            {                
                homeModel.Messages = new MessageHomeViewModel();
                homeModel.Messages.UnreadMessagesCount =
                this.messageService.GetAll().Where(x => x.To.Id == this.UserProfile.Id).Count(x => x.IsRead == false);
                
            }

            return this.View(homeModel);
        }
    }
}