using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _475_Lab_4_Part_3
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> entitySet;

        public Repository(DbContext datacontext)
        {
            entitySet = datacontext.Set<T>();
            context = datacontext;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Insert(T item)
        {
            entitySet.Add(item);
            context.Entry(item).State = EntityState.Added;
            context.SaveChanges();
            context.Entry(item).State = EntityState.Detached;
        }

        public void Remove(T item)
        {
            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Entry(item).State = EntityState.Detached;
        }

        public void Update(T item)
        {
            entitySet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
            context.Entry(item).State = EntityState.Detached;
        }

        public T GetById(T id)
        {
            return id;
        }
        
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return entitySet.Where(predicate);
        }
        
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IList<T> list;
            using (var context = new SchoolDBEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                list = dbQuery
                    .ToList<T>();
            }
            return list;
        }

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                context.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~Repository()
        {
            Dispose(false);
        }

        //This method will find the related records by passing two argument
        //First argument: lambda expression to search a record such as d => d.StandardName.Equals(standardName) to search am record by standard name
        //Second argument: navigation property that leads to the related records such as d => d.Students
        //The method returns the related records that met the condition in the first argument.
        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = entitySet.Include<T, object>(navigationProperty);

            item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(where);

            return item;
        }

        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new SchoolDBEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }
    }
}
