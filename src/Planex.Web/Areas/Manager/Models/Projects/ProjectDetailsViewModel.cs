namespace Planex.Web.Areas.Manager.Models.Projects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectDetailsViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        [Required]
        [UIHint("Number")]
        public int Completed { get; set; }

        [Required]
        [MaxLength(10000)]
        [UIHint("Editor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        [UIHint("Date")]
        public DateTime End { get; set; }

        public decimal FinalPrice { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string LeadId { get; set; }

        public string LeadName { get; set; }

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

        public List<string> UploadedAttachmentFiles { get; set; }

        public List<HttpPostedFileBase> UploadedAttachments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectDetailsViewModel>(string.Empty)
                .ForMember(m => m.LeadName, opt => opt.MapFrom(c => c.Lead.FirstName + " " + c.Lead.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)))
                .ForMember(m => m.Completed, opt => opt.MapFrom(c => 0));
        }
    }
}