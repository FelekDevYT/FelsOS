using System;

namespace FenixOS.System.FShell;

public class CommandReturn
{
    public CommandReturnType type;
    public String message;

    private CommandReturn(CommandReturnType type, String message)
    {
        this.type = type;
        this.message = message;
    }

    public static CommandReturn success(String msg)
    {
        return new CommandReturn(CommandReturnType.SUCCESS, msg);
    }

    public static CommandReturn error(String msg)
    {
        return new CommandReturn(CommandReturnType.FAILURE, msg);
    }

    public static CommandReturn warning(String msg)
    {
        return new CommandReturn(CommandReturnType.WARNING, msg);
    }
}