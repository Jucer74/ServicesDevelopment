using System;

namespace Pricat.Utilities.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}