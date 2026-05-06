using System;
using FenixOS.System.modes.cli;
using UniLua;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class LuaCommand : ICommand
{
    public string getCommandName() => "lua";

    public string getDescription() => "you can run any of your scripts on lua";

    public CommandReturn execute(Argument[] args)
    {
        if (args.Length != 1)
        {
            return CommandReturn.error("Usage: lua <filename>");
        }

        String code = Kernel.FileSystem.readFile(args[1].argument);
        
        var luaApi = LuaAPI.NewState();
        luaApi.L_OpenLibs();
        var status = luaApi.L_DoString(code);
        if (status != ThreadStatus.LUA_OK)
        {
            string errorMsg = luaApi.ToString(-1);
            return CommandReturn.error("Lua Error: " + errorMsg);
        }
        
        return CommandReturn.success("script executed");
    }
}