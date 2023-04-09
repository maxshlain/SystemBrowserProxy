using System.Diagnostics;
using System.Threading;

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
		Debugger.Launch();
        var args = Environment.GetCommandLineArgs();
        string secondOrNull = args?.ElementAtOrDefault(1);
		var path = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
		Process.Start(path, secondOrNull);
        string message = $"Starting msedge to ${secondOrNull}";
        //Environment.Exit(0);
        return secondOrNull ?? "From Code Behind1";
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

