using PaymentProcessor.Domain.Enitities;
using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Service.BL.Interfaces
{
    public interface ICheapPaymentGateway : IAutoDependencyService
    {
        ActionResponse<PaymentToReturn> MakePayment(Payment payment);
        bool IsGatewayAvailable();
    }

}
