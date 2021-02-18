using PaymentProcessor.Domain.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.Web.SwaggerExamples.Requests
{
    public class MakePaymentExample : IExamplesProvider<PaymentDTO>
    {
        public PaymentDTO GetExamples()
        {
            return new PaymentDTO
            {
                Amount = 19.50m,
                CardHolder = "Adesokan Toyeeb",
                CreditCardNumber = "5323292829282928",
                ExpiryDate = DateTime.Parse("11/01/2021"),
                SecurityCode = "746"
            };
        }
    }
}
