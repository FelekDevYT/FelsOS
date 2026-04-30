using System.Drawing;
using Cosmos.System.Graphics;

namespace FenixOS.System.WindowSystem;

public class DrawTool
{
    public Canvas canvas;
    private Vec2 drawingSize;
    private Vec2 position;

    public DrawTool(Canvas canvas, Vec2 position, Vec2 drawingSize)
    {
        this.canvas = canvas;
        this.drawingSize = drawingSize;
        this.position = position;
    }

    public void updateCtx(Vec2 pos, Vec2 size)
    {
        this.position = pos;
        this.drawingSize = size;
    }

    public void drawRectange(Color color, int x, int y, int width, int height)
    {
        int relativeX = position.x + x;
        int relativeY = position.y + y;

        if (width > drawingSize.x || height > drawingSize.y)
        {
            return;
        }
        
        canvas.DrawRectangle(color, relativeX, relativeY, width, height);
    }
}