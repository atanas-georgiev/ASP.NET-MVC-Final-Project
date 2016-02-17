using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Planex.Data.Models;

namespace Planex.Web.Areas.Lead.Models
{
    public class BaseProjectViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public string Title { get; set; }

        [Required]
        [MaxLength(10000)]
        [UIHint("String")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [UIHint("String")]
        public PriorityType Priority { get; set; }

        [Required]
        [UIHint("String")]
        public string Manager { get; set; }

        [Required]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [Required]
        public TaskStateType State { get; set; }

        public List<string> UploadedAttachmentFiles { get; set; }
    }
}