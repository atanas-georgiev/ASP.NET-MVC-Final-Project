using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Planex.Data;
using Planex.Data.Models;

namespace Planex.Services.Skills
{
    public class SkillService : ISkillService
    {
        private DbContext context;
        private IRepository<Skill> skills;        

        public SkillService(DbContext context, IRepository<Skill> skills)
        {
            this.context = context;
            this.skills = skills;            
        }

        public IQueryable<Skill> GetAll()
        {
            return this.skills.All();
        }

        public void Add(Skill skill)
        {
            this.skills.Add(skill);            
        }

        public void UpdateName(int id, string name)
        {
            var entity = skills.GetById(id);
            entity.Name = name;
            skills.Update(entity);
        }

        public void Delete(int id)
        {
            skills.Delete(id);
        }
    }
}
