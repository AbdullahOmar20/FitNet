

namespace Core.Entities.OrderAggregate
{
    public class Order:BaseEntity
    {
        public Order(IReadOnlyList<OrderItem> orderItems,string buyerEmail, Address shiptoAddress,
         DeliveryMethod deliveryMethod,
         decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shiptoAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }
        public Order()
        {
            
        }
        public string BuyerEmail { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.UtcNow;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;        
        public string PaymentIntentId { get; set; }
        public decimal GetTotal()
        {
                return SubTotal + DeliveryMethod.Price;
        }
    }
}