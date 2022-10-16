using System.Runtime.CompilerServices;

namespace PISWF.infrasrtucture.guard;

public class Guard
{
    private ErrorQueue _errorQueue { get; }

    public Guard(ErrorQueue errorQueue)
    {
        _errorQueue = errorQueue;
    }

    public void ThtowIfNull(object? arg,
        [CallerArgumentExpression("arg")] string param = "")
    {
        if (arg is null)
            _errorQueue.Enqueue(new ArgumentException("Аргумент не может быть null", param));
    }
}