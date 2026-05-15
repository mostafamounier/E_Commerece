using AutoMapper;
using E_Commerce.Dtos;
using E_Commerce.Errors;
using E_Commerece.Core.Services;
using E_Commerece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.Controllers
{

    public class PaymentsController : ApiControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;

        private readonly IConfiguration _configuration;

        public PaymentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;

        }
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var basket=await paymentService.CreateOrUpdatePaymentIntent(BasketId);
            var basketdto=mapper.Map<CustomerBasketDto>(basket);
            if (basket == null)
                return BadRequest(new ApiErrorResponse(400, "A Problem With Your Baset"));

            return Ok(basketdto);
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body)
                .ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _configuration["StripeSettings:WebhookSecret"]
                );

                // Payment Success
                if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    var paymentIntent =
                        stripeEvent.Data.Object as PaymentIntent;


                    var paymentIntentId = paymentIntent.Id;
                    await paymentService.UpdateOrderStastusSuccessOrFailedAsync(paymentIntentId, true);

                    Console.WriteLine($"Payment Success: {paymentIntentId}");

                    // TODO:
                    // هات الاوردر من الداتابيز باستخدام PaymentIntentId
                    // وغير حالته Paid
                }

                // Payment Failed
                else if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    var paymentIntent =
                        stripeEvent.Data.Object as PaymentIntent;

                    var paymentIntentId = paymentIntent.Id;
                    paymentService.UpdateOrderStastusSuccessOrFailedAsync(paymentIntentId, false);
                    Console.WriteLine($"Payment Failed: {paymentIntentId}");

                    // TODO:
                    // غير حالة الاوردر Failed
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe Error: {ex.Message}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest();
            }
        }


    }
}
