using SystemBrowserProxy.Core;

namespace SystemBrowserProxy.Tests;

public static class RouterTestFixtures
{
    public static readonly Routes Routes = new()
    {
        Default = new RouteConfig
        {
            Name = "Edge (Default)",
            Path = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe",
            Args = ""
        },
        Rules = new Dictionary<string, RouteConfig>
        {
            {
                "bing", new RouteConfig
                {
                    Name = "Edge for bing",
                    Path = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe",
                    Args = ""
                }
            },
            {
                "google", new RouteConfig
                {
                    Name = "Chrome for google",
                    Path = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
                    Args = ""
                }
            },
            {
                "ynet", new RouteConfig
                {
                    Name = "Chrome Incognito for ynet",
                    Path = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
                    Args = "--incognito"
                }
            }
        }
    };
}
