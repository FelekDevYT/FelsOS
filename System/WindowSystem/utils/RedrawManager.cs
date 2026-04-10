namespace FenixOS.System.WindowSystem;

public class RedrawManager
{
    public bool needsFullRedraw = false;

    public void requestFullRedraw()
    {
        needsFullRedraw = true;
    }

    public void tick()
    {
        needsFullRedraw = false;
    }
}