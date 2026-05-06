using System;
using System.Drawing;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.modes.panic;

public class PanicMode : IMode
{
    private Canvas canvas;
    private String message = "panic message";
    private int result = -1;
    private String source = "unknown";
    
    public PanicMode(String msg, int result)
    {
        this.message = msg;
        this.result = result;
    }
    
    public void start()
    {
        canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 320, ColorDepth.ColorDepth32));
    }

    public void update()
    {
        canvas.Clear(Color.DarkRed);
        
        canvas.DrawString("FENIX OS -> CRITICAL KERNEL PANIC!", PCScreenFont.Default, Color.White, 20, 20);
        canvas.DrawString("CRITICAL ERROR OCCURED WHILE OS PROCESS",  PCScreenFont.Default, Color.White, 20, 60);
        
        canvas.DrawString("ERROR RESULT CODE: " + result, PCScreenFont.Default, Color.White, 20, 120);
        canvas.DrawString("ERROR MESSAGE: " + message, PCScreenFont.Default, Color.White, 20, 140);
        canvas.DrawString("SOURCE FILE: " + source, PCScreenFont.Default, Color.White, 20, 160);
        
        canvas.DrawString("Due to critical error, OS FULLY HALTED!", PCScreenFont.Default, Color.White, 20, 240);
        canvas.Display();
        
        canvas.DrawString("Press any key to reboot your OS...", PCScreenFont.Default, Color.White, 20, 260);
        Console.ReadKey();
        Cosmos.System.Power.Shutdown();
    }

    public void stop()
    {
        Cosmos.System.Power.Shutdown();
    }
}