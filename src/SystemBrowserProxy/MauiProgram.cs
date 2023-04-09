﻿
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace SystemBrowserProxy2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        SetupSerilog();

        try
        {
            return CreateMauiAppImpl();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while creating Maui App");
            throw;
        }
        finally
        {
            //Log.CloseAndFlush();
        }
    }

    private static MauiApp CreateMauiAppImpl()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
    
        builder.Logging.AddSerilog(dispose: true);
        return builder.Build();
    }
    
    private static void SetupSerilog()
    {
        //var flushInterval = new TimeSpan(0, 0, 1);
        var file = Path.Combine(FileSystem.AppDataDirectory, "MyApp.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(file, encoding: System.Text.Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 99)
            .CreateLogger();
    }
}
