using PaymentProcessor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain.Enitities
{
    public class Payment: BaseEntity
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public PaymentState Status { get; set; }
        public Payment()
        {
            Amount = Amount + 0.0m;
        }
    }

    
}
