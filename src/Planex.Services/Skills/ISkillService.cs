namespace Planex.Services.Skills
{
    using System.Linq;

    using Planex.Data.Models;

    public interface ISkillService
    {
        void Add(Skill skill);

        void Delete(int id);

        IQueryable<Skill> GetAll();

        void UpdateName(int id, string name);
    }
}