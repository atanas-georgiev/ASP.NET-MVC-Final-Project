using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Planex.Data.Models;

namespace Planex.Web.Areas.Lead.Models.SubTask
{
    public class SubTaskViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string Title { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(500)]
        [UIHint("Editor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [Required]
        [UIHint("Number")]
        public int? Duration { get; set; }

        public List<string> UploadedAttachmentFiles { get; set; }
    }
}