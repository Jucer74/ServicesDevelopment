using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBankService.Application.Exceptions
{
    public class InvalidTransactionException : ApplicationException
    {
        public InvalidTransactionException(string message) : base(message) { }
    }
}

