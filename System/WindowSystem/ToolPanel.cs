using System.Collections.Generic;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;
using FenixOS.System.modes.gui;
using FenixOS.System.utils;

namespace FenixOS.System.WindowSystem;

public class ToolPanel
{
    public Vec2 position;
    public Vec2 size;

    public Bitmap logo;

    private bool isMenuOpened = false;
    private int clickCooldown = 0;
    
    private List<IApp> _availableApps = new List<IApp>();

    public void RegisterApp(IApp app) 
    {
        if (app != null)
        {
            _availableApps.Add(app);
        }
    }

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
        
        if (isMenuOpened)
        {
            int menuX = position.x;
            int menuWidth = 180;
            int itemH = 40;
            int menuHeight = (_availableApps.Count * itemH) + 100;
            int menuY = position.y - menuHeight;

            canvas.DrawFilledRectangle(Color.DimGray, menuX, menuY, menuWidth, menuHeight);
            
            for (int i = 0; i < _availableApps.Count; i++)
            {
                canvas.DrawString(_availableApps[i].Name, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.White, menuX + 10, menuY + 10 + (i * itemH));
            }
            
            canvas.DrawString("Reboot", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Yellow, menuX + 10, menuY + ( _availableApps.Count * itemH) + 30);
            canvas.DrawString("Shutdown", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Red, menuX + 10, menuY + ( _availableApps.Count * itemH) + 60);
        }
        
        int btnWidth = 160;
        for (int i = 0; i < allWindows.Count; i++)
        {
            int bx = position.x + 90 + (i * (btnWidth + 10));
            int by = position.y + 10;
            Color btnColor = (i == activeIdx) ? Color.SteelBlue : Color.DarkSlateGray;
            canvas.DrawFilledRectangle(btnColor, bx, by, btnWidth, size.y - 20);
            canvas.DrawString(allWindows[i].getTitle(), Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.White, bx + 10, by + 15);
        }
    }

    public object checkClick(int mx, int my, int windowCount, out bool appLaunched)
    {
        appLaunched = false;
        if (clickCooldown > 0)
        {
            clickCooldown--;
            return -1;
        }
        
        if (isMenuOpened)
        {
            int menuHeight = (_availableApps.Count * 40) + 100;
            int menuY = position.y - menuHeight;
            if (mx >= position.x && mx <= position.x + 180 && my >= menuY && my <= position.y)
            {
                for (int i = 0; i < _availableApps.Count; i++)
                {
                    int itemY = menuY + 10 + (i * 40);
                    if (my >= itemY && my <= itemY + 20)
                    {
                        isMenuOpened = false;
                        appLaunched = true;
                        return _availableApps[i];
                    }
                }

                int powerY = menuY + (_availableApps.Count * 40) + 30;
                if (my >= powerY && my <= powerY + 20) Cosmos.System.Power.Reboot();
                if (my >= powerY + 30 && my <= powerY + 50) Cosmos.System.Power.Shutdown();
                return -2;
            }
        }
        
        if (mx >= position.x && mx <= position.x + 80 && my >= position.y)
        {
            isMenuOpened = !isMenuOpened;
            clickCooldown = 15; 
            GUIMode.redrawManager.requestFullRedraw();
            return -2;
        }
        
        int btnWidth = 150;
        for (int i = 0; i < windowCount; i++)
        {
            int bx = position.x + 90 + (i * (btnWidth + 10));
            if (mx >= bx && mx <= bx + btnWidth) 
            {
                isMenuOpened = false;
                GUIMode.redrawManager.requestFullRedraw();
                return i;
            }
        }
        
        if (isMenuOpened) { isMenuOpened = false; GUIMode.redrawManager.requestFullRedraw(); }
        return -1;
    }
}