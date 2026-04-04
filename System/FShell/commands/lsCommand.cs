using System;
using System.IO;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class lsCommand : ICommand
{
    public string getCommandName()
    {
        return "ls";
    }

    public string getDescription()
    {
        return "Lists files and directories in the current location";
    }

    public CommandReturn execute(Argument[] args)
    {
        var dirs = Directory.GetDirectories(Kernel.FileSystem.currentDir);
        var files = Directory.GetFiles(Kernel.FileSystem.currentDir);
        
        IO.printlncol("Directories: ", ConsoleColor.Yellow);
        if (dirs.Length == 0) IO.println("  <none>");
        foreach (var dir in dirs)
        {
            IO.println($"  - {Path.GetFileName(dir)}");
        }
        
        IO.println("");
        IO.printlncol("Files:", ConsoleColor.Cyan);
        if (files.Length == 0) IO.println("  <none>");
        foreach (var file in files)
        {
            IO.println($"  - {Path.GetFileName(file)}");
        }
        
        return CommandReturn.success("ok");
    }
}