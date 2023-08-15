using System.Runtime.Serialization;


namespace Students.Domain.Exceptions;

[Serializable]
public class BadRequestException:Exception
{
    public BadRequestException() { }

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, Exception innerException) : base(message, innerException) {}

    public BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
