using System;
using System.IO;
using FenixOS.System.FShell;
using FenixOS.System.modes.cli;
using FenixOS.System.utils;
using FenixOS.System.WindowSystem.widget;

namespace FenixOS.System.WindowSystem.apps;

public class TerminalWindow : AbstractWindow
{
    private TerminalWidget _terminalWidget;
    private CommandManager _commandManager;
    
    public override string getTitle()
    {
        return "Terminal";
    }
    
    
    public override void start()
    {
        _terminalWidget = new TerminalWidget(0, 0, Data.SCREEN_WIDTH, Data.SCREEN_HEIGHT - Data.TOOLPANEL_HEIGHT);
        _terminalWidget.onEntered = ExecuteCommand;
        content.Add(_terminalWidget);
        _commandManager = CLIMode.registerDefaultCommands();
        
        _terminalWidget.addLine($"Welcome to {Data.OS_FULL} GUI Terminal!");
    }
    
    private void ExecuteCommand(string commandLine)
    {
        var stringWriter = new StringWriter();
        var originalOut = Console.Out;
        Console.SetOut(stringWriter);
        
        try
        {
            string[] parts = commandLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string commandName = parts[0];
            string[] args = new string[parts.Length - 1];
            Array.Copy(parts, 1, args, 0, parts.Length - 1);
            
            if (commandName.ToLower() == "clear")
            {
                _terminalWidget.clear();
            }
            else
            {
                _commandManager.execute(commandName, args);
            }
        }
        catch (Exception ex)
        {
            stringWriter.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            Console.SetOut(originalOut);
            
            string output = stringWriter.ToString();
            if (!string.IsNullOrWhiteSpace(output))
            {
                _terminalWidget.addLine(output);
            }
        }
    }
    
    
}