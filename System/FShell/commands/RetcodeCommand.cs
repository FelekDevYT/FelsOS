using System;
using FenixOS.System.modes.cli;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class RetcodeCommand : ICommand
{
    public string getCommandName()
    {
        return "retcode";
    }

    public string getDescription()
    {
        return "Shows the status and return code of the last executed command";
    }

    public CommandReturn execute(Argument[] args)
    {
        CommandReturn last = ((CLIMode) Kernel.cli).CommandManager.getLastReturn();//so cool thing from Java xd
        
        IO.print("Last command status: ");
        switch (last.type)
        {
            case CommandReturnType.FAILURE://bro fix this later xd
                IO.printcolor("[FAILURE]", ConsoleColor.DarkRed);
                break;
            case CommandReturnType.SUCCESS:
                IO.printcolor("[OK]", ConsoleColor.DarkGreen);
                break;
            case CommandReturnType.WARNING:
                IO.printcolor("[WARNING]", ConsoleColor.Yellow);
                break;
        }
        IO.println("");
        IO.print("Details: ");
        IO.printlncol(last.message, ConsoleColor.Yellow);
        
        return CommandReturn.success("ok");
    }
}