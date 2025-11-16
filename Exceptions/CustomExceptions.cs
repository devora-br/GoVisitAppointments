namespace GoVisit.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }

    public class ValidationException(string message) : Exception(message)
    {
    }

    public class ArgumentException(string message) : Exception(message)
    {
    }

    public class InvalidOperationException(string message) : Exception(message)
    {
    }

}
