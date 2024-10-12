using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input);
        Task<OrderDetailsDto> UpdateOrderPaymentSucceded(string paymentIntentId);
        Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
