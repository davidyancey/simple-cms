using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SimpleCms.Infrastructure.Models;

namespace SimpleCms.Infrastructure
{
    public class Repository<TContext> : IRepository<TContext> where TContext : DbContext
    {
        protected DbContext Context;

        public Repository(DbContext context, string connectionName)
        {
            Context = context;
            context.Database.Connection.ConnectionString =
               ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public T GetSingle<T>(Expression<Func<T, bool>> where) where T : class
        {
            return Context.Set<T>().Where(where).FirstOrDefault();
        }

        public IEnumerable<T> Get<T>(Expression<Func<T, bool>> where) where T : class
        {
            return Context.Set<T>().Where(where);
        }

        public IEnumerable<T> Get<T>() where T : class
        {
            return Context.Set<T>();
        }

        public T Add<T>(T source) where T : class
        {
            return Context.Set<T>().Add(source);
        }

        public void Delete<T>(Expression<Func<T, bool>> where) where T : class
        {
            Context.Set<T>().Remove(Context.Set<T>().Find(where));
        }

        public void Update<T>(T source) where T : class
        {
            Context.Entry(source).State = EntityState.Modified;
        }
    }
}
