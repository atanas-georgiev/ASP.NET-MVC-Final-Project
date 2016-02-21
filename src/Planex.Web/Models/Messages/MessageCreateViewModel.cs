namespace Planex.Web.Models.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class MessageCreateViewModel : IMapFrom<Message>
    {
        [UIHint("DateTime")]
        public DateTime Date { get; set; }

        [Key]
        public int Id { get; set; }

        public bool IsRead { get; set; }

        [UIHint("String")]
        [LocalizedDisplay("MessageReceiver")]
        [LocalizedRequired("RequiredFiled")]
        public string Receiver { get; set; }

        [MaxLength(100)]
        [UIHint("String")]
        [LocalizedDisplay("MessageSubject")]
        [LocalizedRequired("RequiredFiled")]
        public string Subject { get; set; }

        [UIHint("Editor")]
        [LocalizedDisplay("MessageText")]
        [LocalizedRequired("RequiredFiled")]
        [AllowHtml]
        public string Text { get; set; }
    }
}