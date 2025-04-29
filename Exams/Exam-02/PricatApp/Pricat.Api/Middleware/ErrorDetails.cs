using Newtonsoft.Json;

namespace Pricat.Api.Middleware
{
    /// <summary>
    /// Error details with the status code and the error or exception message.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Description of the error type (e.g., "Bad Request").
        /// </summary>
        public string ErrorType { get; set; } = null!;

        /// <summary>
        /// List of error messages or exception messages.
        /// </summary>
        public List<string> Errors { get; set; } = null!;

        /// <summary>
        /// Serialize the object to JSON to return in the response.
        /// </summary>
        /// <returns>The serialized JSON string.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
            });
        }
    }
}
