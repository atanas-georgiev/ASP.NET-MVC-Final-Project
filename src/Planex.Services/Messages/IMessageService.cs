namespace Planex.Services.Messages
{
    using System.Linq;

    using Planex.Data.Models;

    public interface IMessageService
    {
        void Add(Message message);

        void Delete(int id);

        IQueryable<Message> GetAll();

        void Update(Message message);

        void SendSystemMessage(string senderId, string receiverId, SystemMessageType messageType, int? projectId, int? taskId);
    }
}