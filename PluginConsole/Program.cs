using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using PluginInterfaces;
using PluginManager.Classes;

namespace PluginConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string pluginDirectory = ConfigurationManager.AppSettings["pluginDirectory"];

            PluginController pluginController = new PluginController(pluginDirectory);

            Console.WriteLine(pluginController.Plugins.Count() + " plugins loaded!");
            Console.WriteLine();
            Console.Write("Press any key to begin: ");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            foreach (IBasicPlugin basicPlugin in pluginController.GetPlugins<IBasicPlugin>())
            {
                basicPlugin.DoSomethingBasic();
            }

            foreach (IPlugin plugin in pluginController.GetPlugins<IPlugin>())
            {
                pluginController.Execute(plugin);
            }

            foreach (string message in pluginController.GetAllMessages())
            {
                Console.WriteLine(message);
            }

            Console.Write("Press any key to exit: ");
            Console.ReadKey();
        }
    }
}
