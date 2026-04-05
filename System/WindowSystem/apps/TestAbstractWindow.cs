using System.Drawing;
using FenixOS.System.EventSystem;
using FenixOS.System.EventSystem.events;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.WindowSystem.apps;

public class TestAbstractWindow : AbstractWindow, EventListener<MouseOnTitleEvent>
{
    public TestAbstractWindow(Vec3 pos, Vec3 size) : base(pos, size)
    {}

    public override string getTitle()
    {
        return "Cool App";
    }

    public override Vec3 getDefaultSize()
    {
        return new Vec3(800, 600);
    }

    public override void start()
    {
        Kernel.eventManager.registerListener(this);
    }

    public override void update(DrawTool tool)
    {
        tool.drawRectange(Color.Black, 0, 0, 200, 200);
    }

    public void onEvent(MouseOnTitleEvent e)
    {
        e.tool.drawRectange(Color.Tomato, 0, 0, 200, 200);
    }
}