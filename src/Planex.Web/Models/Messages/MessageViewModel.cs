namespace Planex.Web.Models.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class MessageViewModel : IMapFrom<Message>
    {
        [UIHint("DateTime")]
        [LocalizedDisplay("MessageDate")]
        public DateTime Date { get; set; }

        [LocalizedDisplay("MessageSender")]
        public MessageUserViewModel From { get; set; }

        [Key]
        public int Id { get; set; }

        public bool IsRead { get; set; }

        [LocalizedDisplay("MessageSubject")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string Subject { get; set; }

        [LocalizedDisplay("MessageText")]
        [LocalizedRequired("RequiredFiled")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        public MessageUserViewModel To { get; set; }
    }
}