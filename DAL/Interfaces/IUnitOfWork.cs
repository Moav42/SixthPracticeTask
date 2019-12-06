using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository<Category> Categorys { get; }
        IProductRepository<Product> Products { get; }
        ISupplierRepository<Supplier> Suppliers { get; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
