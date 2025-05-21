using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using System.Net.Http;
using static OrderService.Models.OrdersStatus;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrdersController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            Console.WriteLine("GetAll endpoint called!");

            var orders = await _context.Orders.ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            // 1. Kullanıcı var mı kontrol et
            var userCheck = await client.GetAsync($"http://userservice/api/users/{dto.UserId}");
            if (!userCheck.IsSuccessStatusCode)
                return BadRequest("Kullanıcı bulunamadı.");

            // 2. Ürün var mı, bilgileri al
            var productResponse = await client.GetFromJsonAsync<ProductDto>($"http://productservice/api/products/{dto.ProductId}");
            if (productResponse == null)
                return BadRequest("Ürün bulunamadı.");

            // 3. Sipariş oluştur
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                TotalPrice = dto.Quantity * dto.UnitPrice,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Notes = dto.Notes,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] OrderStatusUpdateDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            if (!Enum.IsDefined(typeof(OrderStatus), dto.Status))
                return BadRequest("Geçersiz durum değeri.");

            order.Status = dto.Status;

            await _context.SaveChangesAsync(); // ModifiedAt güncellenir

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            if (!Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
                return BadRequest("Geçersiz sipariş durumu.");

            var orders = await _context.Orders
                .Where(o => o.Status == parsedStatus)
                .ToListAsync();

            return Ok(orders);
        }

        //User

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("product/{productId:guid}")]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            var orders = await _context.Orders
                .Where(o => o.ProductId == productId)
                .ToListAsync();

            return Ok(orders);
        }


    }
}
