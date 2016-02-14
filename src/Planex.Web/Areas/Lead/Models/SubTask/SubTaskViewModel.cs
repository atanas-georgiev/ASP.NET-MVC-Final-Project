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

//        [Required]
//        [UIHint("EnumDropDown")]
//        public PriorityType Priority { get; set; }
//
//        [Required]
//        [UIHint("Email")]
//        public string Manager { get; set; }
//
//        [Required]
//        [UIHint("Email")]
//        public string Lead { get; set; }
//
//        [Required]
//        [UIHint("String")]
//        public TaskStateType State { get; set; }

        public List<string> UploadedAttachmentFiles { get; set; }
    }
}