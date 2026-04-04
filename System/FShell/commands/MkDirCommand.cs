using System;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class MkDirCommand : ICommand
{
    public string getCommandName()
    {
        return "mkdir";
    }

    public string getDescription()
    {
        return "Creates a new directory at the specified path";
    }

    public CommandReturn execute(Argument[] args)
    {
        if (args.Length == 0)
        {
            IO.Debug.error("Usage: mkdir <path>");
            return CommandReturn.error("Usage: mkdir <path>");
        }
        
        String path = args[0].argument.Trim();
        Kernel.FileSystem.createDirectory(path);
        IO.Debug.ok("Directory " + path + " created");
        
        return CommandReturn.success("ok");
    }
}