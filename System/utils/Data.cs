using System;
using System.Drawing;

namespace FenixOS.System.utils;

public class Data
{
    public static String OS_NAME = "Fenix OS";
    public static String OS_VERSION = "Alpha 2.3";
    public static String OS_FULL = OS_NAME + " " + OS_VERSION;
    
    public static int SCREEN_WIDTH = 1920;
    public static int SCREEN_HEIGHT = 1080;
    
    //GUI
    public static int TOOLPANEL_HEIGHT = 70;
    public static int TOOLPANEL_WIDTH = 1920;

    public static int MENU_HEIGHT = 500;
    public static int MENU_WIDTH = 500;
    
    public static ConsoleColor defaultBackgroundColor = ConsoleColor.Black;
    public static ConsoleColor USER_COLOR = ConsoleColor.DarkGray;
    public static ConsoleColor DOG_COLOR = ConsoleColor.Gray;
    public static ConsoleColor PATH_COLOR = ConsoleColor.DarkGray;
    public static ConsoleColor WELCOME_COLOR = ConsoleColor.White;
    
    //GUI
    public static Color ToolPanelBackgroundColor = Color.LightSlateGray;
    public static Color MenuPanelBackgroundColor = Color.DimGray;
}