namespace Planex.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(300)]
        public string Name { get; set; }
    }
}
