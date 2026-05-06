using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using FenixOS.System.modes.cli;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class STECommand : ICommand
{
    private List<string> lines;
    private int x = 0;
    private int y = 0;
    private int scrollY = 0;
    private string currentFile = "";
    private bool isRunning = false;
    
    public string getCommandName() => "ste";

    public string getDescription() => "simple text editor for FenixOS";

    public CommandReturn execute(Argument[] args)
    {
        if (args.Length == 0)
        {
            return CommandReturn.error("Usage: nano <filename>");
        }
        
        currentFile = Kernel.FileSystem.getPath(args[0].argument);
        lines = new List<string>();
        
        if (File.Exists(currentFile))
        {
            string content = Kernel.FileSystem.readFile(currentFile);
            if (content != null)
            {
                var split = content.Replace("\r", "").Split('\n');
                lines.AddRange(split);
            }
        }
        
        if (lines.Count == 0)
        {
            lines.Add("");
        }
        
        x = 0;
        y = 0;
        scrollY = 0;
        isRunning = true;
        
        Console.Clear();
        
        while (isRunning)
        {
            //draw logic
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string header = $" Fenix Nano - File: {Path.GetFileName(currentFile)} ";
            Console.Write(header.PadRight(Console.WindowWidth));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            
            int displayLines = Console.WindowHeight - 2;
            for (int i = 0; i < displayLines; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                int lineIndex = scrollY + i;
                if (lineIndex < lines.Count)
                {
                    string line = lines[lineIndex];
                    if (line.Length > Console.WindowWidth)
                    {
                        line = line.Substring(0, Console.WindowWidth);
                    }
                    Console.Write(line.PadRight(Console.WindowWidth));
                }
                else
                {
                    Console.Write(new string(' ', Console.WindowWidth));
                }
            }
            
            Console.SetCursorPosition(0, Console.WindowHeight - 1);//1am time, i'am so xochy spat
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string footer = " ^S Save    ^X Exit ";
            Console.Write(footer.PadRight(Console.WindowWidth - 1));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            if (y < scrollY) scrollY = y;
            if (y >= scrollY + displayLines) scrollY = y - displayLines + 1;
            int screenX = x;
            if (screenX >= Console.WindowWidth) screenX = Console.WindowWidth - 1;
            Console.SetCursorPosition(screenX, y - scrollY + 1);
            Console.CursorVisible = true;
            
            //input thing
            inputThing();
        }
        
        Console.Clear();
        
        return CommandReturn.success("OK");
    }

    private void inputThing()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Modifiers == ConsoleModifiers.Control)
        {
            if (keyInfo.Key == ConsoleKey.S)
            {
                //let's save this
                string content = "";
                for (int i = 0; i < lines.Count; i++)
                {
                    content += lines[i];
                    if (i < lines.Count - 1)
                    {
                        content += "\n";
                    }
                }
                
                if (!File.Exists(currentFile))
                {
                    Kernel.FileSystem.createFile(currentFile);
                }
                
                FSCode code = Kernel.FileSystem.writeFile(currentFile, content);
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                
                if (code.code == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" File saved successfully! Press any key... ".PadRight(Console.WindowWidth - 1));
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" Error saving file (Code: {code.code}). Press any key... ".PadRight(Console.WindowWidth - 1));
                }

                Console.ReadKey(true);
            }
            else if (keyInfo.Key == ConsoleKey.X)
            {
                isRunning = false;
            }
            
            Console.ReadKey(true); 
            return;
        }
        
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (y > 0) y--;
                if (x > lines[y].Length) x = lines[y].Length;
                break;
            case ConsoleKey.DownArrow:
                if (y < lines.Count - 1) y++;
                if (x > lines[y].Length) x = lines[y].Length;
                break;
            case ConsoleKey.LeftArrow:
                if (x > 0)
                {
                    x--;
                }
                else if (y > 0)
                {
                    y--;
                    x = lines[y].Length;
                }
                break;
            case ConsoleKey.RightArrow:
                if (x < lines[y].Length)
                {
                    x++;
                }
                else if (y < lines.Count - 1)
                {
                    y++;
                    x = 0;
                }
                break;
            case ConsoleKey.Backspace:
                if (x > 0)
                {
                    lines[y] = lines[y].Remove(x - 1, 1);
                    x--;
                }
                else if (y > 0)
                {
                    string currentLine = lines[y];
                    lines.RemoveAt(y);
                    y--;
                    x = lines[y].Length;
                    lines[y] += currentLine;
                }
                break;
            case ConsoleKey.Enter:
                string leftPart = lines[y].Substring(0, x);
                string rightPart = lines[y].Substring(x);
                lines[y] = leftPart;
                lines.Insert(y + 1, rightPart);
                y++;
                x = 0;
                break;
            default:
                if (keyInfo.KeyChar != '\0' && !char.IsControl(keyInfo.KeyChar))
                {
                    lines[y] = lines[y].Insert(x, keyInfo.KeyChar.ToString());
                    x++;
                }
                break;
        }
    }
}