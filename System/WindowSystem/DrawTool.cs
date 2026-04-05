using System.Drawing;
using Cosmos.System.Graphics;

namespace FenixOS.System.WindowSystem;

public class DrawTool
{
    private Canvas canvas;
    private Vec3 drawingSize;
    private Vec3 position;

    public DrawTool(Canvas canvas, Vec3 position, Vec3 drawingSize)
    {
        this.canvas = canvas;
        this.drawingSize = drawingSize;
        this.position = position;
    }

    public void updateCtx(Vec3 pos, Vec3 size)
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