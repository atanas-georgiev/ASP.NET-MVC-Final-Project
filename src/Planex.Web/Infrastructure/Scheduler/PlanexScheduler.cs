namespace Planex.Web.Infrastructure.Scheduler
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Timers;

    using Planex.Data.Common;
    using Planex.Data.Models;
    using Planex.Services.Messages;

    using Timer = System.Timers.Timer;

    public class PlanexScheduler : IPlanexScheduler
    {
        private static Thread thread;

        private DbContext context;

        private IMessageService messageService;

        private IRepository<Project, int> projects;

        private IRepository<SubTask, int> tasks;

        private IRepository<User> users;

        public PlanexScheduler(
            DbContext context, 
            IMessageService messageService, 
            IRepository<User> users, 
            IRepository<Project, int> projects, 
            IRepository<SubTask, int> tasks)
        {
            this.context = context;
            this.users = users;
            this.projects = projects;
            this.tasks = tasks;
            this.messageService = messageService;

            this.TimerWorker();

            //            if (thread == null)
            //            {
            //                thread = new Thread(new ThreadStart(this.ThreadFunc));
            //                thread.IsBackground = true;
            //                thread.Name = "ThreadFunc";
            //                thread.Start();
            //            }
        }

//        protected void ThreadFunc()
//        {
//            Timer t = new System.Timers.Timer();
//            t.Elapsed += new System.Timers.ElapsedEventHandler(this.TimerWorker);
//            t.Interval = 10000;
//            t.Enabled = true;
//            t.AutoReset = true;
//            t.Start();
//        }

        protected void TimerWorker()
        {
            var projectsDb = this.projects.All().Where(x => x.PercentComplete == 1 && x.State != TaskStateType.Finished).ToList();
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
                        x.PercentComplete != 1 && x.End < DateTime.UtcNow && x.IsUserNotified != null
                        && x.IsUserNotified.Value == false).ToList();

            foreach (var task in tasksDb)
            {
                task.IsUserNotified = true;
                this.messageService.SendSystemMessage(
                    systemUser.Id, 
                    task.Project.ManagerId, 
                    SystemMessageType.ProjectCompleted, 
                    task.Project.Id, 
                    task.Id);
                this.tasks.Update(task);
            }
        }
    }
}