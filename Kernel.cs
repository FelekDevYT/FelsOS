using System;
using System.Collections.Generic;
using System.Text;
using FenixOS.System.EventSystem;
using FenixOS.System.EventSystem.events;
using FenixOS.System.modes;
using FenixOS.System.modes.cli;
using FenixOS.System.modes.gui;
using FenixOS.System.modes.panic;
using FenixOS.System.utils;
using Sys = Cosmos.System;

namespace FenixOS
{
    public class Kernel : Sys.Kernel
    {
        public static IMode cli;
        public static IMode gui;

        public static IMode currentMode;

        public static FileSystem FileSystem;
        public static EventManager eventManager;

        protected override void OnBoot()
        {
            base.OnBoot();
            
            //custom
            cli = new CLIMode();
            gui = new GUIMode();

            currentMode = cli;
            
            eventManager = new EventManager();
            eventManager.init();
        }

        protected override void BeforeRun()
        {
            FileSystem = new FileSystem();
            FileSystem.registerFileSystem();
            
            currentMode.start();
        }

        protected override void Run()
        {
            try
            {
                currentMode.update();
            }
            catch (Exception e)
            {
                currentMode = new PanicMode(e.Message, e.HResult);
            }
        }
    }
}
