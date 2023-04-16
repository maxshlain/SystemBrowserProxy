using Microsoft.Toolkit.Uwp.Notifications;
using Serilog;
using SystemBrowserProxy.Core;

namespace SystemBrowserProxy2;

public partial class App : Application
{
    private readonly ICommandLineArgumentsProvider _provider;
    private readonly IBrowserRouter _router;

    public App(ICommandLineArgumentsProvider provider, IBrowserRouter router)
    {
        InitializeComponent();

        _provider = provider;
        _router = router;


        OpenBrowserAndShowBanner();
    }

    private async void OpenBrowserAndShowBanner()
    {
        // Debugger.Launch();
        string bannerText = "";

        try
        {
            var args = _provider.GetCommandLineArguments();
            var routeConfig = _router.OpenBrowser(args);
            bannerText = $"Starting {routeConfig.Name}";
            Log.Information("{bannerText}", bannerText);
        }
        catch (Exception e)
        {
            bannerText = e.Message;
        }

        new ToastContentBuilder()
            .AddText("Route completed")
            .AddText(bannerText)
            .Show();

        await Task.Delay(3000);

        Application.Current.Quit();
    }
}
