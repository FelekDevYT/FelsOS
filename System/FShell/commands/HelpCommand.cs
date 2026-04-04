using System;
using System.Linq;
using FenixOS.System.modes.cli;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class HelpCommand : ICommand
{
    public string getCommandName()
    {
        return "help";
    }

    public string getDescription()
    {
        return "Displays a list of available commands or help for a specific command";
    }

    public CommandReturn execute(Argument[] args)
    {
        CLIMode mode = (CLIMode) Kernel.cli;//should be changed. This is not cool xd
        if (args.Length == 0)
        {
            IO.printlncol("Available commands:", ConsoleColor.Yellow);
            foreach (var cmd in mode.CommandManager.getRegisteredCommands().Values)
            {
                string commandName = cmd.getCommandName().PadRight(6);//i hate C# strings xd
                IO.println($"{commandName} - {cmd.getDescription()}");
            }
        
            return CommandReturn.success("ok");
        }
        
        String name = args[0].argument;//name - description
        if (!mode.CommandManager.getRegisteredCommands().ContainsKey(name))
        {
            IO.Debug.error("Couldn't find command: " + name);
        }
            
        IO.printcolor(name, ConsoleColor.Yellow);
        IO.println(" - " + mode.CommandManager.getRegisteredCommands()[name]);
        
        return CommandReturn.success("ok");
    }
}