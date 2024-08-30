using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Name = "Laptop Casper Nirvana",
                Price = 1.500F,
                Stock = 10
            });

            await _writeProductRepo.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> Get(string id)
        {
            Product product = await _readProductRepo.GetByIdAsync(id);
            return Ok(product);
        }
    }
}