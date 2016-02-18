namespace Planex.Data
{
    using System.Linq;

    public interface IRepository<T>
        where T : class
    {
        void Add(T entity);

        IQueryable<T> All();

        void Delete(T entity);

        void Delete(object id);

        void Detach(T entity);

        T GetById(object id);

        void Update(T entity);
    }
}