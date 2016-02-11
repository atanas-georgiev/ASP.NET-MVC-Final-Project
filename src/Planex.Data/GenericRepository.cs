using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Planex.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            Context = context;
            DbSet = Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public virtual IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
                Context.SaveChanges();
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            Context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);

            if (entity != null)
            {
                Delete(entity);
            }
            Context.SaveChanges();
        }

        public virtual void Detach(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);

            entry.State = EntityState.Detached;
        }
    }
}