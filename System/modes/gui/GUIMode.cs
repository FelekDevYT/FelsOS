using System.Drawing;
using Cosmos.Core.Memory;
using Cosmos.System;
using Cosmos.System.Graphics;
using FenixOS.System.utils;
using FenixOS.System.WindowSystem;
using FenixOS.System.WindowSystem.apps;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.modes.gui;

public class GUIMode : IMode
{
    private Canvas canvas;
    private WindowManager windowManager;
    private FPSTimer timer;
    private ToolPanel panel;
    private Cursor cursor;
    
    public static RedrawManager redrawManager;
    private int ticks;
    
    public void start()
    {
        canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode((uint) Data.SCREEN_WIDTH, (uint) Data.SCREEN_HEIGHT, ColorDepth.ColorDepth32));
        MouseManager.ScreenWidth = (uint) Data.SCREEN_WIDTH;
        MouseManager.ScreenHeight = (uint) Data.SCREEN_HEIGHT;
        MouseManager.MouseSensitivity = 1.2f;

        Bitmap c = new Bitmap(ResourceManager.cursorIcon);
        cursor = new Cursor(c);
        
        windowManager = new WindowManager();
        windowManager.init(canvas);

        panel = new ToolPanel(new Vec2(0, Data.SCREEN_HEIGHT - Data.TOOLPANEL_HEIGHT), new Vec2(Data.TOOLPANEL_WIDTH, Data.TOOLPANEL_HEIGHT));
        
        windowManager.addWindow(new TestAbstractWindow());
        windowManager.addWindow(new TerminalWindow());
        
        panel.RegisterApp(new TerminalApp());
        panel.RegisterApp(new TestAbstractApp());
        
        timer = new FPSTimer();

        redrawManager = new RedrawManager();
        redrawManager.requestFullRedraw();
    }

    public void update()
    {
        timer.Update();
        cursor.genBackground(canvas);
        
        var activeWin = windowManager.getWindows()[windowManager.getActiveIndex()];

        if (MouseManager.MouseState == MouseState.Left && MouseManager.LastMouseState != MouseState.Left)
        {
            int mx = (int)MouseManager.X;
            int my = (int)MouseManager.Y;
            
            bool appLaunched = false;
            object result = panel.checkClick(mx, my, windowManager.getWindows().Count, out appLaunched);
            
            if (appLaunched && result is IApp app)
            {
                windowManager.addWindow(app.CreateInstance());
            }
            else if (result is int panelIdx && panelIdx >= 0)
            {
                windowManager.setActive(panelIdx);
                activeWin = windowManager.getWindows()[windowManager.getActiveIndex()];
            }
            else
            {
                activeWin.HandleMouse(mx, my, MouseState.Left);
            }
        }
        else if (MouseManager.MouseState == MouseState.Left)
        {
            activeWin.HandleMouse((int)MouseManager.X, (int)MouseManager.Y, MouseState.Left);
        }
        else
        {
            activeWin.HandleMouse((int)MouseManager.X, (int)MouseManager.Y, MouseState.None);
        }

        if (MouseManager.ScrollDelta != 0)
        {
            activeWin.handleMouseScroll(0, MouseManager.ScrollDelta);
        }
        if (KeyboardManager.TryReadKey(out var key))
        {
            activeWin.handleKeyboard(key);
        }
        
        activeWin.tick();

        bool needsRender = redrawManager.needsFullRedraw;
        redrawManager.tick();

        if (needsRender)
        {
            canvas.DrawFilledRectangle(Color.Blue, 0, 0, Data.SCREEN_WIDTH, Data.SCREEN_HEIGHT - Data.TOOLPANEL_HEIGHT);
            windowManager.update();
            panel.draw(canvas, windowManager.getWindows(), windowManager.getActiveIndex());
        }

        cursor.draw(canvas, (int)MouseManager.X, (int)MouseManager.Y);
        canvas.Display();
        
        if (++ticks >= 60)
        {
            ticks = 0;
            Cosmos.Core.Memory.Heap.Collect();
        }
    }

    public void stop()
    {
        canvas.Clear(Color.Black);
        canvas.Display();
        canvas.Disable();
    }
}