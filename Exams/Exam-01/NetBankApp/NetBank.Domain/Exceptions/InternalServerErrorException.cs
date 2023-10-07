<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace NetBank.Domain.Exceptions
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