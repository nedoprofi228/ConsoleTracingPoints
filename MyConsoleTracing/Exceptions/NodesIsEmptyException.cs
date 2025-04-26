namespace MyConsoleTracing.Exceptions;

public class NodesIsEmptyException : Exception
{
    public NodesIsEmptyException(string message):base(message) { }
}