using Core.Entities.OrderAggregate;

namespace API.DTO
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.UtcNow;
        public Address ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDTO> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string OrderStatus { get; set; }        
        public decimal Total { get; set; }
    }
}