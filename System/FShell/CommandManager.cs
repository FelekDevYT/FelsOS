using System;
using System.Collections.Generic;
using FenixOS.System.utils;

namespace FenixOS.System.FShell;

public class CommandManager
{
    private Dictionary<String, ICommand> registeredCommands;
    private CommandReturn lastReturn = CommandReturn.error("nothing");

    public CommandManager()
    {
        registeredCommands = new Dictionary<String, ICommand>();
    }

    public void registerCommand(ICommand command)
    {
        registeredCommands.Add(command.getCommandName(), command);
    }

    public Dictionary<String, ICommand> getRegisteredCommands()
    {
        return registeredCommands;
    }

    public void execute(String commandName, String[] args)
    {
        if (registeredCommands.ContainsKey(commandName))
        {
            ICommand command = registeredCommands[commandName];
            Argument[] arguments = getArguments(args);
            
            CommandReturn ret = command.execute(arguments);
            if (ret.type == CommandReturnType.FAILURE)
            {
                IO.Debug.error(ret.message);
            } else if (ret.type == CommandReturnType.WARNING)
            {
                IO.Debug.info(ret.message);//i know, this is info, not warning lol
            }
            
            lastReturn = ret;
        }
    }

    public CommandReturn getLastReturn()
    {
        return lastReturn;
    }

    private Argument[] getArguments(String[] args)
    {
        Argument[] arguments = new Argument[args.Length];

        for (int i = 0; i < args.Length; i++)
        {
            arguments[i] = new Argument(args[i]);
        }
        return arguments;
    }
}