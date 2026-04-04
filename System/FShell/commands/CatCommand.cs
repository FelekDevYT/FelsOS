using System;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class CatCommand : ICommand
{
    public string getCommandName()
    {
        return "cat";
    }

    public string getDescription()
    {
        return "Reads and displays the content of a text file";
    }

    public CommandReturn execute(Argument[] args)
    {
        if (args.Length == 0)
        {
            IO.Debug.error("Usage: cat <file>");
        }

        String path = args[0].argument;
        IO.printlncol("Content of " + path + " :", ConsoleColor.DarkYellow);
        String content = Kernel.FileSystem.readFile(path);
        if (content == null)
        {
            IO.Debug.error("Failed to read from file: " + path);
            return CommandReturn.error("Failed to read from file: " + path);
        }
        
        IO.println(content);
        
        return CommandReturn.success("ok");
    }
}