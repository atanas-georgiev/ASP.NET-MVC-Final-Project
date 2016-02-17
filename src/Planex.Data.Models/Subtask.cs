using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Planex.Data.Models
{
    public class SubTask
    {
        private ICollection<User> users;
        private ICollection<SubTask> tasks;
        private ICollection<SubTask> dependencies;

        public SubTask()
        {
            this.users = new HashSet<User>();
            this.tasks = new List<SubTask>();
            this.dependencies = new List<SubTask>();
        }

        public int Id { get; set; }
        
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual SubTask Parent { get; set; }

        public int? DependencyId { get; set; }

        [ForeignKey("DependencyId")]
        public virtual SubTask Dependency { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int? SkillId { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }

        public decimal PercentComplete { get; set; }

        public virtual ICollection<SubTask> Subtasks
        {
            get { return this.tasks; }
            set { this.tasks = value; }
        }

        public virtual ICollection<SubTask> Dependences
        {
            get { return this.dependencies; }
            set { this.dependencies = value; }
        }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}
