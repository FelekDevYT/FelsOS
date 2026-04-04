using System;
using FenixOS.System.utils;
using NotImplementedException = System.NotImplementedException;

namespace FenixOS.System.FShell.commands;

public class InfoCommand : ICommand
{
    public string getCommandName()
    {
        return "info";
    }

    public string getDescription()
    {
        return "Displays version and system information for Fenix OS";
    }

    public CommandReturn execute(Argument[] args)
    {
        IO.printlncol("CPU: " + Cosmos.Core.CPU.GetCPUBrandString() + 
                      "\nCPU Vendor: " + Cosmos.Core.CPU.GetCPUVendorName() +
                      "\nAmount of RAM: " + Cosmos.Core.CPU.GetAmountOfRAM() +
                      "\nAvailable RAM: " + Cosmos.Core.GCImplementation.GetAvailableRAM() +
                      "\nUsed RAM: " + Cosmos.Core.GCImplementation.GetUsedRAM(), ConsoleColor.DarkYellow);

        return CommandReturn.success("ok");
    }
}