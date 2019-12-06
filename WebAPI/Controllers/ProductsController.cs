using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using DAL.Entities;
using DAL;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private UnitOfWork _unitOfWork;

        public ProductsController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _unitOfWork.Products.ReadAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _unitOfWork.Products.Read(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        public ActionResult<Product> PutProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if(_unitOfWork.Products.Read(product.Id) == null)
            {
                return BadRequest("Product doesn`t exicte");
            }   

            _unitOfWork.Products.Update(product);
            _unitOfWork.SaveChanges();
            return product;
        }

        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            _unitOfWork.Products.Create(product);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var product = _unitOfWork.Products.Read(id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Products.Delete(product.Id);
            _unitOfWork.SaveChanges();

            return product;
        }
    }
}
