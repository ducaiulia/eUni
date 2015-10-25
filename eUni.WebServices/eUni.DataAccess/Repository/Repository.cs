using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.eUniDbContext;

namespace eUni.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected EUniDbContext Context = null;
        protected DbSet<T> DbSet
        {
            get
            {
                return Context.Set<T>();
            }
        }

        public Repository()
        {
        }

        public Repository(EUniDbContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<T>();
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual void Add(T T)
        {
            DbSet.Add(T);
            Context.SaveChanges();
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public virtual void Update(T T)
        {
            //todo
        }


    }
}
