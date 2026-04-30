using System.Drawing;
using FenixOS.System.WindowSystem.widget;

namespace FenixOS.System.WindowSystem;

public class WindowBuilder
{
    public static ContainerWidget addLabel(ContainerWidget c, string text, int x, int y, int w, int h,
        Color color)
    {
        c.Add(new LabelWidget(text, x, y, w, h) { textColor = color == default ? Color.White : color });
        c.visible = true;
        c.enabled = true;
        return c;
    }
}