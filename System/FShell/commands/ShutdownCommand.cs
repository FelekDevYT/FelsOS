using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class ShutdownCommand : ICommand
{
    public string getCommandName()
    {
        return "shutdown";
    }

    public string getDescription()
    {
        return "Safely powers off the system";
    }

    public CommandReturn execute(Argument[] args)
    {
        Cosmos.System.Power.Shutdown();
        
        return CommandReturn.success("ok");
    }
}