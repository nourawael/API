using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Repository.Repositories;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.OrderService.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Data.Entities.Product;

namespace Store.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;

        public PaymentService(IConfiguration configuration,
            UnitOfWork unitOfWork,
            IBasketService basketService)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            if (input is null)
                throw new Exception("Basket is empty");

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId.Value);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method is not provided");

            decimal shippingPrice = deliveryMethod.Price;

            foreach (var item in input.BasketItems) 
            {
                var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.ProductId);

                if(item.Price!= product.Price)
                    item.Price= product.Price;

            }

            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(input.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)input.BasketItems.Sum(item => (item.Quantity * item.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };

                paymentIntent = await service.CreateAsync(options);

                input.PaymentIntentId = paymentIntent.Id;
                input.ClientSecret = paymentIntent.ClientSecret;
            }
            else 
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.BasketItems.Sum(item => (item.Quantity * item.Price * 100)) + (long)(shippingPrice * 100),
                   
                };

                await service.UpdateAsync(input.PaymentIntentId,options);
            }

            await _basketService.UpdateBasketAsync(input);
            return input;
        }

        public Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetailsDto> UpdateOrderPaymentSucceded(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
