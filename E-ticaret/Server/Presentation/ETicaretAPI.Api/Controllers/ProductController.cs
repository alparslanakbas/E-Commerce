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

        [HttpPost]
        public async Task<IActionResult> Add(Product productt)
        {
            var product = await _writeProductRepo.AddAsync(productt);
            return Ok(product);
        }

        [HttpGet]
        public async Task Get()
        {
           Product p = await _readProductRepo.GetByIdAsync("");
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> Get(string id)
        {
            Product product = await _readProductRepo.GetByIdAsync(id);
            return Ok(product);
        }
    }
}