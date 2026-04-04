using System;
using Cosmos.Core.Memory;
using FenixOS.System.FShell;
using FenixOS.System.FShell.commands;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.modes.cli;

public class CLIMode : IMode
{
    public CommandManager CommandManager;
    
    public void start()
    {
        //let's start
        CommandManager = new CommandManager();
        registerDefaultCommands();
        IO.printlncol("Welcome to " + Data.OS_FULL, ConsoleColor.DarkYellow);
    }

    public void update()
    {
        IO.printcolor("user", Data.USER_COLOR);
        IO.printcolor("@", Data.DOG_COLOR);
        IO.printcolor(Kernel.FileSystem.currentDir, Data.PATH_COLOR);
        IO.printcolor("> ", Data.WELCOME_COLOR);
        
        String command = IO.readln();
        if (String.IsNullOrEmpty(command))
        {
            return;
        }
        
        string[] parts = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string commandName = parts[0];
        string[] args = new string[parts.Length - 1];
        Array.Copy(parts, 1, args, 0, parts.Length - 1);// IT TOOK 1 HOUR!
        //i hate C# btw
        CommandManager.execute(commandName, args);

        Heap.Collect();
    }

    public void stop()
    {
        //not needed
    }

    private void registerDefaultCommands()
    {
        CommandManager.registerCommand(new InfoCommand());
        CommandManager.registerCommand(new CdCommand());
        CommandManager.registerCommand(new lsCommand());
        CommandManager.registerCommand(new ClearCommand());
        CommandManager.registerCommand(new RetcodeCommand());
        CommandManager.registerCommand(new HelpCommand());
        CommandManager.registerCommand(new MkFileCommand());
        CommandManager.registerCommand(new MkDirCommand());
        CommandManager.registerCommand(new RmCommand());
        CommandManager.registerCommand(new DiskInfoCommand());
        CommandManager.registerCommand(new ShutdownCommand());
        CommandManager.registerCommand(new RebootCommand());
        CommandManager.registerCommand(new CatCommand());
    }
}