#r "nuget: Serilog, 2.9.0"
#r "nuget: Serilog.Sinks.Console, 3.1.1"
#r "nuget: Serilog.Sinks.File, 4.1.0"
#r "nuget: Serilog.Settings.AppSettings, 2.2.2"

using Serilog;
using Serilog.Filters;

// using (var log = new LoggerConfiguration()
//     .WriteTo.Console()
//     .CreateLogger())
// {
//     log.Information("Hello, Serilog!");
//     log.Warning("Goodbye, Serilog.");
// }

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource<String>())
        .WriteTo.File("log01_.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {UserId} - {Message}{NewLine}"))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource<Decimal>())
        .WriteTo.File("log02_.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Event} - {Message}{NewLine}"))
    .CreateLogger();

using (Serilog.Context.LogContext.PushProperty("UserId", "123"))
using (Serilog.Context.LogContext.PushProperty("Event", "Test Event"))
{
    var myLog = Log.ForContext<String>();
    myLog.Information("Hello, Serilog!");
    var myLog2 = Log.ForContext<Decimal>();
    myLog2.Warning("Goodbye, Serilog.");
}

