namespace Planex.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Planex.Data.Common;
    using Planex.Data.Common.Models;

    public class Skill : BaseModel<int>, IHavePrimaryKey<int>
    {
        private ICollection<SubTask> subTasks;

        private ICollection<User> users;

        public Skill()
        {
            this.users = new HashSet<User>();
            this.subTasks = new HashSet<SubTask>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

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