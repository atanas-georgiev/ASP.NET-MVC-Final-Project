using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Planex.Data.Models;
using Planex.Services.Skills;
using Assert = NUnit.Framework.Assert;

namespace Planex.Web.Tests.Services
{
    [TestFixture]
    public class SkillServiceTest
    {
        private ISkillService skills;

        [OneTimeSetUp]
        public void Init()
        {
            skills = new SkillService(new DbContext("test"), new RepositoryMock<Skill, int>());
        }

        [Test]
        public void AddSkillsShouldWorkProperly()
        {
            skills.Add(new Skill());
            Assert.AreEqual(1, skills.GetAll().Count());

            skills.Add(new Skill());
            Assert.AreEqual(2, skills.GetAll().Count());
        }

        [Test]
        public void DeleteSkillsShouldWorkProperly()
        {
            skills.Delete(0);
            Assert.AreEqual(1, skills.GetAll().Count());            
        }
    }
}
