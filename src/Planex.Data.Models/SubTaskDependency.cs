namespace Planex.Data.Models
{   
    using Common;
    using Common.Models;

    using Kendo.Mvc.UI;

    public class SubTaskDependency : BaseModel<int>, IHavePrimaryKey<int>
    {
        public int PredecessorId { get; set; }

        public int SuccessorId { get; set; }

        public DependencyType Type { get; set; }
    }
}