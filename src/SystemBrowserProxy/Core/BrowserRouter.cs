using System.Diagnostics;
using Serilog;

namespace SystemBrowserProxy2.Core;

public interface IBrowserRouter
{
    string OpenBrowser(string[] commandLineArgs);
}

public class BrowserRouter : IBrowserRouter
{
    private readonly Dictionary<string, object> _rules;

    private const string _msedge = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
    private const string _chrome = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

    public BrowserRouter()
    {
        _rules = new Dictionary<string, object>
        {
            ["bing"] = new[] { _msedge, },
            ["google"] = new[] { _chrome },
            ["ynet"] = new[] { _chrome, "--incognito" }
        };
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

        if (TryOpenUrl(url))
        {
            return url;
        }

        StartProcess(_msedge, new List<string> { url });
        return url;
    }

    private bool TryOpenUrl(string url)
    {
        foreach (var rule in _rules)
        {
            if (url.Contains(rule.Key))
            {
                var details = rule.Value as string[]
                              ?? throw new Exception("rule.Value is not a string[]");

                string processName = details[0]
                                     ?? throw new Exception("details[0] is null");

                List<string> argumentsList = new List<string>();

                if (details.Length > 1)
                {
                    argumentsList.Add(details[1]);
                }

                argumentsList.Add(url);

                StartProcess(processName, argumentsList);
                return true;
            }
        }

        return false;
    }

    private void StartProcess(string processName, List<string> arguments)
    {
        try
        {
            Process.Start(processName, arguments);
        }
        catch (Exception e)
        {
            var argumentsText = "";
            argumentsText = arguments == null ? "null" : argumentsText;
            argumentsText = !arguments.Any() ? "empty" : argumentsText;

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
