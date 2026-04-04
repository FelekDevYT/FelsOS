using System;
using System.IO;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class MkFileCommand : ICommand
{
    public string getCommandName()
    {
        return "mkfile";
    }

    public string getDescription()
    {
        return "Creates a new empty file at the specified path";
    }

    public CommandReturn execute(Argument[] args)
    {
        if (args.Length == 0)
        {
            IO.Debug.error("Usage: mkfile <file>");
            return CommandReturn.error("Usage: mkfile <path>");
        }

        String path = args[0].argument.Trim();
        Kernel.FileSystem.createFile(path);
        IO.Debug.ok("File " + path + " created");
        
        return CommandReturn.success("ok");
    }
}