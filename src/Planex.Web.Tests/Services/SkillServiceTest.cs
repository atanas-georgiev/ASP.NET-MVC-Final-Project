namespace Planex.Web.Tests.Services
{
    using System.Data.Entity;
    using System.Linq;

    using NUnit.Framework;

    using Planex.Data.Models;
    using Planex.Services.Skills;

    [TestFixture]
    public class SkillServiceTest
    {
        private ISkillService skills;

        [OneTimeSetUp]
        public void Init()
        {
            this.skills = new SkillService(new DbContext("test"), new RepositoryMock<Skill, int>());
        }

        [Test]
        public void AddSkillsShouldWorkProperly()
        {
            this.skills.Add(new Skill());
            Assert.AreEqual(1, this.skills.GetAll().Count());

            this.skills.Add(new Skill());
            Assert.AreEqual(2, this.skills.GetAll().Count());
        }

        [Test]
        public void DeleteSkillsShouldWorkProperly()
        {
            this.skills.Delete(0);
            Assert.AreEqual(1, this.skills.GetAll().Count());
        }
    }
}