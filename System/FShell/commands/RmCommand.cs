using System;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class RmCommand : ICommand
{
    public string getCommandName()
    {
        return "rm";
    }

    public string getDescription()
    {
        return "Deletes a file or directory from the system";
    }

    public CommandReturn execute(Argument[] args)
    {
        if (args.Length == 0)
        {
            IO.Debug.error("Usage: rm <file/directory>");
            return CommandReturn.error("Usage: rm <file/directory>");
        }

        String path = args[0].argument.Trim();
        Kernel.FileSystem.remove(path, true);
        
        return CommandReturn.success("ok");
    }
}