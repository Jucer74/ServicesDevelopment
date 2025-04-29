using System.Runtime.Serialization;

namespace Pricat.Application.Exceptions;

public class NotFoundException : BusinessException
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    // Without this constructor, deserialization will fail
    [Obsolete("Obsolete")]
    protected NotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}