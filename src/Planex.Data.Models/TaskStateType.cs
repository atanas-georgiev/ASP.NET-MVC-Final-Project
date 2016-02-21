namespace Planex.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Planex.Data.Models.Resources;

    public enum TaskStateType
    {
        [Display(ResourceType = typeof(ModelResources), Name = "ProjectStateDraft")]
        Draft = 0, 

        [Display(ResourceType = typeof(ModelResources), Name = "ProjectStateRequestedEstimation")]
        UnderEstimation = 2, 

        [Display(ResourceType = typeof(ModelResources), Name = "ProjectStateEstimated")]
        Estimated = 3, 

        [Display(ResourceType = typeof(ModelResources), Name = "ProjectStateStarted")]
        Started = 4, 

        [Display(ResourceType = typeof(ModelResources), Name = "ProjectStateFinished")]
        Finished = 5
    }
}