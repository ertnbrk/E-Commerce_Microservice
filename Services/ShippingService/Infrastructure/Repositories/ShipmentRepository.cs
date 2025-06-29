using Microsoft.EntityFrameworkCore;
using ShippingService.Application.Interfaces;
using ShippingService.Domain.Entities;
using ShippingService.Infrastructure.Data;

namespace ShippingService.Infrastructure.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly ShippingDbContext _context;

    public ShipmentRepository(ShippingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Shipment>> GetAllAsync() =>
        await _context.Shipments.ToListAsync();

    public async Task<Shipment?> GetByIdAsync(Guid id) =>
        await _context.Shipments.FindAsync(id);

    public async Task AddAsync(Shipment shipment)
    {
        await _context.Shipments.AddAsync(shipment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Shipment shipment)
    {
        _context.Shipments.Update(shipment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment != null)
        {
            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
        }
    }
}
