namespace Planex.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Kendo.Mvc.UI;

    public class SubTaskDependency
    {
        [Key]
        public int DependencyId { get; set; }

        public int PredecessorId { get; set; }

        public int SuccessorId { get; set; }

        public DependencyType Type { get; set; }
    }
}