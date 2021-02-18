using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Repository.Interfaces;
using PaymentProcessor.Service.BL.Implementations;
using PaymentProcessor.Service.BL.Interfaces;
using PaymentProcessor.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentProcessor.Tests.ControllerTests
{
    [Trait("Category", "PaymentController")]
    public class PaymentControllerShould
    {
        private readonly Mock<IPaymentService> _paymentService = new Mock<IPaymentService>();
        private readonly PaymentController _paymentController;

        public PaymentControllerShould()
        {
            _paymentController = new PaymentController(_paymentService.Object);
        }

        [Fact]
        public async Task ProcessPayment()
        {
            var request = new PaymentDTO()
            {
                Amount = 29.39m,
                CardHolder = "Adesokan Toyeeb",
                CreditCardNumber = "5323292829282928",
                ExpiryDate = DateTime.Parse("11/01/2021"),
                SecurityCode = "746"
            };

            var mockedResponse = new ActionResponse<PaymentToReturn>();

            _paymentService.Setup(ps => ps.Pay(request)).ReturnsAsync(mockedResponse);

            var response = await _paymentController.ProcessPayment(request);
            
            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<ActionResponse<PaymentToReturn>>(okResult.Value);
            Assert.NotNull(okResult.Value);
            
        }
    }
}
