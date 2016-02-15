using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Models.SubTask
{
    public class SubTaskViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int MainTaskId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ParentId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? DependencyId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string Title { get; set; }

        [MinLength(2)]
        [MaxLength(500)]
        [UIHint("Editor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        public string SelectedSkill { get; set; }

        public List<string> SelectedUsers { get; set; }

        [UIHint("Date")]
        public DateTime Start { get; set; }

        [UIHint("Date")]
        public DateTime End { get; set; }

        [UIHint("Number")]
        public int? Duration { get; set; }

        public List<HttpPostedFileBase> UploadedAttachments { get; set; }
    }
}