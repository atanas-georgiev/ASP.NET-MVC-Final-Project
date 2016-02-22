namespace Planex.Web.Infrastructure.Scheduler
{
    using System;
    using System.Linq;

    using Planex.Data.Common;
    using Planex.Data.Models;
    using Planex.Services.Messages;

    public class PlanexScheduler : IPlanexScheduler
    {        
        private IMessageService messageService;

        private IRepository<Project, int> projects;

        private IRepository<SubTask, int> tasks;

        private IRepository<User> users;

        public PlanexScheduler(
            IMessageService messageService, 
            IRepository<User> users, 
            IRepository<Project, int> projects, 
            IRepository<SubTask, int> tasks)
        {
            this.users = users;
            this.projects = projects;
            this.tasks = tasks;
            this.messageService = messageService;         
        }

        public void Schedule()
        {
            var projectsDb =
                this.projects.All().Where(x => x.PercentComplete == 1 && x.State != TaskStateType.Finished).ToList();
            var systemUser = this.users.All().FirstOrDefault(x => x.FirstName == "System" && x.LastName == "Message");

            foreach (var project in projectsDb)
            {
                project.State = TaskStateType.Finished;
                this.messageService.SendSystemMessage(
                    systemUser.Id, 
                    project.LeadId, 
                    SystemMessageType.ProjectCompleted, 
                    project.Id, 
                    null);
                this.messageService.SendSystemMessage(
                    systemUser.Id, 
                    project.ManagerId, 
                    SystemMessageType.ProjectCompleted, 
                    project.Id, 
                    null);
                this.projects.Update(project);
            }

            var tasksDb =
                this.tasks.All()
                    .Where(
                        x =>
                        x.PercentComplete != 1 && x.End < DateTime.Now && x.IsUserNotified != null
                        && x.IsUserNotified.Value == false)
                    .ToList();

            foreach (var task in tasksDb)
            {
                task.IsUserNotified = true;
                this.messageService.SendSystemMessage(
                    systemUser.Id, 
                    task.Project.ManagerId, 
                    SystemMessageType.TaskOverDue, 
                    task.Project.Id, 
                    task.Id);
                this.tasks.Update(task);
            }
        }
    }
}