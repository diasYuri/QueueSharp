using Microsoft.Extensions.DependencyInjection;
using QueueSharp.Application.Adapters;
using QueueSharp.Application.Model;

namespace QueueSharp.Application;

public class QueueSharpApplication : IQueueSharpApplication
{
    private readonly IPersistenceAdapter _persistenceAdapter;

    public QueueSharpApplication(IPersistenceAdapter persistenceAdapter)
    {
        _persistenceAdapter = persistenceAdapter;
    }
    public async Task<ApplicationResponse<Empty>> Enqueue(string topic, QModel model)
    {
        await _persistenceAdapter.Enqueue(topic, model);
        return new ApplicationResponse<Empty>(Empty.Unit);
    }

    public async Task<ApplicationResponse<QModel>> Dequeue(string topic)
    {
        var result = await _persistenceAdapter.Dequeue(topic);
        return new ApplicationResponse<QModel>(result);
    }
}

public interface IQueueSharpApplication
{
    Task<ApplicationResponse<Empty>> Enqueue(string topic, QModel model);
    Task<ApplicationResponse<QModel>> Dequeue(string topic);
}


public static class QueueSharpApplicationExtensions
{
    public static IServiceCollection AddQueueSharpApplication(this IServiceCollection services)
    {
        return services.AddSingleton<IQueueSharpApplication, QueueSharpApplication>();
    } 
}