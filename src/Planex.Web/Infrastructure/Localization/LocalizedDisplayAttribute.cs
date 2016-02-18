namespace Planex.Web.Infrastructure.Localization
{
    using System.ComponentModel;

    using Planex.Web.App_LocalResources;

    public class LocalizedDisplayAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayAttribute(string displayNameKey)
            : base(displayNameKey)
        {
            this.DisplayNameKey = displayNameKey;
        }

        public override string DisplayName
        {
            get
            {
                return ModelLabels.ResourceManager.GetString(this.DisplayNameKey);
            }
        }

        private string DisplayNameKey { get; set; }
    }
}