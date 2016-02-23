namespace Planex.Data.Models
{
    using Kendo.Mvc.UI;

    using Planex.Data.Common;
    using Planex.Data.Common.Models;

    public class SubTaskDependency : BaseModel<int>, IHavePrimaryKey<int>
    {
        public int PredecessorId { get; set; }

        public int SuccessorId { get; set; }

        public DependencyType Type { get; set; }
    }
}