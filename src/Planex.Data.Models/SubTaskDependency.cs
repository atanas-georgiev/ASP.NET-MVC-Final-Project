namespace Planex.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Kendo.Mvc.UI;

    using Planex.Data.Common;
    using Planex.Data.Common.Models;

    public class SubTaskDependency : BaseModel<int>, IHavePrimaryKey<int>
    {
        [Key]
        public int Id { get; set; }

        public int PredecessorId { get; set; }

        public int SuccessorId { get; set; }

        public DependencyType Type { get; set; }
    }
}