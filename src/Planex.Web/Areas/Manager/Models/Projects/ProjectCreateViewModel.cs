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
        [LocalizedRequired("RequiredFiled")]
        [MaxLength(10000)]
        [UIHint("Editor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [LocalizedRequired("RequiredFiled")]
        public string LeadId { get; set; }

        [LocalizedRequired("RequiredFiled")]
        public decimal Price { get; set; }

        [LocalizedRequired("RequiredFiled")]
        public PriorityType Priority { get; set; }

        [LocalizedRequired("RequiredFiled")]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [LocalizedRequired("RequiredFiled")]
        public TaskStateType State { get; set; }

        [LocalizedRequired("RequiredFiled")]
        [MaxLength(100)]
        [UIHint("String")]
        public string Title { get; set; }

        public List<HttpPostedFileBase> UploadedAttachments { get; set; }
    }
}