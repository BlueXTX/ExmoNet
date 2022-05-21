namespace ExmoNet.Application.Exceptions;

public class ResponseToJsonException : Exception
{
    public ResponseToJsonException()
    {
    }

    public ResponseToJsonException(string message) : base(message)
    {
    }
}
