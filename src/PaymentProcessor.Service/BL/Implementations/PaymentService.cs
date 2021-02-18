using AutoMapper;
using Microsoft.Extensions.Logging;
using PaymentProcessor.Domain.Enitities;
using PaymentProcessor.Domain.Enums;
using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Repository.Interfaces;
using PaymentProcessor.Service.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Service.BL.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly ICheapPaymentGateway _cheapPayment;
        private readonly IExpensivePaymentGateway _expensivePayment;
        private readonly IPremiumPaymentGateway _premiumPayment;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(ICheapPaymentGateway cheapPayment, IExpensivePaymentGateway expensivePayment, IPremiumPaymentGateway premiumPayment, IRepository repository, ILogger<PaymentService> logger, IMapper mapper)
        {
            _cheapPayment = cheapPayment;
            _expensivePayment = expensivePayment;
            _premiumPayment = premiumPayment;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ActionResponse<PaymentToReturn>> Pay(PaymentDTO request)
        {
            var paymentResponse = new ActionResponse<PaymentToReturn>()
            { 
                Data = new PaymentToReturn()
            };

            try
            {
                var payment = new Payment
                {
                    Amount = request.Amount,
                    CardHolder = request.CardHolder,
                    CreatedDate = DateTime.Now,
                    ExpiryDate = request.ExpiryDate,
                    CreditCardNumber = request.CreditCardNumber,
                    ID = Guid.NewGuid(),
                    LastModifiedDate = DateTime.Now,
                    SecurityCode = request.SecurityCode,
                    Status = PaymentState.Pending
                };

                _repository.Add(payment);

                if(await _repository.SaveChanges())
                {
                    if(payment.Amount <= 20)
                    {
                        if (_cheapPayment.IsGatewayAvailable())
                            paymentResponse = _cheapPayment.MakePayment(payment);
                    } 
                    else if (payment.Amount <= 500)
                    {
                        if (_expensivePayment.IsGatewayAvailable())
                            paymentResponse = _expensivePayment.MakePayment(payment);
                        else
                        {
                            if (_cheapPayment.IsGatewayAvailable())
                                paymentResponse = _cheapPayment.MakePayment(payment);
                        }
                    } 
                    else if(payment.Amount > 500)
                    {
                        if (_premiumPayment.IsGatewayAvailable())
                            paymentResponse = _premiumPayment.MakePayment(payment);
                        else
                        {
                            int count = 0;
                            while(count < 3)
                            {
                                if (_premiumPayment.IsGatewayAvailable())
                                    paymentResponse = _premiumPayment.MakePayment(payment);
                                else count++;
                            }
                        }
                    }
                    else
                    {
                        // Do nothing
                    }
                }

                if(paymentResponse.Status)
                    payment.Status = PaymentState.Processed;
                else payment.Status = PaymentState.Failed;
            
                _repository.Update(payment);
                await _repository.SaveChanges();

                paymentResponse.Data = _mapper.Map<PaymentToReturn>(payment);

                return paymentResponse;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return paymentResponse;
            }
        }
    }
}
