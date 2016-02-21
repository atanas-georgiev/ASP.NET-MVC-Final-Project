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
        void Add(T entity);

        IQueryable<T> All();

        void Delete(T entity);

        void Delete(TKey entity);

        T GetById(TKey id);

        void Save();

        void Update(T entity);
    }

    public interface IHavePrimaryKey<TKey>
    {
        TKey Id { get; set; }
    }
}