namespace Planex.Web.Areas.Lead.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;

    public class BaseProjectViewModel
    {
        [LocalizedDisplay("ProjectDescription")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        [AllowHtml]
        public string Description { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [LocalizedDisplay("ProjectManager")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string Manager { get; set; }

        [LocalizedDisplay("ProjectPriority")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public PriorityType Priority { get; set; }

        [LocalizedDisplay("ProjectStartDate")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [LocalizedDisplay("ProjectState")]
        [LocalizedRequired("RequiredFiled")]
        public TaskStateType State { get; set; }

        [LocalizedDisplay("ProjectTitle")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string Title { get; set; }

        public List<string> UploadedAttachmentFiles { get; set; }
    }
}