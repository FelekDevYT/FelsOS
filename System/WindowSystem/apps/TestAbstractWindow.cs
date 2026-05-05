using System.Drawing;
using FenixOS.System.EventSystem;
using FenixOS.System.EventSystem.events;
using FenixOS.System.modes.gui;
using FenixOS.System.WindowSystem.widget;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.WindowSystem.apps;

public class TestAbstractWindow : AbstractWindow
{
    private int clickCount = 0;
    
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
        WindowBuilder.addLabel(content, "Welcome to test appplication", 0, 0, 100, 100, Color.Black);
        WindowBuilder.addButton(content, "Click Me!", 10, 40, 120, 30, () =>
        {
            clickCount++;
            WindowBuilder.addLabel(content, "Button clicked: " + clickCount, 500, 500 + (clickCount * 20), 400, 20, Color.Black);
            GUIMode.redrawManager.requestFullRedraw();
        });
    }
}