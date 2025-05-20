namespace MoneyBankService.Application.Common
{
    public class ServiceResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        protected ServiceResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static ServiceResult Success() => new ServiceResult(true, string.Empty);
        public static ServiceResult Fail(string message) => new ServiceResult(false, message);
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; }

        protected ServiceResult(T data, bool isSuccess, string message)
            : base(isSuccess, message)
        {
            Data = data!; 
        }

        public static ServiceResult<T> Success(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "El dato no puede ser nulo para un resultado exitoso");

            return new ServiceResult<T>(data, true, string.Empty);
        }

        public static new ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T>(default!, false, message ?? "Error no especificado");
        }
    }
}