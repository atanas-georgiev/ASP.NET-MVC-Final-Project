namespace Planex.Web.Areas.Worker.Models.Assignments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class AssignmentViewModel : IMapFrom<SubTask>
    {
        [LocalizedDisplay("ProjectEndDate")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Date")]
        public DateTime End { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [LocalizedDisplay("ProjectCompleted")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Percent")]
        [Range(0, 100)]
        public decimal PercentComplete { get; set; }

        [Key]
        public int ProjectId { get; set; }

        [LocalizedDisplay("ProjectTitle")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string ProjectTitle { get; set; }

        [LocalizedDisplay("ProjectStartDate")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [LocalizedDisplay("ProjectTitle")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string Title { get; set; }
    }
}