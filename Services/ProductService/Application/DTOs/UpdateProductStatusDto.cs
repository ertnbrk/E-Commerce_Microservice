namespace ProductService.Application.DTOs
{
    public class UpdateProductStatusDto
    {
        public bool? IsActive { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}