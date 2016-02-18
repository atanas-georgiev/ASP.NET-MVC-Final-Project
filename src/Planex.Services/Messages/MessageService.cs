namespace Planex.Services.Messages
{
    using System.Data.Entity;
    using System.Linq;

    using Planex.Data;
    using Planex.Data.Models;

    public class MessageService : IMessageService
    {
        private DbContext context;

        private IRepository<Message> messages;

        public MessageService(DbContext context, IRepository<Message> messages)
        {
            this.context = context;
            this.messages = messages;
        }

        public void Add(Message message)
        {
            this.messages.Add(message);
        }

        public void Delete(int id)
        {
            this.messages.Delete(id);
        }

        public IQueryable<Message> GetAll()
        {
            return this.messages.All();
        }

        public void Update(Message message)
        {
            this.messages.Update(message);
        }
    }
}