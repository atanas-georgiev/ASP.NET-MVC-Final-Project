using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Models.Messages
{
    public class MessageCreateViewModel : IMapFrom<Message>
    {
        [Key]
        public int Id;

        [Required]
        [UIHint("String")]
        public string Receiver { get; set; }

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

        [UIHint("DateTime")]
        public DateTime Date { get; set; }
    }
}