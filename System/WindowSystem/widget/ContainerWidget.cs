using System.Collections.Generic;
using Cosmos.System;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.WindowSystem.widget;

public class ContainerWidget : Widget
{
    private readonly List<IWidget> _children = new List<IWidget>();
    public IReadOnlyList<IWidget> Children => _children;
    
    public void Add(IWidget child)
    {
        child.Parent = this;
        _children.Add(child);
    }
    
    public void Clear() => _children.Clear();

    public override void draw(DrawTool tool)
    {
        if (!visible) return;
        for (int i = 0; i < _children.Count; i++)
        {
            var child = _children[i];
            if (child.visible) child.draw(tool);
        }
    }
    
    public override bool onMouseDown(int x, int y)
    {
        for (int i = _children.Count - 1; i >= 0; i--)
        {
            var child = _children[i];
            if (child.enabled && child.visible && child.onMouseDown(x, y)) return true;
        }
        return IsHit(x, y);
    }
    
    public override void onMouseUp(int x, int y)
    {
        for (int i = _children.Count - 1; i >= 0; i--)
        {
            var child = _children[i];
            if (child.enabled && child.visible) child.onMouseUp(x, y);
        }
    }
    
    public override void onMouseMove(int x, int y)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            var child = _children[i];
            if (child.enabled && child.visible) child.onMouseMove(x, y);
        }
    }
    
    public override void onKeyPressed(KeyEvent key)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            var child = _children[i];
            if (child.enabled && child.visible) child.onKeyPressed(key);
        }
    }
    
    public override void onMouseScroll(int deltaX, int deltaY)
    {
        for (int i = 0; i < _children.Count; i++)
        {
            var child = _children[i];
            if (child.enabled && child.visible) child.onMouseScroll(deltaX, deltaY);
        }
    }
}