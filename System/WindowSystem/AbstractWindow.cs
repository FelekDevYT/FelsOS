using System;
using FenixOS.System.EventSystem;

namespace FenixOS.System.WindowSystem;

public abstract class AbstractWindow
{
    private Vec3 windowPosition;
    private Vec3 windowSize;

    public AbstractWindow(Vec3 pos, Vec3 size)
    {
        this.windowPosition = pos;
        this.windowSize = size;
    }

    public Vec3 getPosition()
    {
        return windowPosition;
    }

    public void setPosition(int x, int y)
    {
        this.windowPosition.x = x;
        this.windowPosition.y = y;
    }

    public Vec3 getSize()
    {
        return windowSize;
    }

    public void setSize(Vec3 windowSize)
    {
        this.windowSize = windowSize;
    }

    public abstract String getTitle();
    public abstract Vec3 getDefaultSize();

    public abstract void start();
    public abstract void update(DrawTool tool);
}