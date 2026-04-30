using System.Collections.Generic;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;
using FenixOS.System.EventSystem.events;
using FenixOS.System.modes.gui;
using FenixOS.System.utils;

namespace FenixOS.System.WindowSystem;

public class WindowManager
{
    private List<AbstractWindow> windows;
    private Canvas canvas;
    private DrawTool _cachedTool;
    private int activeIndex = 0;

    public void init(Canvas canvas)
    {
        this.canvas = canvas;
        windows = new List<AbstractWindow>();
        _cachedTool = new DrawTool(canvas, new Vec2(0, 0), 
            new Vec2(Data.SCREEN_WIDTH, Data.SCREEN_HEIGHT - Data.TOOLPANEL_HEIGHT));
    }
    
    public void addWindow(AbstractWindow w)
    {
        windows.Add(w);
        activeIndex = windows.Count - 1;
        w.start();
        GUIMode.redrawManager.requestFullRedraw();
    }

    public void startWindowManager()
    {
        foreach (AbstractWindow window in windows)
        {
            window.start();
        }
    }

    public void update()
    {
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].update(_cachedTool);
        }
    }
    
    
    public void setActive(int index)
    {
        if (index >= 0 && index < windows.Count)
        {
            var win = windows[index];
            windows.RemoveAt(index);
            windows.Add(win);
            activeIndex = windows.Count - 1;
            modes.gui.GUIMode.redrawManager.requestFullRedraw();
        }
    }

    public List<AbstractWindow> getWindows() => windows;
    public int getActiveIndex() => activeIndex;
}