using GrpcSample_01.Services;
using System.Text.Json.Serialization;

//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateSlimBuilder(args);

//builder.Services.ConfigureHttpJsonOptions(options =>
//{
//    options.SerializerOptions.TypeInfoResolverChain.Insert();
//});

// Add services to the container.
builder.Services.AddGrpc()
    .AddJsonTranscoding();
builder.WebHost.UseKestrelHttpsConfiguration();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true});
app.MapGrpcService<GreeterService>().EnableGrpcWeb();
app.MapGrpcService<RequestTypeService>().EnableGrpcWeb();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
