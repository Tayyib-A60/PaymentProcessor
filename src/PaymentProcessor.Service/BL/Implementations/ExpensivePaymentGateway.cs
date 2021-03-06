﻿using AutoMapper;
using PaymentProcessor.Domain.Constants;
using PaymentProcessor.Domain.Enitities;
using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Repository.Interfaces;
using PaymentProcessor.Service.BL.Interfaces;
using System;
using System.Threading.Tasks;

namespace PaymentProcessor.Service.BL.Implementations
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        public ExpensivePaymentGateway()
        {
        }

        public bool IsGatewayAvailable()
        {
            Random rand = new Random();
            var num = rand.Next(0, 10);

            return num > 5;
        }

        public ActionResponse<PaymentToReturn> MakePayment(Payment payment)
        {
            var response = new ActionResponse<PaymentToReturn>();
            
            response.Status = true;
            response.Message = ResponseMessages.OperationSuccessful;

            return response;

        }
    }
}
