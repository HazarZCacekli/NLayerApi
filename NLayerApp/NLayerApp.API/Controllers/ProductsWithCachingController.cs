using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.Core.DTOS;
using NLayerApp.Core.Services;

namespace NLayerApp.API.Controllers
{
    public class ProductsWithCachingController : CustomBaseController
    {
        private readonly IProductServiceWithCaching _service;

        public ProductsWithCachingController(IProductServiceWithCaching service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateDTO productdto)
        {
            return CreateActionResult(await _service.AddAsync(productdto));
        }

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(List<ProductCreateDTO> productdtos)
        {
            return CreateActionResult(await _service.AddRangeAsync(productdtos));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO productdto)
        {
            return CreateActionResult(await _service.UpdateAsync(productdto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _service.DeleteAsync(id));
        }

        [HttpDelete("DeleteRange")]
        public async Task<IActionResult> DeleteRange(List<int> ids)
        {
            return CreateActionResult(await _service.DeleteRangeAsync(ids));
        }

        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _service.AnyAsync(x => x.Id == id));
        }

        [HttpGet("GetWithProductFeature/{id}")]
        public async Task<IActionResult> GetWithProductFeature(int id)
        {
            return CreateActionResult(await _service.GetWithProductFeature(id));
        }

        [HttpGet("GetAllWithProductFeature")]
        public async Task<IActionResult> GetAllWithProductFeature()
        {
            return CreateActionResult(await _service.GetAllWithProductFeature());
        }
    }
}
