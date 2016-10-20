using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _475_Lab_4_Part_3
{
    interface IRepository<T> : IDisposable
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(T id);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
    }
}
