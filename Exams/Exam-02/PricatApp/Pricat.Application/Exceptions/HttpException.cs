using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.Exceptions
{
    public class HttpException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }

        public HttpException(string message, int statusCode, string errorCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}