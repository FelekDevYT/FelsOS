using FenixOS.System.WindowSystem;

namespace FenixOS.System.EventSystem.events;

public class MouseClickedTitleEvent : Event
{
    public DrawTool canvas;

    public MouseClickedTitleEvent(DrawTool canvas)
    {
        this.canvas = canvas;
    }

    public DrawTool getCanvas()
    {
        return canvas;
    }
}