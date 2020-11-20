using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BTv6.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected BusinessToolDBEntities context = new BusinessToolDBEntities();

        public void Delete(string id)
        {
            this.context.Set<T>().Remove(Get(id));
            this.context.SaveChanges();
        }

        public T Get(string id)
        {
            return this.context.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return this.context.Set<T>().ToList();
        }

        public void Insert(T entity)
        {
            this.context.Set<T>().Add(entity);
            this.context.SaveChanges();
        }

        public void Update(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }


    }
}