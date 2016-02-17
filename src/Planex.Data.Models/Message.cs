using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planex.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string FromId { get; set; }

        [ForeignKey("FromId")]
        public User From { get; set; }

        public string ToId { get; set; }

        [ForeignKey("ToId")]
        public User To { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
