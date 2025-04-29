using Newtonsoft.Json;

namespace Pricat.Api.Middleware
{
    public class ErrorDetails
    {
        public string ErrorType { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
