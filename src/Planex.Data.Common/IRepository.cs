namespace Planex.Data.Common
{
    using System.Linq;

    using Planex.Data.Common.Models;

    public interface IRepository<T> : IRepository<T, string>
    where T : class, IHavePrimaryKey<string>, IDeletableEntity, IAuditInfo
    {
    }

    public interface IRepository<T, in TKey> : IDbGenericRepository<T, TKey>
        where T : class, IHavePrimaryKey<TKey>, IDeletableEntity, IAuditInfo
    {
        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);
    }

    public interface IDbGenericRepository<T, in TKey>
        where T : class
    {
        IQueryable<T> All();

        T GetById(TKey id);

        void Add(T entity);

        void Delete(T entity);

        void Delete(TKey entity);

        void Update(T entity);

        void Save();
    }

    public interface IHavePrimaryKey<TKey>
    {
        TKey Id { get; set; }
    }
}
