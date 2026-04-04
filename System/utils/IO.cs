using System;

namespace FenixOS.System.utils;

public class IO
{
    public static class Debug
    {
        public static void error(String s)
        {
            print("[  ");
            printcolor("ERROR", ConsoleColor.DarkRed);
            print("  ] ");
            println(s);
        }
        
        public static void info(String s)
        {
            print("[  ");
            printcolor("INFO", ConsoleColor.Blue);
            print("  ] ");
            println(s);
        }
        public static void ok(String s)
        {
            print("[  ");
            printcolor("OK", ConsoleColor.DarkGreen);
            print("  ] ");
            println(s);
        }
        
    }
    
    public static String readln()
    {
        return Console.ReadLine();
    }

    public static void print(String s)
    {
        Console.Write(s);
    }

    public static void printfcolor(String s, ConsoleColor foreground, ConsoleColor background)
    {
        ConsoleColor foregroundColor = Console.ForegroundColor;
        ConsoleColor backgroundColor = Console.BackgroundColor;
        
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
        
        Console.Write(s);
        
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
    }

    public static void printcolor(String s, ConsoleColor foreground)
    {
        ConsoleColor foregroundColor = Console.ForegroundColor;
        ConsoleColor backgroundColor = Console.BackgroundColor;
        
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = Data.defaultBackgroundColor;
        
        Console.Write(s);
        
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
    }
    
    
    public static void println(string s)
    {
        Console.WriteLine(s);
    }

    public static void printlncol(String s, ConsoleColor foreground)
    {
        ConsoleColor foregroundColor = Console.ForegroundColor;
        ConsoleColor backgroundColor = Console.BackgroundColor;
        
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = Data.defaultBackgroundColor;
        
        Console.WriteLine(s);
        
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
    }
    
    public static void printlnfcolor(string s, ConsoleColor foreground, ConsoleColor background)
    {
        ConsoleColor foregroundColor = Console.ForegroundColor;
        ConsoleColor backgroundColor = Console.BackgroundColor;
        
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
        
        Console.WriteLine(s);
        
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
    }
}