using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Planex.Data.Models;
using Planex.Services.Skills;
using Planex.Services.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Planex.Web.Tests.Services
{
    [TestFixture]
    public class TaskServiceTest
    {
        private ITaskService tasks;

        [OneTimeSetUp]
        public void Init()
        {
            tasks = new TaskService(new RepositoryMock<SubTask, int>(), new RepositoryMock<SubTaskDependency, int>(), new RepositoryMock<Project, int>(), new RepositoryMock<Message, int>());
        }

        [Test]
        public void AddTaskShouldWorkProperly()
        {
            tasks.Add(new SubTask() { Start = DateTime.Now });
            Assert.AreEqual(1, tasks.GetAll().Count());

            tasks.Add(new SubTask() { Start = DateTime.Now });
            Assert.AreEqual(2, tasks.GetAll().Count());
        }

        [Test]
        public void DeleteTaskShouldWorkProperly()
        {
            tasks.Delete(0);
            Assert.AreEqual(1, tasks.GetAll().Count());
        }

        [Test]
        public void AddDependencyShouldWorkProperly()
        {
            tasks.AddDependency(new SubTaskDependency());
            Assert.AreEqual(1, tasks.AllDependencies().Count());

            tasks.AddDependency(new SubTaskDependency());
            Assert.AreEqual(2, tasks.AllDependencies().Count());
        }

        [Test]
        public void DeteleDependencyShouldWorkProperly()
        {
            tasks.DeleteDependency(0);
            Assert.AreEqual(1, tasks.AllDependencies().Count());
        }

        [Test]
        public void CheckIfDateInTaskIsWrittenCorrectly()
        {
            var t = tasks.GetById(0);
            Assert.AreEqual(t.Start.Date, DateTime.Now.Date);
        }
    }
}
