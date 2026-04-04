using System;

namespace FenixOS.System.FShell;

public interface ICommand
{
    String getCommandName();
    String getDescription();
    CommandReturn execute(Argument[] args);
}