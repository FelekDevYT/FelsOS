using System;
using System.Drawing;
using Cosmos.System.Graphics.Fonts;

namespace FenixOS.System.WindowSystem.widget;

public class ButtonWidget : Widget
{
    public string Text { get; set; }
    public Action OnClick { get; set; }

    public Color BackgroundColor { get; set; } = Color.DarkGray;
    public Color HoverColor { get; set; } = Color.Gray;
    public Color PressedColor { get; set; } = Color.SteelBlue;
    public Color TextColor { get; set; } = Color.White;

    private bool isPressed = false;
    private bool isHovered = false;
    
    public ButtonWidget(string text, int x, int y, int w, int h)
    {
        this.Text = text;
        position = new Vec2(x, y);
        size = new Vec2(w, h);
    }
    
    public override void draw(DrawTool tool)
    {
        if (!visible) return;
        getAbsoluteposition(out int ax, out int ay);

        Color currentColor = isPressed ? PressedColor : (isHovered ? HoverColor : BackgroundColor);
        tool.canvas.DrawFilledRectangle(currentColor, ax, ay, size.x, size.y);

        if (!string.IsNullOrEmpty(Text))
        {
            int textWidth = Text.Length * PCScreenFont.Default.Width;
            int textX = ax + (size.x - textWidth) / 2;
            int textY = ay + (size.y - PCScreenFont.Default.Height) / 2;
            tool.canvas.DrawString(Text, PCScreenFont.Default, TextColor, textX, textY);
        }
    }
    
    public override bool onMouseDown(int x, int y)
    {
        if (IsHit(x, y))
        {
            isPressed = true;
            modes.gui.GUIMode.redrawManager.requestFullRedraw();
            return true;
        }
        return false;
    }
    
    public override void onMouseUp(int x, int y)
    {
        if (isPressed)
        {
            isPressed = false;
            if (IsHit(x, y))
            {
                OnClick?.Invoke();
            }
            modes.gui.GUIMode.redrawManager.requestFullRedraw();
        }
    }
    
    public override void onMouseMove(int x, int y)
    {
        bool wasHovered = isHovered;
        isHovered = IsHit(x, y);

        if (wasHovered != isHovered)
        {
            modes.gui.GUIMode.redrawManager.requestFullRedraw();
        }
    }
}