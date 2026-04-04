using System;
using System.Collections.Generic;
using System.Text;
using FenixOS.System.modes;
using FenixOS.System.modes.cli;
using Sys = Cosmos.System;

namespace FenixOS
{
    public class Kernel : Sys.Kernel
    {
        public static IMode cli;
        public static IMode gui;

        public static FileSystem FileSystem;

        protected override void OnBoot()
        {
            base.OnBoot();
            
            //custom
            cli = new CLIMode();
        }

        protected override void BeforeRun()
        {
            FileSystem = new FileSystem();
            FileSystem.registerFileSystem();
            
            cli.start();
        }

        protected override void Run()
        {
            cli.update();
        }
    }
}
