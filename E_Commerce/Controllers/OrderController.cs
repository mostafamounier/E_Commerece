using E_Commerce.Dtos;
using E_Commerce.Errors;
using Order = E_Commerece.Core.Models.Order_Aggreation.Order;
using E_Commerece.Repository.Repositoriees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Collections.Specialized;
using System.Security.Claims;
using E_Commerece.Core.Models.Order_Aggreation;
using E_Commerece.Core.Services;
using AutoMapper;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService OrderService,IMapper mapper)
        {
            orderService = OrderService;
            this.mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<OrderReturnDto>>> GetUserOrders()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderService.GetUserOrdersAsync(Email);
            if (orders is null)
                return BadRequest(new ApiErrorResponse(400));
            var MappedOrders=mapper.Map<IEnumerable<Order>,IEnumerable<OrderReturnDto>>(orders);
            return Ok(MappedOrders);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReturnDto>> GetOrderWithId(int id)
        {
            var Email= User.FindFirstValue(ClaimTypes.Email);
            var Order=await orderService.GetOrderByIDAsync(id, Email);
            if (Order is null)
                return BadRequest(new ApiErrorResponse(400));
            var MappOrder = mapper.Map<Order,OrderReturnDto>(Order);

            return Ok(MappOrder);
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
