
using Core.Entities.OrderAggregate;

namespace Core.Specification
{
    public class OrdersWithItemAndDeliverySepcification : BaseSpecification<Order>
    {
        public OrdersWithItemAndDeliverySepcification(string email) : base(o=> o.BuyerEmail == email)
        {
            AddInclude(o=>o.OrderItems);
            AddInclude(o=>o.DeliveryMethod);
            AddOrderByDesc(o=>o.OrderTime);
        }
        public OrdersWithItemAndDeliverySepcification(int id, string email) 
        : base(o=>o.Id==id && o.BuyerEmail==email)
        {
            AddInclude(o=>o.OrderItems);
            AddInclude(o=>o.DeliveryMethod);
        }
    }
}