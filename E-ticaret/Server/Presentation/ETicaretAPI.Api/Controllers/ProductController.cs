using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using ETicaretAPI.Application.Dtos.Product;
using ETicaretAPI.Application.Repositories.FileRepo;
using ETicaretAPI.Application.Repositories.InvoiceFileRepo;
using ETicaretAPI.Application.Repositories.ProductRepo;
using ETicaretAPI.Application.Repositories.ProductRepo.ProductImageFileRepo;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
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
        private readonly IFileService _fileService;
        private readonly IWriteFileRepo _writeFileRepo;
        private readonly IReadFileRepo _readFileRepo;
        private readonly IReadProductImageFileRepo _readProductImageFileRepo;
        private readonly IWriteProductImageFileRepo _writeProductImageFileRepo;
        private readonly IReadInvoiceRepo _readInvoiceRepo;
        private readonly IWriteInvoiceRepo _writeInvoiceRepo;

        public ProductController
            (
                IReadProductRepo readProductRepo, 
                IWriteProductRepo writeProductRepo, 
                IFileService fileService, 
                IWriteFileRepo writeFileRepo, 
                IReadFileRepo readFileRepo, 
                IReadProductImageFileRepo readProductImageFileRepo, 
                IWriteProductImageFileRepo writeProductImageFileRepo, 
                IReadInvoiceRepo readInvoiceRepo, 
                IWriteInvoiceRepo writeInvoiceRepo
            )
        {
            _readProductRepo = readProductRepo;
            _writeProductRepo = writeProductRepo;
            _fileService = fileService;
            _writeFileRepo = writeFileRepo;
            _readFileRepo = readFileRepo;
            _readProductImageFileRepo = readProductImageFileRepo;
            _writeProductImageFileRepo = writeProductImageFileRepo;
            _readInvoiceRepo = readInvoiceRepo;
            _writeInvoiceRepo = writeInvoiceRepo;
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
            var datas= await _fileService.UploadAsync("images/products", Request.Form.Files);
            await _writeProductImageFileRepo.AddRangeAsync(datas
                                                            .Select(d => new ProductImageFile()
                                                            {
                                                                FileName = d.fileName,
                                                                Path = d.path,
                                                            }).ToList());

            await _writeProductImageFileRepo.SaveAsync();
            return NoContent();
        }

    }
}