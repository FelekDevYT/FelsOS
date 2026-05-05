using Cosmos.System;
using Cosmos.System.Graphics;

namespace FenixOS.System.WindowSystem.widget;

public interface IWidget
{
    Vec2 position { get; set; }
    Vec2 size { get; set; }
    bool visible { get; set; }
    bool enabled { get; set; }
    IWidget Parent { get; set; }
    
    void draw(DrawTool canvas);
    void update();
    bool onMouseDown(int x, int y);
    void onMouseUp(int x, int y);
    void onMouseMove(int x, int y);
    void onKeyPressed(KeyEvent key);
    void onMouseScroll(int deltaX, int deltaY); 
}