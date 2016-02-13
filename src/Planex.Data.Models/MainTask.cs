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
        private ICollection<Attachment> attachments;

        public MainTask()
        {
            this.subTasks = new HashSet<Subtask>();
            this.attachments = new HashSet<Attachment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public PriorityType Priority { get; set; }

        public TaskStateType State { get; set; }

        public string ManagerId { get; set; }

        public virtual User Manager { get; set; }

        public string LeadId { get; set; }

        public virtual User Lead { get; set; }

        public virtual ICollection<Subtask> Subtasks
        {
            get { return this.subTasks; }
            set { this.subTasks = value; }
        }

        public virtual ICollection<Attachment> Attachments
        {
            get { return this.attachments; }
            set { this.attachments = value; }
        }
    }
}
