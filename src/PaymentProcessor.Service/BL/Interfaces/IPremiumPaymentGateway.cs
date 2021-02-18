using PaymentProcessor.Domain.Enitities;
using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Service.Helpers;
using System.Threading.Tasks;

namespace PaymentProcessor.Service.BL.Interfaces
{
    public interface IPremiumPaymentGateway : IAutoDependencyService
    {
        ActionResponse<PaymentToReturn> MakePayment(Payment payment);
        bool IsGatewayAvailable();
    }

}
