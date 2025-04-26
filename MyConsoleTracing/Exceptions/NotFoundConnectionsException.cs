namespace MyConsoleTracing.Exceptions;

public class NotFoundConnectionsException : Exception
{
    public NotFoundConnectionsException(string message) : base(message){ }
}