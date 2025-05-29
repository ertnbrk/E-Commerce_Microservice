using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Enums;
using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrderUseCase _createOrderUseCase;
        private readonly IGetAllOrdersUseCase _getAllOrdersUseCase;
        private readonly IGetOrderByIdUseCase _getOrderByIdUseCase;
        private readonly IGetOrdersByStatusUseCase _getOrdersByStatusUseCase;
        private readonly IGetOrdersByUserIdUseCase _getOrdersByUserIdUseCase;
        private readonly IGetOrdersByProductIdUseCase _getOrdersByProductIdUseCase;
        private readonly IUpdateOrderStatusUseCase _updateOrderStatusUseCase;
        private readonly IDeleteOrderUseCase _deleteOrderUseCase;

        public OrdersController(
            ICreateOrderUseCase createOrderUseCase,
            IGetAllOrdersUseCase getAllOrdersUseCase,
            IGetOrderByIdUseCase getOrderByIdUseCase,
            IGetOrdersByStatusUseCase getOrdersByStatusUseCase,
            IGetOrdersByUserIdUseCase getOrdersByUserIdUseCase,
            IGetOrdersByProductIdUseCase getOrdersByProductIdUseCase,
            IUpdateOrderStatusUseCase updateOrderStatusUseCase,
            IDeleteOrderUseCase deleteOrderUseCase)
        {
            _createOrderUseCase = createOrderUseCase;
            _getAllOrdersUseCase = getAllOrdersUseCase;
            _getOrderByIdUseCase = getOrderByIdUseCase;
            _getOrdersByStatusUseCase = getOrdersByStatusUseCase;
            _getOrdersByUserIdUseCase = getOrdersByUserIdUseCase;
            _getOrdersByProductIdUseCase = getOrdersByProductIdUseCase;
            _updateOrderStatusUseCase = updateOrderStatusUseCase;
            _deleteOrderUseCase = deleteOrderUseCase;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _getAllOrdersUseCase.ExecuteAsync();
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _getOrderByIdUseCase.ExecuteAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            var userIdClaim = User.FindFirst("sub")?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var orderId = await _createOrderUseCase.ExecuteAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, null);
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] OrderStatusUpdateDto dto)
        {
            var result = await _updateOrderStatusUseCase.ExecuteAsync(id, dto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _deleteOrderUseCase.ExecuteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            if (!Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
                return BadRequest("Geçersiz sipariş durumu.");

            var orders = await _getOrdersByStatusUseCase.ExecuteAsync(parsedStatus);
            return Ok(orders);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var orders = await _getOrdersByUserIdUseCase.ExecuteAsync(userId);
            return Ok(orders);
        }

        [HttpGet("product/{productId:guid}")]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            var orders = await _getOrdersByProductIdUseCase.ExecuteAsync(productId);
            return Ok(orders);
        }
        [HttpGet("throw")]
        public IActionResult ThrowTestException()
        {
            throw new Exception("Test exception from UserService.");
        }

    }
}
