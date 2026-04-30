using System.Collections.Generic;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;
using FenixOS.System.utils;

namespace FenixOS.System.WindowSystem;

public class ToolPanel
{
    public Vec2 position;
    public Vec2 size;

    public Bitmap logo;

    private bool isMenuopened = false;

    public ToolPanel(Vec2 pos, Vec2 size)
    {
        this.position = pos;
        this.size = size;

        logo = new Bitmap(ResourceManager.logoIcon);
    }

    public void draw(Canvas canvas, List<AbstractWindow> allWindows, int activeIdx)
    {
        canvas.DrawFilledRectangle(Data.ToolPanelBackgroundColor, position.x, position.y, size.x, size.y);
        canvas.DrawImageAlpha(logo, position.x + 5, position.y + 5);

        int btnWidth = 150;
        for (int i = 0; i < allWindows.Count; i++)
        {
            int bx = position.x + 80 + (i * (btnWidth + 10));
            int by = position.y + 10;
            
            Color btnColor = (i == activeIdx) ? Color.SteelBlue : Color.DarkGray;
        
            canvas.DrawFilledRectangle(btnColor, bx, by, btnWidth, size.y - 20);
            canvas.DrawString(allWindows[i].getTitle(), 
                Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.White, bx + 5, by + 15);
        }
    }

    public int checkClick(int mx, int my, int windowCount)
    {
        if (my < position.y) return -1;

        int btnWidth = 150;
        for (int i = 0; i < windowCount; i++)
        {
            int bx = position.x + 80 + (i * (btnWidth + 10));
            if (mx >= bx && mx <= bx + btnWidth) return i;
        }
        return -1;
    }
}