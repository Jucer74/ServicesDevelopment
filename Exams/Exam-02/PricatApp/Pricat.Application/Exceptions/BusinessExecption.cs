using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Pricat.Application.Execptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}