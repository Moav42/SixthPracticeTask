using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DAL.EF;
using DAL.Repositories;
using DAL.Entities;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context DataBase { get; }
        private ICategoryRepository<Category> categorysRepository;
        private IProductRepository<Product> prductsRepository;
        private ISupplierRepository<Supplier> suppliersRepository;

        public UnitOfWork()
        {
            DataBase = new Context();
        }

        public ICategoryRepository<Category> Categorys
        {
            get
            {
                if (categorysRepository == null)
                    categorysRepository = new CategoryRepository(DataBase);
                return categorysRepository;
            }
        }

        public IProductRepository<Product> Products
        {
            get
            {
                if (prductsRepository == null)
                    prductsRepository = new ProductRepository(DataBase);
                return prductsRepository;
            }
        }

        public ISupplierRepository<Supplier> Suppliers
        {
            get
            {
                if (suppliersRepository == null)
                    suppliersRepository = new SupplierRepository(DataBase);
                return suppliersRepository;
            }
        }
        public void Dispose()
        {
            DataBase.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DataBase.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            DataBase.SaveChanges();
        }

       
    }
}
