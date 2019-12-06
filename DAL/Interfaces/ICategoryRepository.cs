using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ICategoryRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<Product> GetProductByCategory(int category);
        IEnumerable<Supplier> SelectSupplaerByCategory(int category);
    }
}
