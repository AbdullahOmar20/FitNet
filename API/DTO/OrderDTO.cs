
namespace API.DTO
{
    public class OrderDTO
    {   
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDTO ShipToAddress { get; set; }

    }
}