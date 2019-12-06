using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository<Product>
    {
        private Context DB;
        public ProductRepository(Context context)
        {
            DB = context;
        }

        public void Create(Product item)
        {
            DB.Products.Add(item);
        }

        public void Delete(int id)
        {
            Product product = DB.Products.Find(id);
            if (product != null)
                DB.Products.Remove(product);
        }

        public Product Read(int id)
        {
            return DB.Products.Find(id);
        }

        public IEnumerable<Product> ReadAll()
        {
            return DB.Products;
        }

        public IEnumerable<Product> SearchBySpecifiedCriteria(Func<Product, bool> func)
        {
            return DB.Products.Where(func);
        }

        public void Update(Product item)
        {
            DB.Products.Update(item);
        }

    }
}
