using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class RebootCommand : ICommand
{
    public string getCommandName()
    {
        return "reboot";
    }

    public string getDescription()
    {
        return "Restarts the operating system";
    }

    public CommandReturn execute(Argument[] args)
    {
        Cosmos.System.Power.Reboot();
        
        return CommandReturn.success("ok");
    }
}