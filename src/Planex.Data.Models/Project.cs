namespace Planex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Planex.Data.Common;
    using Planex.Data.Common.Models;

    public class Project : BaseModel<int>, IHavePrimaryKey<int>
    {
        private ICollection<Attachment> attachments;

        private ICollection<SubTask> tasks;

        public Project()
        {
            this.tasks = new HashSet<SubTask>();
            this.attachments = new HashSet<Attachment>();
        }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Description { get; set; }

        [Required]
        public DateTime End { get; set; }

        [ForeignKey("LeadId")]
        public virtual User Lead { get; set; }

        public string LeadId { get; set; }

        [ForeignKey("ManagerId")]
        public virtual User Manager { get; set; }

        public string ManagerId { get; set; }

        [Required]
        public decimal PercentComplete { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public PriorityType Priority { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public TaskStateType State { get; set; }

        public virtual ICollection<SubTask> Subtasks
        {
            get
            {
                return this.tasks;
            }

            set
            {
                this.tasks = value;
            }
        }

        public virtual ICollection<Attachment> Attachments
        {
            get
            {
                return this.attachments;
            }

            set
            {
                this.attachments = value;
            }
        }
    }
}