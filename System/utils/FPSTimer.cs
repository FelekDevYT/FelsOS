using System;

namespace FenixOS.System.utils;

public class FPSTimer
{
    private int frameCount = 0;
    private float currentFPS = 0;
    private float minFPS = float.MaxValue;
    private float maxFPS = 0;
        
    private DateTime lastTime;
    
    private float updateInterval = 0.5f;

    public FPSTimer()
    {
        lastTime = DateTime.UtcNow;
    }

    public void Update()
    {
        frameCount++;
        DateTime now = DateTime.UtcNow;
        double elapsed = (now - lastTime).TotalSeconds;

        if (elapsed >= updateInterval)
        {
            currentFPS = (float)(frameCount / elapsed);
            if (currentFPS < minFPS) minFPS = currentFPS;
            if (currentFPS > maxFPS) maxFPS = currentFPS;

            frameCount = 0;
            lastTime = DateTime.UtcNow;
        }
    }

    public float getFps()
    {
        return currentFPS;
    }

    public float getMaxFPS()
    {
        return maxFPS;
    }
    
    public float getMinFPS()
    {
        return minFPS;
    }
}