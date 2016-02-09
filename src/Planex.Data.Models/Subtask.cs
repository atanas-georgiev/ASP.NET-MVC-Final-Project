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
        private ICollection<Resource> resources;

        public Subtask()
        {
            this.skills = new HashSet<Skill>();
            this.resources = new HashSet<Resource>();
        }

        public int Id { get; set; }

        [Required]
        public TimeSpan Duration;

        [Required]
        public int MainTaskId { get; set; }

        public virtual MainTask MainTask { get; set; }

        public virtual ICollection<Skill> Skills
        {
            get { return this.skills; }
            set { this.skills = value; }
        }

        public virtual ICollection<Resource> Resources
        {
            get { return this.resources; }
            set { this.resources = value; }
        }        
    }
}
