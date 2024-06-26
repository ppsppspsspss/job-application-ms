namespace job_application_management_system_api.Utils
{
    public class Result<T>
    {
        public bool IsError { get; set; }
        public List<string> Messages { get; set; }
        public T? Data { get; set; }
        public Result(bool isError, List<string> messages, T? data)
        {
            IsError = isError;
            Messages = messages;
            Data = data;
        }
    }
}
