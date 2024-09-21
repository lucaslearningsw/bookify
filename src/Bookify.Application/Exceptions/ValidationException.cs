
namespace Bookify.Application.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> erros)
    {
        Erros = erros;
    }
    public IEnumerable<ValidationError> Erros { get; }
}
