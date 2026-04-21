namespace E_Commerce.Errors
{
    public class ApiErrorResponse
    {
        public int StatusResponse { get; set; }
        public string Message { get; set; }
        public ApiErrorResponse(int status,string ? message=null)
        {
            StatusResponse = status;
            Message = message ?? GetMessage(status);
        }
        private string ? GetMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change.",
                _ => null
            };
        }


    }
}
