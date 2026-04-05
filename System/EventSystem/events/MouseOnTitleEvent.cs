using FenixOS.System.WindowSystem;

namespace FenixOS.System.EventSystem.events;

public class MouseOnTitleEvent : Event
{
    public DrawTool tool;

    public MouseOnTitleEvent(DrawTool tool)
    {
        this.tool = tool;
    }
}