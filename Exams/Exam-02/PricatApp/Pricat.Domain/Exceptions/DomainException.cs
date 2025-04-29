namespace Pricat.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string ErrorCode { get; }

        public DomainException() : this("DOMAIN_ERROR", "Ocurrió un error en el dominio")
        {
        }

        public DomainException(string message) : this("DOMAIN_ERROR", message)
        {
        }

        public DomainException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public DomainException(string message, Exception inner)
            : this("DOMAIN_ERROR", message, inner) { }

        public DomainException(string errorCode, string message, Exception inner)
            : base(message, inner)
        {
            ErrorCode = errorCode;
        }
    }
}