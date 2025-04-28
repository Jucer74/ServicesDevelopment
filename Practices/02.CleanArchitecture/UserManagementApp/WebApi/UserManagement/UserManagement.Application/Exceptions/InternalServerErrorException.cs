using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InternalServerErrorException : BusinessException
    {
        public InternalServerErrorException()
        {
        }

        public InternalServerErrorException(string message) : base(message)
        {
        }

        public InternalServerErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected InternalServerErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}