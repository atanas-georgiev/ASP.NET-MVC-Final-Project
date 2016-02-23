using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planex.Data.Common;
using Planex.Data.Common.Models;
using Planex.Data.Models;

namespace Planex.Web.Tests.Services
{
    public class RepositoryMock<T, TKey> : IRepository<T, TKey>
        where T : BaseModel<TKey>, IHavePrimaryKey<TKey> where TKey : struct
    {
        private List<T> data;         

        public RepositoryMock()
        {          
            this.data = new List<T>();  
        }

        public void Add(T entity)
        {
            this.data.Add(entity);
            this.Save();
        }

        public IQueryable<T> All()
        {
            return this.data.AsQueryable();
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.data.AsQueryable();
        }

        public void Delete(T entity)
        {
            if (this.data.Count > 0)
            {
                this.data.RemoveAt(0);
            }
        }

        public virtual void Delete(TKey id)
        {
            if (this.data.Count > 0)
            {
                this.data.RemoveAt(0);
            }
        }

        public T GetById(TKey id)
        {
            return this.data.First();
        }

        public void HardDelete(T entity)
        {
            if (this.data.Count > 0)
            {
                this.data.RemoveAt(0);
            }
        }

        public void Save()
        {
            return;
        }

        public void Update(T entity)
        {
            return;
        }
    }
}
