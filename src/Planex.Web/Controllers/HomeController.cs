namespace Planex.Web.Controllers
{
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Planex.Services.Cache;
    using Planex.Services.Messages;
    using Planex.Services.Users;
    using Planex.Web.Infrastructure.Scheduler;
    using Planex.Web.Models.Home;

    public class HomeController : BaseController
    {
        private readonly IMessageService messageService;

        private readonly IPlanexScheduler scheduler;

        private readonly ICacheService cacheService;

        public HomeController(IUserService userService, IMessageService messageService, IPlanexScheduler scheduler, ICacheService cacheService)
            : base(userService)
        {
            this.messageService = messageService;
            this.scheduler = scheduler;
            this.cacheService = cacheService;
        }

        public ActionResult Index()
        {
            var homeModel = new HomeViewModel();

            this.cacheService.Get("systemMessagesCache", () => { this.scheduler.Schedule(); return 0; }, 10 * 60);            

            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                homeModel.Messages = new MessageHomeViewModel();
                homeModel.Messages.UnreadMessagesCount =
                    this.messageService.GetAll()
                        .Where(x => x.To.Id == this.UserProfile.Id)
                        .Count(x => x.IsRead == false);
            }

            return this.View(homeModel);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            string[] languages = requestContext.HttpContext.Request.UserLanguages;

            if (languages != null)
            {
                Thread.CurrentThread.CurrentCulture =
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(languages[0]);
            }

            base.Initialize(requestContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = this.RedirectToAction("Index", "Error");
        }
    }
}