using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planex.Data.Models
{
    public class MainTask
    {
        private ICollection<Subtask> subTasks;

        public MainTask()
        {
            this.subTasks = new HashSet<Subtask>();
        }

        public int Id { get; set; }

        public DateTime DeadLine { get; set; }

        public PriorityType Priority { get; set; }

        public virtual ICollection<Subtask> Subtasks
        {
            get { return this.subTasks; }
            set { this.subTasks = value; }
        }
    }
}
