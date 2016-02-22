namespace Planex.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Common.Models;

    public class Attachment : BaseModel<int>, IHavePrimaryKey<int>
    {
        [MaxLength(300)]
        public string Name { get; set; }
    }
}