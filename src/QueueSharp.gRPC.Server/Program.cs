using QueueSharp.Application;
using QueueSharp.gRPC.Server.Services;
using QueueSharp.SqlServerAdapter.Adapter;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddQueueSharpApplication()
    .AddSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
    .AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<QueueService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();