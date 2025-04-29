using System;

namespace Pricat.Utilities.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}