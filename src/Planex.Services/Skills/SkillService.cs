namespace Planex.Services.Skills
{
    using System.Data.Entity;
    using System.Linq;

    using Planex.Data;
    using Planex.Data.Models;

    public class SkillService : ISkillService
    {
        private DbContext context;

        private IRepository<Skill> skills;

        public SkillService(DbContext context, IRepository<Skill> skills)
        {
            this.context = context;
            this.skills = skills;
        }

        public void Add(Skill skill)
        {
            this.skills.Add(skill);
        }

        public void Delete(int id)
        {
            this.skills.Delete(id);
        }

        public IQueryable<Skill> GetAll()
        {
            return this.skills.All();
        }

        public void UpdateName(int id, string name)
        {
            var entity = this.skills.GetById(id);
            entity.Name = name;
            this.skills.Update(entity);
        }
    }
}