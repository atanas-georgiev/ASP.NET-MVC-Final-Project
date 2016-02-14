using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Planex.Web.Areas.Lead.Models.SubTask;

namespace Planex.Web.Areas.Lead.Models
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mappings;

    public class EstimationEditViewModelSubTask : SubTaskViewModel, IMapFrom<Subtask>, IHaveCustomMappings
    {
//        [Key]
//        [HiddenInput(DisplayValue = false)]
//        public int Id { get; set; }
//
//        [Required]
//        [MinLength(2)]
//        [MaxLength(50)]
//        [UIHint("String")]
//        public string Title { get; set; }
//
//        [Required]
//        [MinLength(2)]
//        [MaxLength(500)]
//        [UIHint("Editor")]
//        [DataType(DataType.MultilineText)]
//        [AllowHtml]
//        public string Description { get; set; }
//
//        public List<HttpPostedFileBase> UploadedAttachments { get; set; }
//
//        [Required]
//        public string Skill { get; set; }
//
//        [Required]
//        public List<string> Users { get; set; }
//
//        [Required]
//        [UIHint("DateTime")]
//        public DateTime Start;
//
//        public DateTime End;
//
//        [Required]
//        public int MainTaskId { get; set; }
//
//        public virtual ICollection<Skill> Skills
//        {
//            get { return this.skills; }
//            set { this.skills = value; }
//        }

//        public virtual ICollection<Attachment> Attachments
//        {
//            get { return this.attachements; }
//            set { this.attachements = value; }
//        }

//        public virtual ICollection<User> Users
//        {
//            get { return this.users; }
//            set { this.users = value; }
//        }

        public void CreateMappings(IConfiguration configuration)
        {
//            configuration.CreateMap<Subtask, SubTaskViewModel>("")
//                .ForMember(m => m.Start, opt => opt.MapFrom(c => DateTime.Parse(c.Start.ToString())));
        }
    }
}