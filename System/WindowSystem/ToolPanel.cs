using Cosmos.System;
using Cosmos.System.Graphics;
using FenixOS.System.utils;

namespace FenixOS.System.WindowSystem;

public class ToolPanel
{
    public Vec3 position;
    public Vec3 size;

    public Bitmap logo;

    private bool isMenuopened = false;

    public ToolPanel(Vec3 pos, Vec3 size)
    {
        this.position = pos;
        this.size = size;

        logo = new Bitmap(ResourceManager.logoIcon);
    }

    public void draw(Canvas canvas)
    {
        canvas.DrawFilledRectangle(Data.ToolPanelBackgroundColor, position.x, position.y, size.x, size.y);
        canvas.DrawImageAlpha(logo, position.x, position.y);

        if (isMenuopened)
        {
            canvas.DrawFilledRectangle(Data.MenuPanelBackgroundColor, 
                position.x, position.y - Data.MENU_WIDTH, Data.MENU_WIDTH, Data.MENU_HEIGHT);
        }
    }

    public void update(int mx, int my)
    {
        if (MouseManager.LastMouseState == MouseState.None && mx >= position.x && mx <= position.x + size.x && my >= position.y && my <= position.y + size.y && MouseManager.MouseState == MouseState.Left)
        {
            isMenuopened = !isMenuopened;
        }
    }
}