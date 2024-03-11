
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;


namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {

        private readonly IBasketRepository _basketrepo;
        private readonly IUnitOfWOrk _unitOfWOrk;


        public OrderService(IBasketRepository basketrepo, IUnitOfWOrk unitOfWOrk)
        {
            _basketrepo = basketrepo;
            _unitOfWOrk = unitOfWOrk;
            
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingToAddress)
        {
            //Get basket from repo
            var basket = await _basketrepo.GetBasketAsync(BasketId);
            //get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWOrk.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered,productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            //get delivery method from repo
            var deliverymethod = await _unitOfWOrk.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            //calc subtotal 
            var subtotal = items.Sum(i => i.Price*i.Quantity);
            //create order
            var order = new Order(items, BuyerEmail, ShippingToAddress, deliverymethod, subtotal);
            //save to DB
            _unitOfWOrk.Repository<Order>().Add(order);
            var result = await _unitOfWOrk.Complete();
            if(result <= 0) return null;
            //delete basket 
            await _basketrepo.DeleteBaketAsync(BasketId);
            
            //return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWOrk.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(string BuyerEmail, int id)
        {
            var spec = new OrdersWithItemAndDeliverySepcification(id,BuyerEmail);
            return await _unitOfWOrk.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetUserOrdersAsync(string BuyerEmail)
        {
            var spec = new OrdersWithItemAndDeliverySepcification(BuyerEmail);
            return await _unitOfWOrk.Repository<Order>().ListAsync(spec);
        }
    }
}