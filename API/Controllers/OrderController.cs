
using System.Security.Cryptography.X509Certificates;
using API.DTO;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO OrderDto)
        {
            var email = HttpContext.User.RetrieveEmail();
            var address = _mapper.Map<AddressDTO, Address>(OrderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email,  OrderDto.BasketId,OrderDto.DeliveryMethodId, address);
            if(order == null)
            {
                return BadRequest(new APIResponse(400,"problem creating order"));
            }
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetUserOrders()
        {
            var email = HttpContext.User.RetrieveEmail();
            var orders = await _orderService.GetUserOrdersAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(int id)
        {
            var email = HttpContext.User.RetrieveEmail();
            var order = await _orderService.GetOrderByIdAsync(email,id);
            if(order == null) return NotFound(new APIResponse(404));
            return _mapper.Map<OrderToReturnDTO>(order);
        }
        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var methods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(methods);
        }
    }
}