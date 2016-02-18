namespace Planex.Web.Models.Messages
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.App_LocalResources;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class MessageCreateViewModel : IMapFrom<Message>
    {
        [Key]
        public int Id { get; set; }

        [UIHint("DateTime")]
        public DateTime Date { get; set; }

        [UIHint("String")]
        [LocalizedDisplay("Receiver")]
        [LocalizedRequired("Receiver")]
        public string Receiver { get; set; }

        [MaxLength(100)]
        [UIHint("String")]
        [LocalizedDisplay("Subject")]
        [LocalizedRequired("Subject")]
        public string Subject { get; set; }

        [Required]
        [MaxLength(10000)]
        [UIHint("Editor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        public bool IsRead { get; set; }
    }
}