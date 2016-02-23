using Planex.Web.App_LocalResources;

namespace Planex.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using Planex.Data.Models;
    using Planex.Services.Users;
    using Planex.Web.Infrastructure.Extensions;
    using Planex.Web.Infrastructure.Notifications.Toastr;
    using Planex.Web.Models.Account;

    [Authorize]
    public class AccountController : BaseController
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private ApplicationSignInManager signInManager;

        private ApplicationUserManager userManager;

        public AccountController(IUserService userService)
            : base(userService)
        {
        }

        public AccountController(
            ApplicationUserManager userManager, 
            ApplicationSignInManager signInManager, 
            IUserService userService)
            : base(userService)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result =
                await
                this.SignInManager.PasswordSignInAsync(
                    model.Email, 
                    model.Password, 
                    model.RememberMe, 
                    shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    this.AddToastMessage("", NotificationMessages.LoginSuccess, ToastType.Success);
                    var user = this.UserService.GetAll().FirstOrDefault(x => x.Email == model.Email);
                    if (user != null && user.ResetPassword)
                    {
                        return this.RedirectToAction("ResetPassword");
                    }

                    return this.RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.RequiresVerification:
                    return this.RedirectToAction(
                        "SendCode", 
                        new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View(model);
            }
        }

        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            this.AddToastMessage("", NotificationMessages.LogoutSuccess, ToastType.Success);
            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult ResetPassword()
        {
            return this.View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.AddToastMessage("", NotificationMessages.PasswordChanged, ToastType.Success);
            this.UserProfile.ResetPassword = false;
            this.UserService.UpdatePassword(this.UserProfile, model.Password);
            return this.RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.userManager != null)
                {
                    this.userManager.Dispose();
                    this.userManager = null;
                }

                if (this.signInManager != null)
                {
                    this.signInManager.Dispose();
                    this.signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                this.LoginProvider = provider;
                this.RedirectUri = redirectUri;
                this.UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = this.RedirectUri };
                if (this.UserId != null)
                {
                    properties.Dictionary[AccountController.XsrfKey] = this.UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, this.LoginProvider);
            }
        }
    }
}