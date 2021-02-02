using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Infrastructure.Abstract;
using BlogApp.Areas.Admin.Data;
using System.Data.Entity;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class GenericRepository<T>:IGenericRepository<T> where T:class
    {
        protected blogEntities db = null;
        protected DbSet<T> table = null;

        public GenericRepository()
        {
            this.db = new blogEntities();
            table = db.Set<T>();
        }

        public GenericRepository(blogEntities db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return table.ToList();
        }

        public T SelectByID(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void DeleteAll()
        {
            foreach(var item in table)
            {
                table.Remove(item);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}