using System;
using System.Collections.Generic;
using System.Data.Entity;
using Planex.Data.Models;

namespace Planex.Data
{
    public class PlanexData : IPlanexData
    {
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public PlanexData(DbContext context)
        {
            Context = context;
        }

        public IRepository<MainTask> MainTaskss => GetRepository<MainTask>();

        public IRepository<User> Users => GetRepository<User>();

        public IRepository<Skill> Skills => GetRepository<Skill>();

        public IRepository<Resource> Resources => GetRepository<Resource>();

        public IRepository<Subtask> Subtasks => GetRepository<Subtask>();

        public IRepository<MainTask> MainTasks => GetRepository<MainTask>();

        public DbContext Context { get; }

        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        ///     The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context?.Dispose();
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!repositories.ContainsKey(typeof (T)))
            {
                var type = typeof (GenericRepository<T>);

                repositories.Add(typeof (T), Activator.CreateInstance(type, Context));
            }

            return (IRepository<T>) repositories[typeof (T)];
        }
    }
}