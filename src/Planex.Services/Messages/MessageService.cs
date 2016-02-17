using System.Data.Entity;
using System.Linq;
using Planex.Data;
using Planex.Data.Models;

namespace Planex.Services.Messages
{
    public class MessageService : IMessageService
    {
        private DbContext context;
        private IRepository<Message> messages;

        public MessageService(DbContext context, IRepository<Message> messages)
        {
            this.context = context;
            this.messages = messages;
        }


        public IQueryable<Message> GetAll()
        {
            return messages.All();
        }

        public void Add(Message message)
        {
            messages.Add(message);
        }

        public void Update(Message message)
        {
            messages.Update(message);
        }

        public void Delete(int id)
        {
            messages.Delete(id);
        }
    }
}
