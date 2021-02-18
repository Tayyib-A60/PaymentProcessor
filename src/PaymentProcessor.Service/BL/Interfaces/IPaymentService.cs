using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Service.Helpers;
using System.Threading.Tasks;

namespace PaymentProcessor.Service.BL.Interfaces
{
    public interface IPaymentService: IAutoDependencyService
    {
        Task<ActionResponse<PaymentToReturn>> Pay(PaymentDTO request);
    }

}
