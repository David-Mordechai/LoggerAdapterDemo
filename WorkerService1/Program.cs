using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using WorkerService1;
using WorkerService1.Logger;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton(typeof(ILoggerAdapter<>),typeof(LoggerAdapter<>));
builder.Services.AddHostedService<Worker>();

const string serviceName = "WorkerService1";
builder.Services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));
builder.Services.AddOpenTelemetry()
    
    .WithTracing(tracing => tracing
        // The rest of your setup code goes here
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddGrpcClientInstrumentation()
        
        // .AddHttpClientInstrumentation()
        .AddOtlpExporter())
    .WithMetrics(metrics => metrics
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
        // The rest of your setup code goes here
        .AddOtlpExporter());

builder.Logging.AddOpenTelemetry(logging => {
    // The rest of your setup code goes here
    logging.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName));
    logging.IncludeFormattedMessage = true;
    logging.AddOtlpExporter();
});

var host = builder.Build();
host.Run();