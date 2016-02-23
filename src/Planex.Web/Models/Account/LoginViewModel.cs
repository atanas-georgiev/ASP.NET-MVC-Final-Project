namespace Planex.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using Planex.Web.Infrastructure.Localization;

    public class LoginViewModel
    {
        [LocalizedDisplay("UserEmail")]
        [LocalizedRequired("RequiredFiled")]
        [EmailAddress]
        public string Email { get; set; }

        [LocalizedDisplay("UserPassword")]
        [LocalizedRequired("RequiredFiled")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [LocalizedDisplay("UserRememberMe")]
        public bool RememberMe { get; set; }
    }
}