using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleCms.Infrastructure
{
    public interface IRepository<TContext> where TContext : DbContext
    {
        void SaveChanges();
        T GetSingle<T>(Expression<Func<T, bool>> where) where T : class;
        IEnumerable<T> Get<T>(Expression<Func<T, bool>> where) where T : class;
        T Add<T>(T source) where T : class;
        void Delete<T>(Expression<Func<T, bool>> where) where T : class;
        void Update<T>(T source) where T : class;
    }
 }