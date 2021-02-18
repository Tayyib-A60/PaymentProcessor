using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain.Enums
{
    public enum PaymentState
    {
        Pending = 0,
        Processed = 5,
        Failed = 9
    }
}
