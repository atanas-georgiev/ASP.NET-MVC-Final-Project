namespace Planex.Web.Areas.Manager.Models
{
    public class SubTaskAssignentsViewModel
    {
        public int Id { get; set; }
        public int taskId { get; set; }

        public int resourceId { get; set; }

        public int value { get; set; }
    }
}