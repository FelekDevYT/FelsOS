using System.Drawing;
using FenixOS.System.EventSystem;
using FenixOS.System.EventSystem.events;
using FenixOS.System.WindowSystem.widget;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.WindowSystem.apps;

public class TestAbstractWindow : AbstractWindow
{
    public TestAbstractWindow()
    {
        content = new ContainerWidget();
    }

    public override string getTitle()
    {
        return "Cool App";
    }

    public override void start()
    {
        // Kernel.eventManager.registerListener(this);
        WindowBuilder.addLabel(content, "Welcome to test appplication", 0, 0, 100, 100, Color.Black);
    }

    public override void update(DrawTool tool)
    {
        tool.drawRectange(Color.OrangeRed, 0, 0, 100, 100);
    }
}