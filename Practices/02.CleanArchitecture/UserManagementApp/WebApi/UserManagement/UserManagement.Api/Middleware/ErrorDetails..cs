using Newtonsoft.Json;

namespace UserManagement.Api.Middleware
{
    public class ErrorDetails
    {
        /// <summary>
        /// Error Type Description
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// Mesage Error or Exception Message
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Serialize the Objet to response the Details
        /// </summary>
        /// <returns>The Json Serialized</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
