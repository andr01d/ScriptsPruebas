#r "nuget: Serilog, 2.9.0"
#r "nuget: Serilog.Sinks.Console, 3.1.1"
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
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource<String>())
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message}{NewLine}"))
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource<Decimal>())
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {Message}{NewLine}"))
    .CreateLogger();

var myLog = Log.ForContext<String>();
myLog.Information("Hello, Serilog!");
var myLog2 = Log.ForContext<Decimal>();
myLog2.Warning("Goodbye, Serilog.");
