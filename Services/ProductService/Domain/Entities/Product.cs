using ProductService.Domain.Entities.Base;
using ProductService.Domain.Enums;
using System;

namespace ProductService.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }

        public ProductCategory Category { get; set; } = ProductCategory.Default;

        public string DisplayInfo => $"{Name} ({Category}) - {UnitPrice:C}";
    }

    

}
