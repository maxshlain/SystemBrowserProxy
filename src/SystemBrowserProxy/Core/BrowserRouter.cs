using System.Diagnostics;
using Serilog;

namespace SystemBrowserProxy2.Core;

public interface IBrowserRouter
{
    string OpenBrowser(string[] commandLineArgs);
}

public class BrowserRouter : IBrowserRouter
{
    private readonly Routes _routes;

    public BrowserRouter(Routes routes)
    {
        _routes = routes;
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

        TryOpenUrl(url);
        return url;
    }

    private void TryOpenUrl(string url)
    {
        foreach (var rule in _routes.Rules)
        {
            if (!url.Contains(rule.Key))
            {
                continue;
            }

            StartProcess(rule.Value.Path, new List<string> { rule.Value.Args, url });
            return;
        }

        StartProcess(_routes.Default.Path, new List<string> { _routes.Default.Args, url });
    }

    private void StartProcess(string processName, List<string> arguments)
    {
        try
        {
            arguments = arguments.Where(x => !string.IsNullOrEmpty(x)).ToList();
            Process.Start(processName, arguments);
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to start process {processName} with arguments {arguments}",
                processName, ArgumentsToText(arguments));
        }
    }

    private string ArgumentsToText(List<string> arguments)
    {
        if (arguments == null)
        {
            return "null";
        }

        if (!arguments.Any())
        {
            return "empty";
        }

        return string.Join(" ", arguments);
    }
}
