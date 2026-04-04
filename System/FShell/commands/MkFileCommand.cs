using System;
using System.IO;
using FenixOS.System.modes.cli;
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
        FSCode code = Kernel.FileSystem.createFile(path);
        if (code.code != 0)
        {
            IO.Debug.error("Failed to create file at " + path + " with code " +  code.code);
            return CommandReturn.error("Failed to create file at " + path);
        }
        IO.Debug.ok("File " + path + " created");
        
        return CommandReturn.success("ok");
    }
}