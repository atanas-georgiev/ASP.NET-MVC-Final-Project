namespace Planex.Data.Models
{
    using Planex.Data.Common;
    using Planex.Data.Common.Models;

    public class Image : BaseModel<int>, IHavePrimaryKey<int>
    {
        public byte[] Content { get; set; }

        public string FileExtension { get; set; }
    }
}