namespace Planex.Web.Areas.Manager.Models.Projects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectCreateViewModel : IMapFrom<Project>
    {
        [Required]
        [MaxLength(10000)]
        [UIHint("Editor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string LeadId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public PriorityType Priority { get; set; }

        [Required]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [Required]
        public TaskStateType State { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public string Title { get; set; }

        public List<HttpPostedFileBase> UploadedAttachments { get; set; }
    }
}