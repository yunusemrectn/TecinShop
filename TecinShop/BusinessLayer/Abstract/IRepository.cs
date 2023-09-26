using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRepository<T> where T:class, new()
    {
        List<T> List();
        T GetById(int id);
        void Insert(T parameter);
        void Update(T parameter);
        void Delete(T parameter);
        
    }
}
