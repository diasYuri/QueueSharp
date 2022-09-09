using QueueSharp.Application.Model;

namespace QueueSharp.Application.Adapters;

public interface IPersistenceAdapter
{
    Task Enqueue(string topic, QModel model);
    Task<QModel?> Dequeue(string topic);
}