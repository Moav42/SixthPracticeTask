using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using DAL.Entities;
using DAL.Repositories;
using DAL;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private UnitOfWork _unitOfWork;

        public SuppliersController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [HttpGet]
        public  ActionResult<IEnumerable<Supplier>> GetSuppliers()
        {
            return  _unitOfWork.Suppliers.ReadAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Supplier> GetSupplier(int id)
        {
            var supplier = _unitOfWork.Suppliers.Read(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }
        [HttpGet("{id}/products")]
        public ActionResult<IEnumerable<Product>> GetProductBySupplaer(int id)
        {
            return _unitOfWork.Suppliers.GetProductBySupplaer(id).ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<Supplier> PutSupplier( Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (_unitOfWork.Products.Read(supplier.Id) == null)
            {
                return BadRequest("Supplier doesn`t exicte");
            }
            _unitOfWork.Suppliers.Update(supplier);
            _unitOfWork.SaveChanges();
            return supplier;
        }

        [HttpPost]
        public ActionResult<Supplier> PostSupplier(Supplier supplier)
        {
            _unitOfWork.Suppliers.Create(supplier);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("GetSupplier", new { id = supplier.Id }, supplier);
        }

        [HttpDelete("{id}")]
        public ActionResult<Supplier> DeleteSupplier(int id)
        {
            var supplier =  _unitOfWork.Suppliers.Read(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _unitOfWork.Suppliers.Delete(supplier.Id);
            _unitOfWork.SaveChanges();

            return supplier;
        }
    }
}
