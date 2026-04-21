namespace E_Commerce.Errors
{
    public class ApiExceptionResponse : ApiErrorResponse
    {
        public string StackTrace { get; set; }
        public ApiExceptionResponse(int status , string? message =null, string ? stackTrace=null) : base(status, message)
        {
            StackTrace = stackTrace;
        }
    }
}
