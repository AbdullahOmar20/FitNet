using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethod, Address ShippingToAddress);
        Task<IReadOnlyList<Order>> GetUserOrdersAsync(string BuyerEmail);
        Task<Order> GetOrderByIdAsync(string BuyerEmail, int id);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync(); 
    }
}