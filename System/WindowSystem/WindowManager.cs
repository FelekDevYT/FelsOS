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

    public AbstractWindow takedWindow;
    
    private int dragOffsetX;
    private int dragOffsetY;
    private bool isDragging = false;
    private DrawTool _cachedTool;

    public void init(Canvas canvas)
    {
        this.canvas = canvas;
        
        windows = new List<AbstractWindow>();
        _cachedTool = new DrawTool(canvas, new Vec3(0, 0), new Vec3(0, 0));
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
        int mx = (int)MouseManager.X;
        int my = (int)MouseManager.Y;
        
        handleInput(mx, my);
        
        drawAll(mx, my);
    }

    private void drawAll(int mx, int my)
    {
        foreach (AbstractWindow window in windows)
        {
            Vec3 pos = window.getPosition();
            Vec3 size = window.getDefaultSize();
            
            drawWindowDecoration(window);
            _cachedTool.updateCtx(pos, size);
            window.update(_cachedTool);
        }
    }
    
    private void handleInput(int mx, int my)
    {
        if (MouseManager.MouseState == MouseState.Left)
        {
            if (takedWindow != null){
                int newX = mx - dragOffsetX;
                int newY = my - dragOffsetY;

                if (newY < 22) newY = 22;

                takedWindow.setPosition(new Vec3(newX, newY));
            }
            else
            {
                for (int i = windows.Count - 1; i >= 0; i--)
                {
                    if (isMouseOverTitle(windows[i], mx, my))
                    {
                        if (isMouseOverClose(windows[i], mx, my))
                        {
                            windows.RemoveAt(i);
                            return;
                        }
                        
                        takedWindow = windows[i];
                        dragOffsetX = mx - takedWindow.getPosition().x;
                        dragOffsetY = my - takedWindow.getPosition().y;
                        
                        var w = windows[i];
                        windows.RemoveAt(i);
                        windows.Add(w);
                        break;
                    }
                }
            }
        }
        else
        {
            takedWindow = null;
        }
    }

    private bool isMouseOverTitle(AbstractWindow window, int mouseX, int mouseY)
    {
        return mouseY >= window.getPosition().y - 22 &&
               mouseY <= window.getPosition().y &&
               mouseX >= window.getPosition().x &&
               mouseX <= window.getPosition().x + window.getDefaultSize().x;
    }

    private void drawWindowDecoration(AbstractWindow window)
    {
        var pos = window.getPosition();
        if (pos.x < 0) pos.x = 0;
        if (pos.y < 25) pos.y = 25;
        
        canvas.DrawFilledRectangle(Color.DarkGray, window.getPosition().x, window.getPosition().y, window.getDefaultSize().x, window.getDefaultSize().y);
            
        canvas.DrawFilledRectangle(Color.DarkSlateGray, 
            window.getPosition().x, window.getPosition().y - 22,
            window.getDefaultSize().x, 22
        );
        canvas.DrawString(window.getTitle(), Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, 
            window.getPosition().x+2,  window.getPosition().y-20);
        
        canvas.DrawFilledRectangle(Color.DarkRed, window.getPosition().x+window.getDefaultSize().x-22, window.getPosition().y - 22,
            22, 22);
        canvas.DrawLine(Color.WhiteSmoke, window.getPosition().x+window.getDefaultSize().x-20, window.getPosition().y - 20,
            window.getPosition().x+window.getDefaultSize().x-2, window.getPosition().y - 2);
        canvas.DrawLine(Color.WhiteSmoke, window.getPosition().x+window.getDefaultSize().x-2, window.getPosition().y - 20,
            window.getPosition().x+window.getDefaultSize().x-20, window.getPosition().y - 2);
            
        canvas.DrawRectangle(Color.Black,  window.getPosition().x, window.getPosition().y - 22,
            window.getDefaultSize().x, window.getDefaultSize().y + 22);
    }
    
    private bool isMouseOverClose(AbstractWindow window, int mx, int my)
    {
        return mx >= window.getPosition().x + window.getDefaultSize().x - 22 &&
               mx <= window.getPosition().x + window.getDefaultSize().x &&
               my >= window.getPosition().y - 22 &&
               my <= window.getPosition().y;
    }
}