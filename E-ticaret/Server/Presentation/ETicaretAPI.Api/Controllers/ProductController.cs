using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using ETicaretAPI.Application.Dtos.Product;
using ETicaretAPI.Application.Repositories.ProductRepo;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IReadProductRepo _readProductRepo;
        private readonly IWriteProductRepo _writeProductRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IReadProductRepo readProductRepo, IWriteProductRepo writeProductRepo, IWebHostEnvironment webHostEnvironment)
        {
            _readProductRepo = readProductRepo;
            _writeProductRepo = writeProductRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll
            (
                [FromQuery] Pagination pagination,
                [FromQuery] Guid? id = null,
                [FromQuery] string? name = null,
                [FromQuery] string? stock = null,
                [FromQuery] string? price = null,
                [FromQuery] string? createdDate = null,
                [FromQuery] string? updatedDate = null,
                [FromQuery] string? sortColumn = null,
                [FromQuery] bool ascending = true
            )
        {
            var products = await _readProductRepo.GetPagedAsync
                (
                    pagination, id, name, stock, price, createdDate, updatedDate, sortColumn, ascending
                );
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _readProductRepo.GetByIdAsync(id);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productExists = await _writeProductRepo.ExistInDatabaseAsync(p => p.Name == createProductDto.Name);
            if (productExists)
            {
                return BadRequest("Bu ürün adý zaten mevcut.");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = createProductDto.Name,
                Stock = createProductDto.Stock,
                Price = createProductDto.Price,
            };

            await _writeProductRepo.AddAsync(product);
            await _writeProductRepo.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = await _readProductRepo.GetByIdAsync(id);
            if (existingProduct is null) return NotFound();

            var productExists = await _writeProductRepo.ExistInDatabaseAsync(p => p.Name == updateProductDto.Name && p.Id != id);
            if (productExists)
            {
                return BadRequest("Bu ürün adý baþka bir kayýt tarafýndan kullanýlmakta.");
            }

            existingProduct.Name = updateProductDto.Name;
            existingProduct.Stock = updateProductDto.Stock;
            existingProduct.Price = updateProductDto.Price;
            existingProduct.UpdatedDate = DateTime.UtcNow; 

            await _writeProductRepo.UpdateAsync(existingProduct);
            await _writeProductRepo.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _readProductRepo.GetByIdAsync(id);
            if (product is null) return NotFound();

            await _writeProductRepo.DeleteAsync(product);
            await _writeProductRepo.SaveAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/products");
            
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            Random random = new();

            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{random.Next()}{Path.GetExtension(file.FileName)}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync:false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return NoContent();
        }

    }
}