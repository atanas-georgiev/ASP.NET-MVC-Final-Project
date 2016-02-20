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
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectDetailsViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        [UIHint("Editor")]
        [LocalizedRequired("RequiredFiled")]
        [LocalizedDisplay("ProjectDescription")]
        [AllowHtml]
        public string Description { get; set; }

        [UIHint("Date")]
        [LocalizedDisplay("ProjectEndDate")]
        public DateTime End { get; set; }

        [LocalizedDisplay("ProjectPrice")]
        public decimal FinalPrice { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [LocalizedRequired("RequiredFiled")]
        [LocalizedDisplay("ProjectLead")]
        public string LeadId { get; set; }

        public string LeadName { get; set; }

        [LocalizedRequired("RequiredFiled")]
        [LocalizedDisplay("ProjectPriority")]
        public PriorityType Priority { get; set; }

        [UIHint("Date")]
        [LocalizedRequired("RequiredFiled")]
        [LocalizedDisplay("ProjectStartDate")]
        public DateTime Start { get; set; }

        [LocalizedRequired("RequiredFiled")]
        [LocalizedDisplay("ProjectState")]
        public TaskStateType State { get; set; }

        [UIHint("String")]
        [LocalizedRequired("RequiredFiled")]
        [LocalizedDisplay("ProjectTitle")]
        public string Title { get; set; }

        public List<string> UploadedAttachmentFiles { get; set; }

        public List<HttpPostedFileBase> UploadedAttachments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectDetailsViewModel>(string.Empty)
                .ForMember(m => m.LeadName, opt => opt.MapFrom(c => c.Lead.FirstName + " " + c.Lead.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)));
        }
    }
}