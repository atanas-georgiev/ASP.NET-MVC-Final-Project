namespace Planex.Services.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Planex.Data;
    using Planex.Data.Common;
    using Planex.Data.Models;

    public class MessageService : IMessageService
    {
        private DbContext context;

        private IRepository<Project, int> projects;
        private IRepository<Message, int> messages;
        private IRepository<User> users;
        private IRepository<SubTask, int> tasks;

        public MessageService(DbContext context, IRepository<Message, int> messages, IRepository<User> users, IRepository<Project, int> projects, IRepository<SubTask, int> tasks)
        {
            this.context = context;
            this.messages = messages;
            this.users = users;
            this.projects = projects;
            this.tasks = tasks;
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
            var systemUser = this.users.All().FirstOrDefault(x => x.FirstName == "System" && x.LastName == "Message");
            var users = this.users.All();
            var allNonSystem = this.messages.All().Where(x => x.IsSystemMessage == false);
            var allSystem = this.messages.All().Where(x => x.IsSystemMessage == true);
            List<Message> allSystemModified = new List<Message>();

            foreach (var systemMessage in allSystem)
            {
                var toUser = users.FirstOrDefault(x => x.Id == systemMessage.ToId);
                var fromUser = users.FirstOrDefault(x => x.Id == systemMessage.FromId);
                switch (systemMessage.MessageType)
                {
                    case SystemMessageType.ProjectRequestedEstimation:
                        {
                            allSystemModified.Add(new Message()
                            {
                                Id = systemMessage.Id,
                                IsRead = systemMessage.IsRead,
                                FromId = systemUser.Id,
                                From = systemUser,
                                ToId = toUser.Id,
                                To = toUser,
                                Subject = String.Format(SystemMessagesResources.ProjectRequestedEstimationSubject.ToString(), this.projects.All().FirstOrDefault(x => x.Id == systemMessage.ProjectId).Title),
                                Text = String.Format(SystemMessagesResources.ProjectRequestedEstimationText, toUser.FirstName + " " + toUser.LastName, fromUser.FirstName + " " + fromUser.LastName, this.projects.All().FirstOrDefault(x => x.Id == systemMessage.ProjectId).Title),
                                Date = systemMessage.Date,
                            });
                            break;
                        }
                    case SystemMessageType.ProjectEstimated:
                        {
                            allSystemModified.Add(new Message()
                            {
                                Id = systemMessage.Id,
                                IsRead = systemMessage.IsRead,
                                FromId = systemUser.Id,
                                From = systemUser,
                                ToId = toUser.Id,
                                To = toUser,
                                Subject = String.Format(SystemMessagesResources.ProjectEstimatedSubject.ToString(), this.projects.All().FirstOrDefault(x => x.Id == systemMessage.ProjectId).Title),
                                Text = String.Format(SystemMessagesResources.ProjectEstimatedText, toUser.FirstName + " " + toUser.LastName, fromUser.FirstName + " " + fromUser.LastName, this.projects.All().FirstOrDefault(x => x.Id == systemMessage.ProjectId).Title),
                                Date = systemMessage.Date,
                            });
                            break;
                        }
                    case SystemMessageType.TaskComplete:
                        {
                            allSystemModified.Add(new Message()
                            {
                                Id = systemMessage.Id,
                                IsRead = systemMessage.IsRead,
                                FromId = systemUser.Id,
                                From = systemUser,
                                ToId = toUser.Id,
                                To = toUser,
                                Subject = String.Format(SystemMessagesResources.TaskCompletedSubject.ToString(), this.tasks.All().FirstOrDefault(x => x.Id == systemMessage.SubTaskId).Title),
                                Text = String.Format(SystemMessagesResources.TaskCompletedText, toUser.FirstName + " " + toUser.LastName, fromUser.FirstName + " " + fromUser.LastName, this.tasks.All().FirstOrDefault(x => x.Id == systemMessage.SubTaskId).Title, this.projects.All().FirstOrDefault(x => x.Id == systemMessage.ProjectId).Title, fromUser.FirstName + " " + fromUser.LastName),
                                Date = systemMessage.Date,
                            });
                            break;
                        }
                    case SystemMessageType.ProjectApproved:
                        {
                            allSystemModified.Add(new Message()
                            {
                                Id = systemMessage.Id,
                                IsRead = systemMessage.IsRead,
                                FromId = systemUser.Id,
                                From = systemUser,
                                ToId = toUser.Id,
                                To = toUser,
                                Subject = String.Format(SystemMessagesResources.ProjectApprovedSubject.ToString(), this.projects.All().FirstOrDefault(x => x.Id == systemMessage.ProjectId).Title),
                                Text = String.Format(SystemMessagesResources.ProjectApprovedSubject, toUser.FirstName + " " + toUser.LastName, fromUser.FirstName + " " + fromUser.LastName, this.projects.All().FirstOrDefault(x => x.Id == systemMessage.ProjectId).Title),
                                Date = systemMessage.Date,
                            });
                            break;
                        }
                }
            }
            allSystemModified.AddRange(allNonSystem);
            return allSystemModified.AsQueryable();
        }

        public void Update(Message message)
        {
            var messageDb = this.messages.All().FirstOrDefault(x => x.Id == message.Id);
            messageDb.IsRead = message.IsRead;
            this.messages.Update(messageDb);
        }

        public void SendSystemMessage(string senderId, string receiverId, SystemMessageType messageType, int? projectId, int? taskId)
        {
            var message = new Message();
            message.FromId = senderId;
            message.ToId = receiverId;
            message.IsSystemMessage = true;
            message.MessageType = messageType;
            message.ProjectId = projectId;
            message.SubTaskId = taskId;
            message.Subject = "System";
            message.Text = "System";
            message.Date = DateTime.UtcNow;
            this.Add(message);
        }
    }
}