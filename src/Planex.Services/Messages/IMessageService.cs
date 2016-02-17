using System.Linq;
using Planex.Data.Models;

namespace Planex.Services.Messages
{
    public interface IMessageService
    {
        IQueryable<Message> GetAll();
        void Add(Message message);
        void Update(Message message);
        void Delete(int id);
    }
}
