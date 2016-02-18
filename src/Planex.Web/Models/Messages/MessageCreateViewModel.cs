﻿namespace Planex.Web.Models.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class MessageCreateViewModel : IMapFrom<Message>
    {
        [Key]
        public int Id;

        [UIHint("DateTime")]
        public DateTime Date { get; set; }

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
    }
}