using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTOs;
using PaymentService.Application.Interfaces;
using PaymentService.Domain;
namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ICreatePaymentUseCase _createPaymentUseCase;
        private readonly IGetAllPaymentsUseCase _getAllPaymentsUseCase;
        private readonly IGetPaymentByIdUseCase _getPaymentByIdUseCase;
        private readonly IUpdatePaymentStatusUseCase _updatePaymentStatusUseCase;
        public PaymentsController(
            ICreatePaymentUseCase createPaymentUseCase,
            IGetAllPaymentsUseCase getAllPaymentsUseCase,
            IGetPaymentByIdUseCase getPaymentByIdUseCase,
            IUpdatePaymentStatusUseCase updatePaymentStatusUseCase)
        {
            _createPaymentUseCase = createPaymentUseCase;
            _getAllPaymentsUseCase = getAllPaymentsUseCase;
            _getPaymentByIdUseCase = getPaymentByIdUseCase;
            _updatePaymentStatusUseCase = updatePaymentStatusUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentCreateDto dto)
        {
            var id = await _createPaymentUseCase.ExecuteAsync(dto);
            return Ok(new { id });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _getAllPaymentsUseCase.ExecuteAsync();
            return Ok(payments);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _getPaymentByIdUseCase.ExecuteAsync(id);
            return payment != null ? Ok(payment) : NotFound();
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] PaymentStatusUpdateDto dto)
        {
            var result = await _updatePaymentStatusUseCase.ExecuteAsync(id, dto.NewStatus);
            return result ? NoContent() : NotFound();
        }
    }
}
