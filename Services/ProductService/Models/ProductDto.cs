namespace ProductService.Models
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
