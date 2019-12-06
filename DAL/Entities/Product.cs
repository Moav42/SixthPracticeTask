using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("CategoryFK")]
        public int CategoryFK { get; set; }

        [ForeignKey("SupplierFK")]
        public int SupplierFK { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public override string ToString()
        {
            return $"Gun - {Name}, Category - {CategoryFK}, SupplaerId - { SupplierFK}, Price - { Price}, Quantity - {Quantity}";
        }
    }
}
