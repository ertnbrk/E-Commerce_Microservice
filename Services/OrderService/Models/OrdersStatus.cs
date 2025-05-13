namespace OrderService.Models
{
    public class OrdersStatus
    {
        public enum OrderStatus
        {
            Pending,        // Sipariş oluşturuldu ama henüz onaylanmadı
            Approved,       // Sipariş onaylandı
            Shipped,        // Kargoya verildi
            Delivered,      // Teslim edildi
            Cancelled       // İptal edildi
        }

    }
}
