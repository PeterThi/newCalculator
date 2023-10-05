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

        public static ILogger Log => Serilog.Log.Logger;


        public static readonly string ServiceName = Assembly.GetCallingAssembly().GetName().Name ?? "Unknown";

        public static TracerProvider TracerProvider;

        public static ActivitySource ActivitySource = new ActivitySource(ServiceName);


        static MonitoringService()
        {
            //openTelemetry stuff
            TracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddConsoleExporter()
                .AddZipkinExporter()
                .AddSource(ActivitySource.Name)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
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