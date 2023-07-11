using System;
using System.IO;
using NLog;
using Serilog;
using Serilog.Events;

namespace Logger
{
    public class Class1
    {
        private static NLog.Logger nlogLogger;
        private static Serilog.ILogger serilogLogger;

        public Class1()
        {
            Save("Logs");
        }

        private static void Save(string logFolderPath)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string logDirectory = Path.Combine(desktopPath, logFolderPath);
            Directory.CreateDirectory(logDirectory);

            var nlogConfig = new NLog.Config.LoggingConfiguration();
            var nlogFileTarget = new NLog.Targets.FileTarget("nlogFileTarget")
            {
                FileName = Path.Combine(logDirectory, "SaveLogs.txt")
            };
            nlogConfig.AddRule(LogLevel.Info, LogLevel.Fatal, nlogFileTarget);
            NLog.LogManager.Configuration = nlogConfig;
            nlogLogger = NLog.LogManager.GetCurrentClassLogger();

            var serilogConfig = new LoggerConfiguration()
                .WriteTo.File(Path.Combine(logDirectory, "serilog-logfile.txt"), LogEventLevel.Information)
                .CreateLogger();
            serilogLogger = serilogConfig;

            try
            {

            }
            catch (Exception ex)
            {
                nlogLogger.Error(ex, "Error");
                serilogLogger.Error(ex, "Error");
            }
        }
    }
}
