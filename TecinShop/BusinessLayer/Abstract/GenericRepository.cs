using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        DataContext Db = new DataContext();
        DbSet<T> data;
        public GenericRepository()
        {
            data = Db.Set<T>();
        }
        public List<T> List()
        {
            return data.ToList();
        }
        public T GetById(int id)
        {
            return data.Find(id);
        }
        public void Insert(T parameter)
        {
            data.Add(parameter);
            Db.SaveChanges();
        }
        public void Update(T parameter)
        {
            Db.Entry<T>(parameter).State = EntityState.Modified;
            Db.SaveChanges();
        }
        public void Delete(T parameter)
        {
            data.Remove(parameter);
            Db.SaveChanges();
        }
    }
}
