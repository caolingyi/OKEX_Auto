using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKEX.Auto.TradeApi.Infrastructure.Exceptions
{
    public class BIBIRequestCreateOrderDomainException : Exception
    {
        public BIBIRequestCreateOrderDomainException()
        { }

        public BIBIRequestCreateOrderDomainException(string message)
            : base(message)
        { }

        public BIBIRequestCreateOrderDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
