using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class SupplierRepository : ISupplierRepository<Supplier>
    {

        private Context DB;
        public SupplierRepository(Context context)
        {
            DB = context;
        }

        public void Create(Supplier item)
        {
            DB.Suppliers.Add(item);
        }

        public void Delete(int id)
        {
            Supplier supplier = DB.Suppliers.Find(id);
            if (supplier != null)
            {
                DB.Suppliers.Remove(supplier);
            }
        }
        public Supplier Read(int id)
        {
            return DB.Suppliers.Find(id);
        }

        public IEnumerable<Supplier> ReadAll()
        {
            return DB.Suppliers;
        }

        public void Update(Supplier item)
        {
            DB.Suppliers.Update(item);
        }
        public IEnumerable<Product> GetProductBySupplaer(int supplaer)
        {
            return DB.Products.Where(p => p.SupplierFK == supplaer);
        }
        public IEnumerable<Supplier> SelectSupplaerByCategory(int category)
        {
           
            IEnumerable<int> supplaersId = DB.Products.Where(c => c.CategoryFK == category).Distinct().Select(s => s.SupplierFK).AsEnumerable();
            foreach (var item in supplaersId)
            {
                yield return Read(item);
            }
        }

    }
}
