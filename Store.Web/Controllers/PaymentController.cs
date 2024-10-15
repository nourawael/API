using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.PaymentService;

namespace Store.Web.Controllers
{
    
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
            => Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));


    }
}
