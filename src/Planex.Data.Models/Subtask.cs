using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planex.Data.Models
{
    public class Subtask
    {
        private ICollection<Skill> skills;
        private ICollection<Attachment> attachements;
        private ICollection<User> users;

        public Subtask()
        {
            this.skills = new HashSet<Skill>();
            this.attachements = new HashSet<Attachment>();
            this.users = new HashSet<User>();
        }

        public int Id { get; set; }

        public DateTime Start;

        [Required]
        public DateTime End;

        [Required]
        public int MainTaskId { get; set; }

        public virtual MainTask MainTask { get; set; }

        public virtual ICollection<Skill> Skills
        {
            get { return this.skills; }
            set { this.skills = value; }
        }

        public virtual ICollection<Attachment> Resources
        {
            get { return this.attachements; }
            set { this.attachements = value; }
        }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}
