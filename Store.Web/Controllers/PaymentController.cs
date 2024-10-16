using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.PaymentService;
using Stripe;

namespace Store.Web.Controllers
{
    
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        const string endpointSecret = "whsec_f1e005369ebf5b5269aa00021ce0b91d92dd3e545814f007235c4683328a93d9";

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
            => Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                PaymentIntent paymentIntent;
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Failed : ", paymentIntent.Id);
                    var order=await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("Order Updated to Payment Failed : ", order.Id);

                }
                else if (stripeEvent.Type == "payment_intent.succeedded")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Succeeded : ", paymentIntent.Id);
                    var order = await _paymentService.UpdateOrderPaymentSucceded(paymentIntent.Id);
                    _logger.LogInformation("Order Updated to Payment Succeeded : ", order.Id);

                }
                else if (stripeEvent.Type == "payment_intent.created")
                {
                    _logger.LogInformation("Payment Created  ");

                }
                else
                {
                    Console.WriteLine("Unhandled Event type", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }


    }
}
