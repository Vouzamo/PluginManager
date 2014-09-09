using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using PluginInterfaces;
using System.ComponentModel.Composition;

namespace PluginManager.Classes
{
    public class PluginController
    {
        public IPluginMaster Master { get; set; }
        
        [ImportMany(typeof(IPlugin))]
        public IEnumerable<IPlugin> Plugins { get; set; }  

        public PluginController(string pluginDirectory)
        {
            Master = new PluginMaster();
            RegisterPlugins(pluginDirectory);
        }

        private void RegisterPlugins(string pluginDirectory)
        {
            try
            {
                var aggregateCatalog = new AggregateCatalog();

                var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                var directoryCatalog = new DirectoryCatalog(pluginDirectory);

                aggregateCatalog.Catalogs.Add(assemblyCatalog);
                aggregateCatalog.Catalogs.Add(directoryCatalog);

                //var container = new CompositionContainer(aggregateCatalog);
                var container = new CompositionContainer(directoryCatalog);
                container.ComposeParts(this);

                foreach (IPlugin plugin in Plugins)
                {
                    plugin.Master = Master;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<T> GetPlugins<T>()
        {
            return Plugins.OfType<T>().ToList();
        }

        public void StartAll()
        {
            foreach (IPlugin plugin in Plugins)
            {
                if (plugin is IBasicPlugin)
                {
                    IBasicPlugin basicPlugin = plugin as IBasicPlugin;
                    basicPlugin.DoSomethingBasic();
                }
                else
                {
                    Execute(plugin);
                }
            }
        }

        public Queue<string> GetAllMessages()
        {
            Queue<string> messages = new Queue<string>();

            foreach (IPlugin plugin in Plugins)
            {
                messages.Enqueue(plugin.Name + "[" + plugin.MajorVersion + "." + plugin.MinorVersion + "]");
            }

            foreach (string message in Master.Messages)
            {
                messages.Enqueue(message);
            }

            return messages;
        }

        public void Execute(IPlugin plugin)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Reset();
            stopWatch.Start();
            plugin.Before();
            stopWatch.Stop();
            Console.WriteLine("Before took: " + stopWatch.ElapsedTicks + " ticks");

            stopWatch.Reset();
            stopWatch.Start();
            plugin.Main();
            stopWatch.Stop();
            Console.WriteLine("Main took: " + stopWatch.ElapsedTicks + " ticks");

            stopWatch.Reset();
            stopWatch.Start();
            plugin.After();
            stopWatch.Stop();
            Console.WriteLine("After took: " + stopWatch.ElapsedTicks + " ticks");
        }
    }
}
