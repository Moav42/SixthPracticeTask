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
    public class CategoriesController : ControllerBase
    {
        private UnitOfWork _unitOfWork;

        public CategoriesController()
        {
            _unitOfWork = new UnitOfWork();
        }
       
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategorys()
        {
            return _unitOfWork.Categorys.ReadAll().ToList();
        }
      
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _unitOfWork.Categorys.Read(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpGet("{id}/products")]
        public ActionResult<IEnumerable<Product>> GetProductByCategory(int id)
        {
            return _unitOfWork.Categorys.GetProductByCategory(id).ToList();
        }

        [HttpGet("{categoryId}/suppliers")]
        public ActionResult<IEnumerable<Supplier>> SelectSupplaerByCategory(int categoryId)
        {
            return _unitOfWork.Suppliers.SelectSupplaerByCategory(categoryId).ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<Category> PutCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (_unitOfWork.Categorys.Read(category.Id) == null)
            {
                return BadRequest("Category doesn`t exicte");
            }
            _unitOfWork.Categorys.Update(category);
            _unitOfWork.SaveChanges();
            return category;
        }

        [HttpPost]
        public ActionResult<Category> PostCategory(Category category)
        {
            _unitOfWork.Categorys.Create(category);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> DeleteSupplier(int id)
        {
            var category = _unitOfWork.Categorys.Read(id);
            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.Categorys.Delete(category.Id);
            _unitOfWork.SaveChanges();

            return category;
        }
    }
}
