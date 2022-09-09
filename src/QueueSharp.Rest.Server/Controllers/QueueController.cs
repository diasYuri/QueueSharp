using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QueueSharp.Application;
using QueueSharp.Application.Model;

namespace QueueSharp.Rest.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class QueueController : ControllerBase
{
    private readonly IQueueSharpApplication _queueSharpApplication;
    public QueueController(IQueueSharpApplication queueSharpApplication)
    {
        _queueSharpApplication = queueSharpApplication;
    }

    [HttpPost("{topic}")]
    public async Task<IActionResult> Enqueue([FromRoute] string topic, EnqueueDto request)
    {
        await _queueSharpApplication.Enqueue(topic, new QModel(request.Payload));
        return Accepted();
    }
    
    [HttpGet("{topic}")]
    public async Task<IActionResult> Dequeue([FromRoute] string topic)
    {
        var message = await _queueSharpApplication.Dequeue(topic);

        if (message.Data is not null)
        {
            return Ok(message.Data);
        }

        return NotFound();
    }
}

public class EnqueueDto
{
    public string Payload { get; set; }
}