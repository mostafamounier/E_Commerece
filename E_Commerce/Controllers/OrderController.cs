using E_Commerce.Dtos;
using E_Commerce.Errors;
using Order = E_Commerece.Core.Models.Order_Aggreation.Order;
using E_Commerece.Core.Repositories;
using E_Commerece.Repository.Repositoriees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Collections.Specialized;
using System.Security.Claims;
using E_Commerece.Core.Models.Order_Aggreation;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService OrderService)
        {
            orderService = OrderService;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            string Email=User.FindFirstValue(ClaimTypes.Email);

            var Order =await orderService.CreateOrderAsync(Email, orderDto.BasketId, orderDto.deliveryMethodId, orderDto.ShippingAddress);
            if (Order is null)
                return BadRequest(new ApiErrorResponse(400));
            return Ok(Order);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetUserOrders()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderService.GetUserOrdersAsync(Email);
            if (orders is null)
                return BadRequest(new ApiErrorResponse(400));

            return Ok(orders);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderWithId(int id)
        {
            var Email= User.FindFirstValue(ClaimTypes.Email);
            var Order=await orderService.GetOrderByIDAsync(id, Email);
            if (Order is null)
                return BadRequest(new ApiErrorResponse(400));

            return Ok(Order);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetAllDeliveryMethods()
        {
           var Dmethods= await orderService.GetAllDeliveryMethods();
            if(Dmethods is null)
                return NotFound(new ApiErrorResponse(404));
            return Ok(Dmethods);
        }
        
    }
}
