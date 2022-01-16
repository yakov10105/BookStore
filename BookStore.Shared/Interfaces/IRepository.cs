using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        void Remove(T item);
        IEnumerable<T> GetBy(Func<T,bool> func);
        void Update(T item);
    }
}
