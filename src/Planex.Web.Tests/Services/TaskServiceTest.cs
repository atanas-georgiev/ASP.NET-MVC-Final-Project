namespace Planex.Web.Tests.Services
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using Planex.Data.Models;
    using Planex.Services.Tasks;

    [TestFixture]
    public class TaskServiceTest
    {
        private ITaskService tasks;

        [OneTimeSetUp]
        public void Init()
        {
            this.tasks = new TaskService(
                new RepositoryMock<SubTask, int>(), 
                new RepositoryMock<SubTaskDependency, int>(), 
                new RepositoryMock<Project, int>(), 
                new RepositoryMock<Message, int>());
        }

        [Test]
        public void AddDependencyShouldWorkProperly()
        {
            this.tasks.AddDependency(new SubTaskDependency());
            Assert.AreEqual(1, this.tasks.AllDependencies().Count());

            this.tasks.AddDependency(new SubTaskDependency());
            Assert.AreEqual(2, this.tasks.AllDependencies().Count());
        }

        [Test]
        public void AddTaskShouldWorkProperly()
        {
            this.tasks.Add(new SubTask() { Start = DateTime.Now });
            Assert.AreEqual(1, this.tasks.GetAll().Count());

            this.tasks.Add(new SubTask() { Start = DateTime.Now });
            Assert.AreEqual(2, this.tasks.GetAll().Count());
        }

        [Test]
        public void CheckIfDateInTaskIsWrittenCorrectly()
        {
            var t = this.tasks.GetById(0);
            Assert.AreEqual(t.Start.Date, DateTime.Now.Date);
        }

        [Test]
        public void DeleteTaskShouldWorkProperly()
        {
            this.tasks.Delete(0);
            Assert.AreEqual(1, this.tasks.GetAll().Count());
        }

        [Test]
        public void DeteleDependencyShouldWorkProperly()
        {
            this.tasks.DeleteDependency(0);
            Assert.AreEqual(1, this.tasks.AllDependencies().Count());
        }
    }
}