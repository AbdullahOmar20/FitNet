
using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        } 
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            var customerbasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);
            
            var basketUpdated = await _basketRepo.UpdateBasketAsync(customerbasket);
            return Ok(basketUpdated);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeletBasket(string id)
        {
            return await _basketRepo.DeleteBaketAsync(id);
        }
    }
}