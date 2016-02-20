namespace Planex.Web.Infrastructure.Scheduler
{
    using System;
    using System.Linq;
    using System.Web.Hosting;

    using FluentScheduler;

    using Planex.Data;
    using Planex.Data.Common;
    using Planex.Data.Models;
    using Planex.Services.Cache;
    using Planex.Services.Messages;
    using Planex.Services.Projects;
    using Planex.Services.Skills;
    using Planex.Services.Tasks;
    using Planex.Services.Users;

    public class ProjectStateTask : ITask, IRegisteredObject
    {
        private readonly object _lock = new object();

        private bool _shuttingDown;

        private readonly Repository<Message, int> messageService;

        private readonly MessageService messageSrv;

        private readonly Repository<Project, int> projectService;

        private readonly Repository<SubTask, int> subTaskService;

        private readonly Repository<User> userService;


        public ProjectStateTask()
        {
            // todo dependency injection
            var db = new PlanexDbContext();
            this.userService = new Repository<User>(db);
            this.subTaskService = new Repository<SubTask, int>(db);
            this.projectService = new Repository<Project, int>(db);
            this.messageService = new Repository<Message, int>(db);

            messageSrv = new MessageService(db, this.messageService, this.userService, this.projectService, this.subTaskService);

            // Register this task with the hosting environment.
            // Allows for a more graceful stop of the task, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;

                var projects =
                    this.projectService.All().Where(x => x.PercentComplete == 1 && x.State != TaskStateType.Finished);
                var systemUser = userService.All().FirstOrDefault(x => x.FirstName == "System" && x.LastName == "Message");

                foreach (var project in projects)
                {
                    project.State = TaskStateType.Finished;
                    this.messageSrv.SendSystemMessage(systemUser.Id, project.LeadId, SystemMessageType.ProjectCompleted, project.Id, null);
                    this.messageSrv.SendSystemMessage(systemUser.Id, project.ManagerId, SystemMessageType.ProjectCompleted, project.Id, null);
                    this.projectService.Update(project);
                }

                var tasks = this.subTaskService.All().Where(x => x.PercentComplete != 1 && x.End < DateTime.UtcNow && x.IsUserNotified != null && x.IsUserNotified.Value == false);

                foreach (var task in tasks)
                {
                    task.IsUserNotified = true;
                    this.messageSrv.SendSystemMessage(systemUser.Id, task.Project.ManagerId, SystemMessageType.ProjectCompleted, task.Project.Id, task.Id);
                    this.subTaskService.Update(task);
                }
            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}