using ProductService.Domain.Enums;

namespace ProductService.Application.DTOs
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public ProductCategory Category { get; set; }
        public bool IsActive { get; set; }
    }
}
