using Serilog;

namespace SystemBrowserProxy.Core;

public interface IBrowserRouter
{
    string OpenBrowser(string[] commandLineArgs);
}

public class BrowserRouter : IBrowserRouter
{
    private readonly Routes _routes;
    private readonly IProcessStarter _processStarter;

    public BrowserRouter(Routes routes, IProcessStarter processStarter)
    {
        _routes = routes;
        _processStarter = processStarter;
    }

    public string OpenBrowser(string[] commandLineArgs)
    {
        if (!commandLineArgs.Any())
        {
            Log.Warning("Command line arguments are empty");
            return "";
        }

        var processName = commandLineArgs[0];
        Log.Information("Process name: {processName}", processName);

        if (commandLineArgs.Length < 2)
        {
            Log.Warning("no second argument");
            return "";
        }

        var url = commandLineArgs[1];
        Log.Information("url: {url}", url);

        var route = TryOpenUrl(url);
        return "route";
    }

    private RouteConfig TryOpenUrl(string url)
    {
        foreach (var rule in _routes.Rules)
        {
            if (!url.Contains(rule.Key))
            {
                continue;
            }

            _processStarter.StartProcess(rule.Value.Path, new List<string> { rule.Value.Args, url });
            return rule.Value;
        }

        _processStarter.StartProcess(_routes.Default.Path, new List<string> { _routes.Default.Args, url });
        return _routes.Default;
    }
}
