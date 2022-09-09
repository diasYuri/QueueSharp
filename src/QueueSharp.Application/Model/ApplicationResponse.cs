namespace QueueSharp.Application.Model;

public class ApplicationResponse<T>
{
    public T? Data { get; }

    public ApplicationResponse(T? data)
    {
        Data = data;
    }
}

public struct Empty
{
    private static Empty _unit = new ();
    public static Empty Unit => _unit;
}
