using Newtonsoft.Json;
namespace Pricat.Api.Middleware
{
    /// <summary>
    /// Error details with the StatusCode and the Message Error or Execption Message
    /// </summary>
    public class ErrorDetails
    {
       
        public string ErrorType { get; set; } = null!;


        public List<string> Errors { get; set; } = null!;

       
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}