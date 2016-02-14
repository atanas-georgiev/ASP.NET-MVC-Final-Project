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

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Required]
        public int MainTaskId { get; set; }

        public virtual MainTask MainTask { get; set; }

        public virtual ICollection<Skill> Skills
        {
            get { return this.skills; }
            set { this.skills = value; }
        }

        public virtual ICollection<Attachment> Attachments
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
