using Microsoft.AspNetCore.Mvc;
using ShippingService.Application.Interfaces;
using ShippingService.Application.UseCases;
using ShippingService.Domain.Entities;
using ShippingService.Application.DTOs;
namespace ShippingService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly IGetAllShipmentsUseCase _getAllUseCase;
    private readonly ICreateShipmentUseCase _createShipment;

    public ShipmentsController(IGetAllShipmentsUseCase getAllUseCase, ICreateShipmentUseCase createShipment)
    {
        _getAllUseCase = getAllUseCase;
        _createShipment = createShipment;
    }

    [HttpGet]
    public async Task<ActionResult<List<Shipment>>> GetAll()
    {
        var shipments = await _getAllUseCase.ExecuteAsync();
        return Ok(shipments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShipmentDto dto)
    {
        await _createShipment.ExecuteAsync(dto);
        return Ok("Kargo oluşturuldu.");
    }
}
