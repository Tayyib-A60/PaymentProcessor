using AutoMapper;
using PaymentProcessor.Domain.Enitities;
using PaymentProcessor.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Service.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Payment, PaymentToReturn>();
            CreateMap<PaymentDTO, Payment>();
        }
    }
}
