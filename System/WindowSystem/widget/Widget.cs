using Cosmos.System;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.WindowSystem.widget;

public abstract class Widget : IWidget
{
    public Vec2 position { get; set; }
    public Vec2 size { get; set; }
    public bool visible { get; set; } = true;
    public bool enabled { get; set; } = true;
    public IWidget Parent { get; set; }
    public virtual void onKeyPressed(KeyEvent key) { }
    public virtual void onMouseScroll(int deltaX, int deltaY) { }

    public void getAbsoluteposition(out int x, out int y)
    {
        x = position.x;
        y = position.y;
        IWidget current = Parent;
        while (current != null)
        {
            x += current.position.x;
            y += current.position.y;
            current = current.Parent;
        }
    }
    
    public abstract void draw(DrawTool tool);
    public virtual void update() { }
    public virtual bool onMouseDown(int x, int y) { return false; }
    public virtual void onMouseUp(int x, int y) { }
    public virtual void onMouseMove(int x, int y) { }

    protected bool IsHit(int mx, int my)
    {
        getAbsoluteposition(out int ax, out int ay);
        return mx >= ax && mx < ax + size.x && my >= ay && my < ay + size.y;
    }
}