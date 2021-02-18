using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain.ViewModels
{
    public class ActionResponse<T> where T: class
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class PaymentDTO
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public PaymentDTO()
        {
            Amount = Amount + 0.0m;
        }
    }
}
