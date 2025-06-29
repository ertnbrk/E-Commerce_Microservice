namespace ShippingService.Domain.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = null!; // Event type: "OrderCreatedEvent"
        public string Content { get; set; } = null!; // JSON serialized body
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
        public bool IsPublished { get; set; } = false;
        public DateTime? PublishedOn { get; set; }
        //public bool IsProcessed { get; set; } // bu alan olmalı

        public void MarkAsProcessed()
        {
            IsPublished = true;
            ProcessedAt = DateTime.UtcNow;
            PublishedOn = DateTime.UtcNow;
        }
    }
}
