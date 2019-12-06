using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository<Category>
    {
        private Context DB;
        public CategoryRepository(Context context)
        {
            DB = context;
        }

        public void Create(Category item)
        {
            DB.Categories.Add(item);
        }

        public void Delete(int id)
        {
            Category category = DB.Categories.Find(id);
            if(category != null)
            {
                DB.Categories.Remove(category);
            }

        }

        public Category Read(int id)
        {
            return DB.Categories.Find(id);
        }

        public IEnumerable<Category> ReadAll()
        {
            return DB.Categories;
        }

        public void Update(Category item)
        {
            DB.Categories.Update(item);
        }
        public IEnumerable<Product> GetProductByCategory(int category)
        {
            return DB.Products.Where(p => p.CategoryFK == category);

        }
        public IEnumerable<Supplier> SelectSupplaerByCategory(int category)
        {
            SupplierRepository sp = new SupplierRepository(DB);
            IEnumerable<int> supplaersId = DB.Products.Where(c => c.CategoryFK == category).Distinct().Select(s => s.SupplierFK).AsEnumerable();
            foreach (var item in supplaersId)
            {
                yield return sp.Read(item);
            }
        }
    }
}
