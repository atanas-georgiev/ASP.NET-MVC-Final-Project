using System.Linq;
using Planex.Data.Models;

namespace Planex.Services.Skills
{
    public interface ISkillService
    {
        IQueryable<Skill> GetAll();
        void Add(Skill skill);
        void UpdateName(int id, string name);
        void Delete(int id);
    }
}
