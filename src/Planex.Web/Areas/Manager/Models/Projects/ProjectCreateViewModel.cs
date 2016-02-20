namespace Planex.Web.Areas.Manager.Models.Projects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectCreateViewModel : IMapFrom<Project>
    {
        [LocalizedDisplay("ProjectDescription")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Editor")]
        [AllowHtml]
        public string Description { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [LocalizedDisplay("ProjectLead")]
        [LocalizedRequired("RequiredFiled")]
        public string LeadId { get; set; }

        [LocalizedDisplay("ProjectPrice")]
        [LocalizedRequired("RequiredFiled")]
        public decimal Price { get; set; }

        [LocalizedDisplay("ProjectPriority")]
        [LocalizedRequired("RequiredFiled")]
        public PriorityType Priority { get; set; }

        [LocalizedDisplay("ProjectStartDate")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        public TaskStateType State { get; set; }

        [LocalizedDisplay("ProjectTitle")]
        [LocalizedRequired("RequiredFiled")]
        [MaxLength(100)]
        [UIHint("String")]
        public string Title { get; set; }

        public List<HttpPostedFileBase> UploadedAttachments { get; set; }
    }
}