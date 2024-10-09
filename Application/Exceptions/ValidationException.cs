namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException():base("Erro de validación") { }

        public ValidationException(string error) : base(error) { }
        
    }
}
