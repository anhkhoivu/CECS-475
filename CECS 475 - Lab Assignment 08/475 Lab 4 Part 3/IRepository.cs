using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _475_Lab_4_Part_3
{
    public interface IRepository<T> : IDisposable
    {
        void Insert(T items);
        void Remove(T entity);
        void Update(T entity);
        T GetById(T id);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
    }
}
