using System.Collections.Generic;

namespace Pricat.Api.Middleware
{
    public class ErrorDetails
    {
        public string ErrorType { get; set; } = null!;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
