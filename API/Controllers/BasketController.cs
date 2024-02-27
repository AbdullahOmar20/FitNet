
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepo;

        public BasketController(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        } 
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {

            var basketUpdated = await _basketRepo.UpdateBasketAsync(basket);
            return Ok(basketUpdated);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeletBasket(string id)
        {
            return await _basketRepo.DeleteBaketAsync(id);
        }
    }
}