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
        private ICollection<SubTask> subTasks;

        private ICollection<User> users;

        public SubTask()
        {
            this.users = new HashSet<User>();
            this.subTasks = new HashSet<SubTask>();
        }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool? IsUserNotified { get; set; }

        [ForeignKey("ParentId")]
        public virtual SubTask Parent { get; set; }

        public int? ParentId { get; set; }

        [Required]
        public decimal PercentComplete { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int ProjectId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public virtual ICollection<SubTask> Subtasks
        {
            get
            {
                return this.subTasks;
            }

            set
            {
                this.subTasks = value;
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