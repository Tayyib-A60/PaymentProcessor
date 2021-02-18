using FluentValidation;
using PaymentProcessor.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.Web.Helpers
{
    public class PaymentValidator : AbstractValidator<PaymentDTO>
    {
        public PaymentValidator()
        {
            RuleFor(payment => payment.Amount).GreaterThan(0);           
            RuleFor(payment => payment.CreditCardNumber).MinimumLength(16).MaximumLength(19);
            RuleFor(payment => payment.CardHolder).NotNull().NotEmpty();
            RuleFor(payment => payment.SecurityCode).Must((code) => code.Length == 0 || code.Length == 3).WithMessage("Security code must be a string of 3 characters");
            RuleFor(payment => payment.ExpiryDate.Date).GreaterThan(DateTime.Now.Date).WithMessage($"Expiry date must be a later date from today:{DateTime.Now.Date}");
        }
    }
}
