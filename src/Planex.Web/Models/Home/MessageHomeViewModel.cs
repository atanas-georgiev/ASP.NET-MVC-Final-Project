namespace Planex.Web.Models.Home
{
    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class MessageHomeViewModel
    {
        public int UnreadMessagesCount { get; set; }
    }
}