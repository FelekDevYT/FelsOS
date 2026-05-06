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
        CommandManager = registerDefaultCommands();
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
        Heap.Collect();
    }

    public static CommandManager registerDefaultCommands()
    {
        CommandManager cmd = new CommandManager();
        
        cmd.registerCommand(new InfoCommand());
        cmd.registerCommand(new CdCommand());
        cmd.registerCommand(new lsCommand());
        cmd.registerCommand(new ClearCommand());
        cmd.registerCommand(new RetcodeCommand());
        cmd.registerCommand(new HelpCommand());
        cmd.registerCommand(new MkFileCommand());
        cmd.registerCommand(new MkDirCommand());
        cmd.registerCommand(new RmCommand());
        cmd.registerCommand(new DiskInfoCommand());
        cmd.registerCommand(new STECommand());
        cmd.registerCommand(new LuaCommand());
        cmd.registerCommand(new ShutdownCommand());
        cmd.registerCommand(new RebootCommand());
        cmd.registerCommand(new CatCommand());
        cmd.registerCommand(new CalcCommand());
        cmd.registerCommand(new GuiCommand());

        return cmd;
    }
}