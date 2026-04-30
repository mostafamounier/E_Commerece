using E_Commerce.Errors;
using E_Commerece.Core.Models;
using E_Commerece.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    
    public class BasketController :ApiControllerBase
    {
        private readonly IBasketRepository basketRepo;

        public BasketController(IBasketRepository BasketRepo)
        {
            basketRepo = BasketRepo;
        }
        [HttpPost("Id")]
        public async Task <ActionResult<CustomerBasket>> GetBasketById(string Id) { 
        
        
            var basket =await basketRepo.GetBasket(Id);
            if (basket == null)
                return new CustomerBasket(Id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var basketupdate = await basketRepo.UpdateBasket(basket);
            if (basketupdate == null)
                return BadRequest(new ApiErrorResponse(400));
            return Ok(basketupdate);

        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Id)
        {
            return await basketRepo.DeleteBasket(Id);
        }
    }
}
