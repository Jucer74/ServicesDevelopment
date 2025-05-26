using Newtonsoft.Json;

namespace MoneyBankService.Api.Middleware
{
    public class ErrorDetails
    {
        /// <summary>
        /// Descripción del tipo de error
        /// </summary>
        public string ErrorType { get; set; } = null!; // Inicializado con string vacío

        /// <summary>
        /// Lista de mensajes de error o excepciones
        /// </summary>
        public List<string> Errors { get; set; } = null!; // Inicializado con una lista vacía

        /// <summary>
        /// Serializa el objeto a JSON para devolver los detalles
        /// </summary>
        /// <returns>El objeto serializado en formato JSON</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
