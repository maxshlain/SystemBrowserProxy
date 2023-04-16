using System.Diagnostics;
using Serilog;

namespace SystemBrowserProxy.Core;

public interface IProcessStarter
{
    void StartProcess(string processName, List<string> arguments);
    void OpenConfig(string editor);
}

public class ProcessStarter : IProcessStarter
{
    public void StartProcess(string processName, List<string> arguments)
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

    public void OpenConfig(string editor)
    {
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        Process.Start(editor, new[] { configPath });
    }

    private static string ArgumentsToText(List<string> arguments)
    {
        return arguments.Any() ? string.Join(" ", arguments) : "empty";
    }
}
