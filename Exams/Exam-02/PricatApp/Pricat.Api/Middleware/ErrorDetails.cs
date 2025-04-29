using Newtonsoft.Json;

namespace Pricat.Api.Middleware;

public class ErrorDetails
{
    public string ErrorType { get; set; }
    public List<string> Errors { get; set; }
    
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}