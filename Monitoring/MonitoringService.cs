using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Core;
using System.Diagnostics;
using System.Reflection;
namespace Monitoring
   
{
    public static  class MonitoringService
    {
        public static readonly string ServiceName = Assembly.GetCallingAssembly().GetName().Name ?? "Unknown";

        public static TracerProvider TracerProvider;

        public static ActivitySource ActivitySource = new ActivitySource(ServiceName);

        public static ILogger Log => Serilog.Log.Logger;

        static MonitoringService()
        {
            //openTelemetry stuff
            TracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetSampler(new AlwaysOnSampler())
                .AddConsoleExporter()
                .AddSource(ActivitySource.Name)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
                .AddZipkinExporter(config =>
                config.Endpoint = new Uri("http://zipkin:9411/api/v2/spans"))
                .Build();

            //Serilog stuff
            Serilog.Log.Logger = new LoggerConfiguration()

                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.Seq("http://seq")
                .CreateLogger();

            Log.Debug("Started Logger in MonitorService");
            //Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"Serilog: {msg}"));
        }
        


    }
}