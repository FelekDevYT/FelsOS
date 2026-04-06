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
    private Bitmap cursor;
    private ToolPanel panel;
    
    public void start()
    {
        canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode((uint) Data.SCREEN_WIDTH, (uint) Data.SCREEN_HEIGHT, ColorDepth.ColorDepth32));
        MouseManager.ScreenWidth = (uint) Data.SCREEN_WIDTH;
        MouseManager.ScreenHeight = (uint) Data.SCREEN_HEIGHT;
        MouseManager.MouseSensitivity = 1.2f;

        cursor = new Bitmap(ResourceManager.cursorIcon);
        
        windowManager = new WindowManager();
        windowManager.init(canvas);

        panel = new ToolPanel(new Vec3(0, Data.SCREEN_HEIGHT - Data.TOOLPANEL_HEIGHT), new Vec3(Data.TOOLPANEL_WIDTH, Data.TOOLPANEL_HEIGHT));
        
        windowManager.addWindow(new TestAbstractWindow(new Vec3(100, 100), new Vec3(800, 500)));
        timer = new FPSTimer();
        
        windowManager.startWindowManager();
    }

    public void update()
    {
        timer.Update();
        
        canvas.DrawFilledRectangle(Color.Blue, 0, 0, Data.SCREEN_WIDTH, Data.SCREEN_HEIGHT);//background
        
        windowManager.updateAllWindows();
        panel.update((int) MouseManager.X,(int) MouseManager.Y);
        panel.draw(canvas);
        
        canvas.DrawImageAlpha(cursor, (int) MouseManager.X, (int) MouseManager.Y);
        
        canvas.DrawString(timer.getFps().ToString(),Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, 10, 10);
        
        canvas.Display();
    }

    public void stop()
    {
        
    }
}