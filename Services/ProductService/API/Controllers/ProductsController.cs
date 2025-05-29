using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Persistence;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGetAllProductsUseCase _getAll;
        private readonly IGetProductByIdUseCase _getById;
        private readonly ICreateProductUseCase _create;
        private readonly IUpdateProductStatusUseCase _update;
        private readonly IDeleteProductUseCase _delete;

        public ProductsController(
            IGetAllProductsUseCase getAll,
            IGetProductByIdUseCase getById,
            ICreateProductUseCase create,
            IUpdateProductStatusUseCase update,
            IDeleteProductUseCase delete)
        {
            _getAll = getAll;
            _getById = getById;
            _create = create;
            _update = update;
            _delete = delete;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAll.ExecuteAsync();
            return Ok(result);
        }
        [HttpGet("test-error")]
        public IActionResult ThrowError()
        {
            throw new Exception("Bu test hatasıdır.");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _getById.ExecuteAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            var id = await _create.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateProductStatusDto dto)
        {
            var success = await _update.ExecuteAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _delete.ExecuteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
