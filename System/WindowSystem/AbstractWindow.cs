using System;
using Cosmos.System;
using FenixOS.System.EventSystem;
using FenixOS.System.WindowSystem.widget;

namespace FenixOS.System.WindowSystem;

public abstract class AbstractWindow
{
    public abstract String getTitle();
    protected ContainerWidget content { get; set; } = new ContainerWidget();
    public abstract void start();
    public virtual void update(DrawTool tool)
    {
        content.draw(tool);
    }
    
    public bool HandleMouse(int mx, int my, MouseState state)
    {
        if (state == MouseState.Left) return content.onMouseDown(mx, my);
        if (state == MouseState.None) content.onMouseUp(mx, my);
        content.onMouseMove(mx, my);
        return false;
    }
}