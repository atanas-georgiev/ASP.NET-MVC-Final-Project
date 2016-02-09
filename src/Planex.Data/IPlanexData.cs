using System.Data.Entity;
using Planex.Data.Models;

namespace Planex.Data
{
    public interface IPlanexData
    {
        DbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<Skill> Skills { get; }

        IRepository<Resource> Resources { get; }

        IRepository<Subtask> Subtasks { get; }

        IRepository<MainTask> MainTasks { get; }

        void Dispose();

        int SaveChanges();
    }
}