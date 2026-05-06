using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.WindowSystem.apps;

public class TestAbstractApp : IApp
{
    public string Name => "TestAbstractApp";
    public AbstractWindow CreateInstance() => new TestAbstractWindow();
}