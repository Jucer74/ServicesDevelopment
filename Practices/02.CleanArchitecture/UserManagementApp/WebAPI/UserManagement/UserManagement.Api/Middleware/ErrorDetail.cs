using Newtonsoft.Json;

namespace UserManagement.Api.Middleware
{
    public class ErrorDetail
    {
        public string ErrorType { get; set; } = null!;

        /// <summary>
        /// Message Error or Exception Message
        /// </summary>
        public List<string> Errors { get; set; } = null!;

        /// <summary>
        /// Serialize the Object to response the Details
        /// </summary>
        /// <returns>The Json Serialized</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}