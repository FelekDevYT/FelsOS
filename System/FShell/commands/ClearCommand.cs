using System;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class ClearCommand : ICommand
{
    public string getCommandName()
    {
        return "clear";
    }

    public string getDescription()
    {
        return "Clears the console screen";
    }

    public CommandReturn execute(Argument[] args)
    {
        Console.Clear();
        
        return CommandReturn.success("ok");
    }
}