<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace NetBank.Domain.Exceptions
{
    /// <summary>
    /// Base Business Exception
    /// </summary>
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

        // Without this constructor, deserialization will fail
        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}