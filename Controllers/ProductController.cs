using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NG_Core_Auth.Data;
using NG_Core_Auth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.MiddlewareAnalysis;

namespace NG_Core_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        } 



        // GET: api/Product

        [HttpGet("[action]")]
        [Authorize(Policy = "RequireLoggedIn")]
        public IActionResult GetProducts()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpPost("[action]")]
        [Authorize(Policy = "RequireAdministrator")]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel product)
        {
            var newproduct = new ProductModel()
            {
                Name = product.Name,
                Discription = product.Discription,
                OutOfStock = product.OutOfStock,
                ImageUrl = product.ImageUrl,
                Price = product.Price
            };
            await _db.Products.AddAsync(newproduct);

            await _db.SaveChangesAsync();

            return Ok( new JsonResult("The Product was Added Successfully"));

            // create error
        }

        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "RequireAdministrator")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductModel formdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var findProduct = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if(findProduct == null)
            {
                return NotFound();
            }
            // nếu tìm product

            findProduct.Name = formdata.Name;
            findProduct.Discription = formdata.Discription;
            findProduct.ImageUrl = formdata.ImageUrl;
            findProduct.OutOfStock = formdata.OutOfStock;
            findProduct.Price = formdata.Price;

            _db.Entry(findProduct).State = EntityState.Modified;

            await _db.SaveChangesAsync();

            return Ok(new JsonResult("The Product with" + id + "is updated"));
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(Policy = "RequireAdministrator")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // find Product

            var findProduct = await _db.Products.FindAsync(id);
            if(findProduct == null)
            {
                return NotFound();
            }
            _db.Products.Remove(findProduct);

            await _db.SaveChangesAsync();

            return Ok(new JsonResult("The Product with" + id + "is deleted"));
        }
    }
}
