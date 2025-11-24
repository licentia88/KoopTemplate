// using KoopTemplate.Grpc.Services;
using KoopTemplate.Repository.Extensions;
 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Register Koop services
builder.Services.RegisterRepository();
builder.Services.AutoRegisterFromKoopTemplateRepository();

var app = builder.Build();

// Configure the HTTP request pipeline.
// app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();