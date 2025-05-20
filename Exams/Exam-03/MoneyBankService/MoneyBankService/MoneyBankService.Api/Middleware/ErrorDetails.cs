using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoneyBankService.Api.Middleware
{
    public class ErrorDetails
    {
        public string ErrorType { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();

        public ErrorDetails()
        {
        }

        public ErrorDetails(string errorType, IEnumerable<string> errors)
        {
            ErrorType = errorType;
            Errors = new List<string>(errors);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}