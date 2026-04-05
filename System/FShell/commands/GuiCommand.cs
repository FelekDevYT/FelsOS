using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class GuiCommand : ICommand
{
    public string getCommandName()
    {
        return "gui";
    }

    public string getDescription()
    {
        return "Switches the system from text mode to Graphical User Interface mode";
    }

    public CommandReturn execute(Argument[] args)
    {
        Kernel.currentMode.stop();
        Kernel.currentMode = Kernel.gui;
        Kernel.currentMode.start();
        
        return CommandReturn.success("ok");
    }
}