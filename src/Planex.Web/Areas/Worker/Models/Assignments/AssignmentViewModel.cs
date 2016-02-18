namespace Planex.Web.Areas.Worker.Models.Assignments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class AssignmentViewModel : IMapFrom<SubTask>
    {
        [Required]
        [UIHint("Date")]
        public DateTime End { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [UIHint("Percent")]
        [Range(0, 100)]
        public decimal PercentComplete { get; set; }

        [Key]
        public int ProjectId { get; set; }

        [UIHint("String")]
        public string ProjectTitle { get; set; }

        [Required]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public string Title { get; set; }
    }
}