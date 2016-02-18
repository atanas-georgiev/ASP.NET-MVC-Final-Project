namespace Planex.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public byte[] Content { get; set; }

        public string FileExtension { get; set; }

        [Key]
        public int Id { get; set; }
    }
}