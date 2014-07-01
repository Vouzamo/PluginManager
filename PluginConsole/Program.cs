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

            Console.WriteLine(pluginController.Plugins.Count + " plugins loaded!");
            Console.WriteLine();
            Console.Write("Press any key to begin: ");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            foreach (KeyValuePair<IPluginDescriptor, IPlugin> plugin in pluginController.Plugins)
            {
                Console.WriteLine("------ " + plugin.Key.Name + " ------");

                pluginController.Execute(plugin.Key, plugin.Value);

                Console.WriteLine();
            }

            Console.Write("Press any key to exit: ");
            Console.ReadKey();
        }
    }
}
