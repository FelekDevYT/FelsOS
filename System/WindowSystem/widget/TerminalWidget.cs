using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using FenixOS.System.modes.gui;

namespace FenixOS.System.WindowSystem.widget;

public class TerminalWidget : Widget
{
    public Action<String> onEntered;
    private List<String> _lines = new List<String>();
    private List<String> _history = new List<String>();
    private StringBuilder _input =  new StringBuilder();
    private int _historyIndex = -1;
    private int scrollOffset = 0;
    private PCScreenFont font = PCScreenFont.Default;
    private int charHeight;
    private int displayeableLines;
    private int blinkTimer = 0;
    private bool cursorVisible = true;

    public TerminalWidget(int x, int y, int w, int h)
    {
        position = new Vec2(x, y);
        size = new Vec2(w, h);
        charHeight = font.Height;
        displayeableLines = (size.y / charHeight) - 1;
    }
    
    public void addLine(string text)
    {
        var textLines = text.Replace("\r", "").TrimEnd('\n').Split('\n');
        foreach (var line in textLines)
        {
            _lines.Add(line);
        }
        
        if (_lines.Count > displayeableLines)
        {
            scrollOffset = _lines.Count - displayeableLines;
        }
        
        GUIMode.redrawManager.requestFullRedraw();
    }

    public override void update()
    {
        blinkTimer++;
        if (blinkTimer > 30)
        {
            blinkTimer = 0;
            cursorVisible = !cursorVisible;
            GUIMode.redrawManager.requestFullRedraw();
        }
    }

    public override void draw(DrawTool tool)
    {
        getAbsoluteposition(out int ax, out int ay);
        tool.canvas.DrawFilledRectangle(Color.Black, ax, ay, size.x, size.y);
        
        int linesToDraw = Math.Min(displayeableLines,  _lines.Count);
        for (int i = 0; i < linesToDraw; i++)
        {
            int lineIndex = scrollOffset + i;
            if (lineIndex < _lines.Count)
            {
                tool.canvas.DrawString(_lines[lineIndex], font, Color.White, ax + 4, ay + 4 + i * charHeight);
            }
        }
        
        int inputY = ay + 4 + displayeableLines * charHeight;
        string prompt = "> " + _input;
        tool.canvas.DrawString(prompt, font, Color.White, ax + 4, inputY);
        
        if (cursorVisible)
        {
            int cursorX = ax + 4 + prompt.Length * font.Width;
            tool.canvas.DrawFilledRectangle(Color.White, cursorX, inputY, font.Width, font.Height);
        }
    }

    public override void onMouseScroll(int deltaX, int deltaY) 
    {
        scrollOffset -= deltaY;
        
        int maxScroll = Math.Max(0, _lines.Count - displayeableLines);
        if (scrollOffset > maxScroll) scrollOffset = maxScroll;
        if (scrollOffset < 0) scrollOffset = 0;
        
        GUIMode.redrawManager.requestFullRedraw();
    }
    
    public override void onKeyPressed(KeyEvent key)
    {
        switch (key.Key)
        {
            case ConsoleKeyEx.Enter:
                string cmd = _input.ToString();
                addLine("> " + cmd);
                if (!string.IsNullOrWhiteSpace(cmd))
                {
                    _history.Add(cmd);
                    onEntered?.Invoke(cmd);
                }
                _input.Clear();
                _historyIndex = -1;
                break;
            
            case ConsoleKeyEx.Backspace:
                if (_input.Length > 0)
                {
                    _input.Remove(_input.Length - 1, 1);
                }
                break;
                
            case ConsoleKeyEx.UpArrow:
                if (_history.Count > 0)
                {
                    if (_historyIndex == -1) _historyIndex = _history.Count - 1;
                    else if (_historyIndex > 0) _historyIndex--;
                    
                    _input.Clear().Append(_history[_historyIndex]);
                }
                break;
            
            case ConsoleKeyEx.DownArrow:
                if (_historyIndex != -1)
                {
                    if (_historyIndex < _history.Count - 1)
                    {
                        _historyIndex++;
                        _input.Clear().Append(_history[_historyIndex]);
                    }
                    else
                    {
                        _historyIndex = -1;
                        _input.Clear();
                    }
                }
                break;
            
            default:
                if (key.KeyChar != '\0')
                {
                    _input.Append(key.KeyChar);
                }
                break;
        }
        
        blinkTimer = 0;
        cursorVisible = true;
        GUIMode.redrawManager.requestFullRedraw();
    }
    public void clear()
    {
        _lines.Clear();
        scrollOffset = 0;
        GUIMode.redrawManager.requestFullRedraw();
    }
}