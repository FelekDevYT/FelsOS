using System.Drawing;
using Cosmos.System.Graphics.Fonts;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.WindowSystem.widget;

public class LabelWidget : Widget
{
    public string text { get; set; }
    public Color textColor { get; set; } = Color.White;
    public Color background { get; set; } = Color.Transparent;

    public LabelWidget(string text, int x, int y, int w, int h)
    {
        position = new Vec2(x, y);
        size = new Vec2(w, h);
        text = text;
    }

    public override void draw(DrawTool tool)
    {
        if (!visible || string.IsNullOrEmpty(text)) return;
        getAbsoluteposition(out int ax, out int ay);
        
        if (background != Color.Transparent)
            tool.canvas.DrawFilledRectangle(background, ax, ay, size.x, size.y);
            
        tool.canvas.DrawString(text, PCScreenFont.Default, textColor, ax + 4, ay + 4);
    }
}