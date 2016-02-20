namespace Planex.Web.Infrastructure.Localization
{
    using System.ComponentModel.DataAnnotations;

    using Planex.Web.App_LocalResources;

    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        public LocalizedRequiredAttribute(string error)
        {
            this.ErrorMessageResourceType = typeof(ErrorMessages);
            this.ErrorMessageResourceName = error;
            
        }
    }
}