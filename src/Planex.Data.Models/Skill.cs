namespace Planex.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Skill
    {
        private ICollection<SubTask> subTasks;

        private ICollection<User> users;

        public Skill()
        {
            this.users = new HashSet<User>();
            this.subTasks = new HashSet<SubTask>();
        }

        [Key]
        public int Id { get; set; }

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