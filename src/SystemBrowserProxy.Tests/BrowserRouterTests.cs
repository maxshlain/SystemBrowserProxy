using FluentAssertions;
using NSubstitute;
using SystemBrowserProxy.Core;

namespace SystemBrowserProxy.Tests;

public class BrowserRouterTests
{
    [Fact]
    public void Test1()
    {
        var router = new BrowserRouter(
            new Routes(),
            Substitute.For<IProcessStarter>()
        );

        router.Should().NotBeNull();
    }
}