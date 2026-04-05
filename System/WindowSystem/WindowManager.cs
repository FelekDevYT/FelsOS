using System.Collections.Generic;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;
using FenixOS.System.EventSystem.events;

namespace FenixOS.System.WindowSystem;

public class WindowManager
{
    private List<AbstractWindow> windows;
    private Canvas canvas;

    public void init(Canvas canvas)
    {
        this.canvas = canvas;
        
        windows = new List<AbstractWindow>();
    }
    
    public void addWindow(AbstractWindow abstractWindow)
    {
        windows.Add(abstractWindow);
    }

    public void startWindowManager()
    {
        foreach (AbstractWindow window in windows)
        {
            window.start();
        }
    }

    public void updateAllWindows()
    {
        foreach (AbstractWindow window in windows)
        {
            canvas.DrawFilledRectangle(Color.DarkGray, window.getPosition().x, window.getPosition().y, window.getDefaultSize().x, window.getDefaultSize().y);
            
            canvas.DrawFilledRectangle(Color.DarkSlateGray, 
                window.getPosition().x, window.getPosition().y - 22,
                window.getDefaultSize().x, 22
            );
            canvas.DrawString(window.getTitle(), Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, 
                window.getPosition().x+2,  window.getPosition().y-20);
            
            canvas.DrawRectangle(Color.Black,  window.getPosition().x, window.getPosition().y - 22,
                window.getDefaultSize().x, window.getDefaultSize().y + 22);
            
            window.update(new DrawTool(canvas, window.getPosition(), window.getDefaultSize()));
        }

        foreach (AbstractWindow window in windows)
        {
            int mouseX = (int) MouseManager.X;
            int mouseY = (int) MouseManager.Y;
            if (mouseY >= window.getPosition().y - 22 &&
                mouseY <= window.getPosition().y &&
                mouseX >= window.getPosition().x &&
                mouseX <= window.getPosition().x + window.getDefaultSize().x)
            {
                Kernel.eventManager.callEvent(new MouseOnTitleEvent(new DrawTool(canvas, window.getPosition(), window.getDefaultSize())));

                if (MouseManager.MouseState == MouseState.Left)
                {
                    Kernel.eventManager.callEvent(new MouseClickedTitleEvent(new DrawTool(canvas,  window.getPosition(), window.getDefaultSize())));
                }
            }
        }
    }
}