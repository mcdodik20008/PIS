namespace PISWF.infrasrtucture.guard;

public class ErrorQueue
{
    private Queue<Exception> exceptions = new();

    public void Enqueue(Exception exception)
    {
        exceptions.Enqueue(exception);
    }

    public Exception Dequeue()
    {
        return exceptions.Dequeue();
    }

    public int Size
    {
        get => exceptions.Count;
    }
}