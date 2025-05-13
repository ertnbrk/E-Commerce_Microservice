using ProductService.Models.Base;
using System;

namespace ProductService.Models
{
    public class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }

        public string DisplayInfo => $"{Name} ({Category}) - {UnitPrice:C}";
    }

    public class UpdateProductStatusDto
    {
        public bool? IsActive { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
