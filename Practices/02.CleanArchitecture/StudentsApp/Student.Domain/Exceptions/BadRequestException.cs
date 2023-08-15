using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Student.Domain.Exceptions
{
    [Serializable]
    public class BadRequestException:Exception
    {
        public BadRequestException() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException) {}

        public BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
