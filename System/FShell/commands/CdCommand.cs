using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class CdCommand : ICommand
{
    public string getCommandName()
    {
        return "cd";
    }

    public string getDescription()
    {
        return "Changes the current working directory";
    }

    public CommandReturn execute(Argument[] args)
    {
        Kernel.FileSystem.changeDirectory(args[0].argument);

        return CommandReturn.success(args[0].argument);
    }
}