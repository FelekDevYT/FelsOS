using System;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class DiskInfoCommand : ICommand
{
    public string getCommandName()
    {
        return "diskinfo";
    }

    public string getDescription()
    {
        return "Displays information about the disk and file system.";
    }

    public CommandReturn execute(Argument[] args)
    {
        IO.printlncol("Drive Information 0:\\", ConsoleColor.DarkYellow);
        IO.print("File System Type : "); IO.printlncol("  " + Kernel.FileSystem.getFileSystemType(), ConsoleColor.DarkCyan);
        IO.print("Total Capacity   :"); IO.printlncol("  " + Kernel.FileSystem.getTotalSize()/1024/1024 + " MB", ConsoleColor.DarkCyan);
        IO.print("Free Space       :"); IO.printlncol("  " + Kernel.FileSystem.getAvailableFreeSpace()/1024/1024 + " MB", ConsoleColor.DarkCyan);
        
        return CommandReturn.success("ok");
    }
}