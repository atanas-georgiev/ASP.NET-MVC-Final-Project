namespace Planex.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using Planex.Web.App_LocalResources;
    using Planex.Web.Infrastructure.Localization;

    public class ResetPasswordViewModel
    {
        public string Code { get; set; }

        [DataType(DataType.Password)]
        [LocalizedDisplay("UserConfirmPassword")]
        [LocalizedRequired("RequiredFiled")]
        [Compare("Password", ErrorMessageResourceType = typeof(ErrorMessages), 
            ErrorMessageResourceName = "PasswordError")]
        public string ConfirmPassword { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(ErrorMessages), 
            ErrorMessageResourceName = "PasswordErrorLen")]
        [DataType(DataType.Password)]
        [LocalizedDisplay("UserPassword")]
        [LocalizedRequired("RequiredFiled")]
        public string Password { get; set; }
    }
}