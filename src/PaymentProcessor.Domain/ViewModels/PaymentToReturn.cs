using PaymentProcessor.Domain.Enums;
using System;

namespace PaymentProcessor.Domain.ViewModels
{
    public class PaymentToReturn
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public PaymentState Status { get; set; }
    }
}
