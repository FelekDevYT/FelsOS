using System;
using FenixOS.System.modes.cli;
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
        FSCode code = Kernel.FileSystem.remove(path, true);

        if (code.code != 0)
        {
            IO.Debug.error("Failed to remove file/directory at " + path + " with code " +  code.code);   
            return CommandReturn.error("Failed to remove file/directory at " + path);
        }
        
        IO.Debug.ok("File/Directory removed at: " + path);
        
        return CommandReturn.success("ok");
    }
}