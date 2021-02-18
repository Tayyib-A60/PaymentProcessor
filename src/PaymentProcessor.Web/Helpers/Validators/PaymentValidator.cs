using FluentValidation;
using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Web.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentProcessor.Web.Helpers
{
    public class PaymentValidator : AbstractValidator<PaymentDTO>
    {
        private readonly IUtilities _utilities;
        public PaymentValidator(IUtilities utilities)
        {
            _utilities = utilities;
            
            RuleFor(payment => payment.Amount).GreaterThan(0);
            RuleFor(payment => payment.CreditCardNumber).MinimumLength(16).MaximumLength(19);
            RuleFor(payment => payment.CardHolder).NotNull().NotEmpty();
            RuleFor(payment => payment.SecurityCode).Must((code) => code.Length == 0 || code.Length == 3).WithMessage("Security code must be a string of 3 characters");
            RuleFor(payment => payment.ExpiryDate.Date).GreaterThan(DateTime.Now.Date).WithMessage($"Expiry date must be a later date from today:{DateTime.Now.Date}");
            RuleFor(payment => payment.CreditCardNumber).MustAsync(async (cardNumber, cancellation) => await VerifyCardDetails(cardNumber.Substring(0, 5))).WithMessage("Card Number is not valid");
        }

        private async Task<bool> VerifyCardDetails(string cardPan)
        {
            var response = await _utilities.MakeHttpRequest(null, "https://lookup.binlist.net/", cardPan, HttpMethod.Get);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
