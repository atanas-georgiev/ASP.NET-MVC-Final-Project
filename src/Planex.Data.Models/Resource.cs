using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Planex.Data.Models
{
    public class Resource
    {
        private ICollection<Subtask> subTasks;

        public Resource()
        {
            this.subTasks = new HashSet<Subtask>();
        }
        
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Unit { get; set; }

        public virtual ICollection<Subtask> Subtasks
        {
            get { return this.subTasks; }
            set { this.subTasks = value; }
        }

    }
}
