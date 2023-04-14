using NSubstitute;
using SystemBrowserProxy.Core;

namespace SystemBrowserProxy.Tests;

public class BrowserRouterTests
{
    [Fact]
    public void WillOpenBingExecutableForBingUrl()
    {
        var starter = Substitute.For<IProcessStarter>();
        var router = new BrowserRouter(
            RouterTestFixtures.Routes,
            starter
        );

        router.OpenBrowser(new[] { "test.exe", "http://www.bing.com" });

        starter.Received(1).StartProcess(
            RouterTestFixtures.Routes.Rules["bing"].Path,
            Arg.Is<List<string>>(x => x.Any(y => y == "http://www.bing.com"))
        );
    }

    [Fact]
    public void WillOpenChromeExecutableForGoogleUrl()
    {
        var starter = Substitute.For<IProcessStarter>();
        var router = new BrowserRouter(
            RouterTestFixtures.Routes,
            starter
        );

        router.OpenBrowser(new[] { "test.exe", "http://www.google.com" });

        starter.Received(1).StartProcess(
            RouterTestFixtures.Routes.Rules["google"].Path,
            Arg.Is<List<string>>(x => x.Any(y => y == "http://www.google.com"))
        );
    }
}
