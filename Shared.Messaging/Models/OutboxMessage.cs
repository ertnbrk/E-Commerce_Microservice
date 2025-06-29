namespace Shared.Messaging.Models
{
    public class OutboxMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = null!; // Event tipi: "OrderCreatedEvent"
        public string Content { get; set; } = null!; // JSON serialized body
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; } // null ise henüz gönderilmedi
        public bool IsProcessed { get; set; } = false;

    }
}
