using System;
using FenixOS.System.modes.cli;
using FenixOS.System.utils;
using UniLua;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class CalcCommand : ICommand
{
    public string getCommandName()
    {
        return "calc";
    }

    public string getDescription()
    {
        return "Evaluates mathematical expressions and returns the result";
    }

    public CommandReturn execute(Argument[] args)
    {
        if (args.Length == 0)
        {
            IO.Debug.error("Usage: calc <expr1> <expr2> <exprN>");
            return CommandReturn.error("Usage: calc <expr1> <expr2> <exprN>");
        }

        String expression = "";
        foreach (Argument arg in args)
        {
            String parsed = arg.argument;

            expression += parsed;
        }

        var luaApi = LuaAPI.NewState();
        luaApi.L_OpenLibs();
        luaApi.L_DoString("print(" + expression + ")");//TODO: Change with getting answer and printing using IO.println();
        
        return CommandReturn.success("ok");
    }
}