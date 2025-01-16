namespace Business.Services
{
    public class ServiceResult
    {
        public bool Success { get; }
        public string Message { get; }
        public object Data { get; }

        public ServiceResult(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}