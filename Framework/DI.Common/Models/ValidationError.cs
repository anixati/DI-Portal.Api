namespace DI.Models
{
    public class ValidationError
    {
        public ValidationError(string message, string name = "")
        {
            Name = name;
            Message = message;
        }

        public ValidationError(string message, int code = 0)
        {
            Code = code;
            Message = message;
        }

        public string Name { get; }
        public int Code { get; }
        public string Message { get; }
    }
}