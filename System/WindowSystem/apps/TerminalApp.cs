using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.WindowSystem.apps;

public class TerminalApp : IApp
{
    public string Name =>  "Terminal";
    public AbstractWindow CreateInstance() => new TerminalWindow();
}