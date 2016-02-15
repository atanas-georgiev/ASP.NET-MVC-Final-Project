using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Models
{
    public class TaskCreateViewModel : IMapFrom<MainTask>
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
        public TaskStateType State { get; set; }

        [Required]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [Required]
        [UIHint("EnumDropDown")]
        public PriorityType Priority { get; set; }

        public List<HttpPostedFileBase> UploadedAttachments { get; set; }
    }
}