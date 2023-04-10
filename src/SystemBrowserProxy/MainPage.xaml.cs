using Serilog;
using System.Diagnostics;
using SystemBrowserProxy2.Core;

namespace SystemBrowserProxy2;

public partial class MainPage : ContentPage
{
    private readonly ICommandLineArgumentsProvider _commandLineArgumentsProvider;
    private readonly IBrowserRouter _browserRouter;
    
    private int count = 0;

    public MainPage()
    {
        _commandLineArgumentsProvider = new CommandLineArgumentsProvider();
        _browserRouter = new BrowserRouter();
        
        InitializeComponent();
        CentralBanner.Text = GetCentralBannerText(); 
    }

    private string GetCentralBannerText()
    {
        Debugger.Launch();
        var args = _commandLineArgumentsProvider.GetCommandLineArguments();
        var url = _browserRouter.OpenBrowser(args);
        url = string.IsNullOrEmpty(url) ? "null" : url;
        string bannerText = $"Starting msedge to {url}";
        Log.Information("{bannerText}", bannerText);
        return bannerText;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        Log.Information("counter: {counter}", count);
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
