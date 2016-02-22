namespace Planex.Data.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;
    using Common.Models;

    public class Message : BaseModel<int>, IHavePrimaryKey<int>
    {
        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("FromId")]
        public User From { get; set; }

        public string FromId { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsRead { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSystemMessage { get; set; }

        public SystemMessageType? MessageType { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public int? ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [ForeignKey("SubTaskId")]
        public SubTask SubTask { get; set; }

        public int? SubTaskId { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Text { get; set; }

        [ForeignKey("ToId")]
        public User To { get; set; }

        public string ToId { get; set; }
    }
}