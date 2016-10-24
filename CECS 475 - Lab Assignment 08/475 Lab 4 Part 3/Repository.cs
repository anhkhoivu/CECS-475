using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void Insert(params T[] items)
        {
            using (var context = new SchoolDBEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(T entity)
        {
            //Use the context object and entity state to delete the entity
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            //Use the context object and entity state to update the entity
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
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
                    .AsNoTracking()
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
                //
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
        //An example of the method GetStandardByName(string standardName)
        //public Standard GetStandardByName(string standardName)
        //{
        //return _standardRepository.GetSingle(d => d.StandardName.Equals(standardName), d => d.Students);
        //} 
        public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = null;
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = entitySet.Include<T, object>(navigationProperty);

            item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(where);
            return item;

        }
    }
}
