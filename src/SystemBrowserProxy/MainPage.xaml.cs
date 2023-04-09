using Serilog;
using System.Diagnostics;

namespace SystemBrowserProxy2;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        CentralBanner.Text = GetCentralBannerText(); 
    }

    private string GetCentralBannerText()
    {
        //Debugger.Launch();
        var args = Environment.GetCommandLineArgs();
        string secondOrNull = args?.ElementAtOrDefault(1);
        var path = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
        Process.Start(path, secondOrNull);
        string message = $"Starting msedge to ${secondOrNull}";
        //Environment.Exit(0);
        var bannerText = secondOrNull ?? "From Code Behind1";
        Serilog.Log.Information("{bannerText}", bannerText);
        return bannerText;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        Log.Logger.Information("counter: {counter}", count);
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

public class CommandLineArgumentsProvider
{
    public static string[] GetCommandLineArguments()
    {
        var args = Environment.GetCommandLineArgs();
        return args?.ToArray() ?? new string[0];
    }
}
