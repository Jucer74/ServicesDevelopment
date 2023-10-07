<<<<<<< HEAD
using System.Diagnostics.CodeAnalysis;
=======
﻿using System.Diagnostics.CodeAnalysis;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
using System.Runtime.Serialization;

namespace NetBank.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class BadRequestException : BusinessException
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected BadRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}