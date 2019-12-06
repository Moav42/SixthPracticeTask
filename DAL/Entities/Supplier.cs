using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string SupplierName { get; set; } 
        public override string ToString()
        {
            return $"Supplaer {SupplierName}, Id - {Id}";
        }
    }
}
