using Newtonsoft.Json;

namespace MoneyBankService.Api.Middleware;

public class ErrorDetails
{
    public string ErrorType { get; set; } = null!;

    public List<string> Errors { get; set; } = null!;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}