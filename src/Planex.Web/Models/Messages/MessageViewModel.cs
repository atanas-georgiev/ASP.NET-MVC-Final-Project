namespace Planex.Web.Models.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class MessageViewModel : IMapFrom<Message>
    {
        [Key]
        public int Id { get; set; }

        [UIHint("DateTime")]
        public DateTime Date { get; set; }

        public MessageUserViewModel From { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public string Subject { get; set; }

        [Required]
        [MaxLength(10000)]
        [UIHint("Editor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        public bool IsRead { get; set; }

        public MessageUserViewModel To { get; set; }
    }
}