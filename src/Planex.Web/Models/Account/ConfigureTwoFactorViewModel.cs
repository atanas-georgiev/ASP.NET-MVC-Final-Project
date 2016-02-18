namespace Planex.Web.Models.Account
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ConfigureTwoFactorViewModel
    {
        public ICollection<SelectListItem> Providers { get; set; }

        public string SelectedProvider { get; set; }
    }
}