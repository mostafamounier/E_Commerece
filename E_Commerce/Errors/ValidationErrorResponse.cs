namespace E_Commerce.Errors
{
    public class ValidationErrorResponse :ApiErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ValidationErrorResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
