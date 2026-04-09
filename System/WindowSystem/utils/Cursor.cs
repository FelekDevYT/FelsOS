using System.Drawing;
using Cosmos.System.Graphics;
using FenixOS.System.utils;

namespace FenixOS.System.WindowSystem;

public class Cursor
{
    private Bitmap cursorImage;
    private Bitmap backgroundCache;
    private int lastX = -1;
    private int lastY = -1;
    private int width;
    private int height;
    
    public Cursor(Bitmap cursorBmp)
    {
        this.cursorImage = cursorBmp;
        this.width = (int)cursorBmp.Width;
        this.height = (int)cursorBmp.Height;
        
        byte[] emptyData = new byte[width * height * 4]; 
        this.backgroundCache = new Bitmap((uint)width, (uint)height, emptyData, ColorDepth.ColorDepth32);
    }

    public void genBackground(Canvas canvas)
    {
        if (lastX == -1 || lastY == -1) return;
        
        canvas.DrawImage(backgroundCache, lastX, lastY);
    }

    public void draw(Canvas canvas, int x, int y)
    {
        if (x + width > Data.SCREEN_WIDTH) x = Data.SCREEN_WIDTH - width;
        if (y + height > Data.SCREEN_HEIGHT) y = Data.SCREEN_HEIGHT - height;
        
        if (x < 0) x = 0;
        if (y < 0) y = 0;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color c = canvas.GetPointColor(x + i, y + j);
                backgroundCache.RawData[i + j * width] = c.ToArgb();//saving pixel :)
            }
        }
        
        lastX = x;
        lastY = y;
        
        canvas.DrawImageAlpha(cursorImage, x, y);
    }
}