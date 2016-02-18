namespace Planex.Data.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Planex.Data.Common;
    using Planex.Data.Common.Models;

    public class Message : BaseModel<int>, IHavePrimaryKey<int>
    {
        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("FromId")]
        public User From { get; set; }

        public string FromId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Text { get; set; }

        [ForeignKey("ToId")]
        public User To { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsRead { get; set; }

        public string ToId { get; set; }
    }
}