using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ETicaretAPI.Application.Dtos.Product;
using ETicaretAPI.Application.Repositories.ProductRepo;
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

        public ProductController(IReadProductRepo readProductRepo, IWriteProductRepo writeProductRepo)
        {
            _readProductRepo = readProductRepo;
            _writeProductRepo = writeProductRepo;
        }

        [HttpGet]
        public async Task Get()
        {
            await _writeProductRepo.AddAsync(new()
            {
                Name = "�rnek veri",
                Price = 50F,
                Stock = 50
            });

            await _writeProductRepo.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> Get(string id)
        {
            Product product = await _readProductRepo.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_readProductRepo.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductDto createProductDto)
        {
            //if (ModelState.IsValid)
            //{

            //}

            await _writeProductRepo.AddAsync(new(){
                Name = createProductDto.Name,
                Price=createProductDto.Price,
                Stock=createProductDto.Stock
            });
            await _writeProductRepo.SaveAsync();
            return StatusCode((int) HttpStatusCode.Created);
        }

        // Dummy Data
        [HttpPost]
        public async Task AddBulkProducts()
        {
            List<CreateProductDto> productsDto = new List<CreateProductDto>();

            // 100000 �r�n olu�tur
            for (int i = 0; i < 100000; i++)
            {
                var productDto = new CreateProductDto
                (
                    Name: $"�rnek veri {i}",
                    Stock: 50,
                    Price: 50f,
                    CreatedDate: DateTime.UtcNow
                );

                productsDto.Add(productDto);
            }

            // DTO'dan Product entity'sine d�n��t�r
            List<Product> products = productsDto.Select(dto => new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Stock = dto.Stock,
                Price = dto.Price,
                CreatedDate = dto.CreatedDate
            }).ToList();

            // Veritaban�na ekle
            await _writeProductRepo.AddRangeAsync(products);
            await _writeProductRepo.SaveAsync();
        }

    }
}