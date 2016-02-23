namespace Planex.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Planex.Data.Models.Resources;

    public enum PriorityType
    {
        [Display(ResourceType = typeof(ModelResources), Name = "PriorityLow")]
        Low = 0, 

        [Display(ResourceType = typeof(ModelResources), Name = "PriorityNormal")]
        Medium = 1, 

        [Display(ResourceType = typeof(ModelResources), Name = "PriorityHigh")]
        High = 2
    }
}