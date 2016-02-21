namespace Planex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Planex.Data.Common;
    using Planex.Data.Common.Models;

    public class SubTask : BaseModel<int>, IHavePrimaryKey<int>
    {
        private ICollection<SubTask> dependencies;

        private ICollection<SubTask> tasks;

        private ICollection<User> users;

        public SubTask()
        {
            this.users = new HashSet<User>();
            this.tasks = new List<SubTask>();
            this.dependencies = new List<SubTask>();
        }

        public virtual ICollection<SubTask> Dependences
        {
            get
            {
                return this.dependencies;
            }

            set
            {
                this.dependencies = value;
            }
        }

        public string Description { get; set; }

        public int Duration { get; set; }

        public DateTime End { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool? IsUserNotified { get; set; }

        [ForeignKey("ParentId")]
        public virtual SubTask Parent { get; set; }

        public int? ParentId { get; set; }

        public decimal PercentComplete { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }

        public int? SkillId { get; set; }

        public DateTime Start { get; set; }

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

        public string Title { get; set; }

        public virtual ICollection<User> Users
        {
            get
            {
                return this.users;
            }

            set
            {
                this.users = value;
            }
        }
    }
}