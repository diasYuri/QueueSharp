using System.Runtime.CompilerServices;
using Grpc.Core;
using QueueSharp.Application;
using QueueSharp.Application.Model;
using QueueSharp.gRPC.Server;

namespace QueueSharp.gRPC.Server.Services;

public class QueueService : Queue.QueueBase
{
    private readonly IQueueSharpApplication _queueSharpApplication;

    public QueueService(IQueueSharpApplication queueSharpApplication)
    {
        _queueSharpApplication = queueSharpApplication;
    }
    public override async Task<DequeueReply> Dequeue(DequeueRequest request, ServerCallContext context)
    {
        var message = await _queueSharpApplication.Dequeue(request.Topic);

        if (message.Data is not null)
        {
            return new DequeueReply
            {
                Success = true,
                Id = message.Data.Id,
                Payload = message.Data.Payload
            };
        }
        
        return new DequeueReply
        {
            Success = false
        };
    }

    public override async Task<EnqueueReply> Enqueue(EnqueueRequest request, ServerCallContext context)
    {
        await _queueSharpApplication.Enqueue(request.Topic, new QModel(request.Payload));
        return new EnqueueReply
        {
            Success = true
        };
    }
}
