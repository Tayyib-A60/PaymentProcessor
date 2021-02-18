using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using PaymentProcessor.Domain.Constants;
using PaymentProcessor.Domain.Enitities;
using PaymentProcessor.Domain.Enums;
using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Repository.Interfaces;
using PaymentProcessor.Service.BL.Implementations;
using PaymentProcessor.Service.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentProcessor.Tests.ServiceTests
{
    [Trait("Category", "PaymentService")]
    public class PaymentServiceShould
    {
        private IPaymentService _paymentService;

        private readonly Mock<ICheapPaymentGateway> _cheapPayment = new Mock<ICheapPaymentGateway>();
        private readonly Mock<IExpensivePaymentGateway> _expensivePayment = new Mock<IExpensivePaymentGateway>();
        private readonly Mock<IPremiumPaymentGateway> _premiumPayment = new Mock<IPremiumPaymentGateway>();
        private readonly Mock<IRepository> _repository = new Mock<IRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ILogger<PaymentService>> _logger = new Mock<ILogger<PaymentService>>();

        public PaymentServiceShould()
        {
            _paymentService = new PaymentService(_cheapPayment.Object, _expensivePayment.Object, _premiumPayment.Object, _repository.Object,
                _logger.Object, _mapper.Object);
        }

        [Fact]
        public async Task MakePayment()
        {
            var request = new PaymentDTO()
            {
                Amount = 29.39m,
                CardHolder = "Adesokan Toyeeb",
                CreditCardNumber = "5323292829282928",
                ExpiryDate = DateTime.Parse("11/01/2021"),
                SecurityCode = "746"
            };

            _repository.Setup(rep => rep.Add(new Payment())).Verifiable();
            _repository.Setup(rep => rep.SaveChanges()).ReturnsAsync(true);
            _repository.Setup(rep => rep.Update(new Payment())).Verifiable();

            var mockedResponse = new ActionResponse<PaymentToReturn>()
            {
                Message = ResponseMessages.OperationSuccessful,
                Status = true
            };
            _cheapPayment.Setup(cp => cp.IsGatewayAvailable()).Returns(true);
            _cheapPayment.Setup(cp => cp.MakePayment(It.IsAny<Payment>())).Returns(mockedResponse);

            var response = await _paymentService.Pay(request);

            Assert.IsType<ActionResponse<PaymentToReturn>>(response);
            Assert.True(response.Status);
        }
    }
}
