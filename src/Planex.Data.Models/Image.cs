namespace Planex.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Common.Models;

    public class Image : BaseModel<int>, IHavePrimaryKey<int>
    {
        public byte[] Content { get; set; }

        [MaxLength(10)]
        public string FileExtension { get; set; }
    }
}