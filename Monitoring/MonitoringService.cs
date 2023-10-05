using Serilog;
using Serilog.Core;
namespace Monitoring
   
{
    public static  class MonitoringService
    {
        public static ILogger Log => Serilog.Log.Logger;

        static MonitoringService()
        {
            Serilog.Log.Logger = new LoggerConfiguration()

                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.Seq("http://seq")
                .CreateLogger();

            Log.Debug("Started Logger in MonitorService");
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"Serilog: {msg}"));
        }
        


    }
}