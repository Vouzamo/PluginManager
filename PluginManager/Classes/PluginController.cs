using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using PluginInterfaces;

namespace PluginManager.Classes
{
    public class PluginController
    {
        public IPluginMaster Master { get; set; }
        public Dictionary<IPluginDescriptor, IPlugin> Plugins { get; set; }  

        public PluginController(string pluginDirectory)
        {
            Master = new PluginMaster();
            Plugins = RegisterPlugins(pluginDirectory);
        }

        private Dictionary<IPluginDescriptor, IPlugin> RegisterPlugins(string pluginDirectory)
        {
            Dictionary<IPluginDescriptor, IPlugin> plugins = new Dictionary<IPluginDescriptor, IPlugin>();

            try
            {
                DirectoryInfo directory = new DirectoryInfo(pluginDirectory);

                FileInfo[] assemblies = directory.GetFiles("*.dll");
                foreach (FileInfo fileInfo in assemblies)
                {
                    Assembly assembly = Assembly.LoadFile(fileInfo.FullName);
                    Type[] types = assembly.GetTypes();

                    foreach (Type t in types)
                    {
                        Type[] interfaces = t.GetInterfaces();

                        foreach (Type i in interfaces)
                        {
                            if (i == typeof(IPlugin))
                            {
                                IPlugin plugin = Activator.CreateInstance(t) as IPlugin;

                                if (plugin != null)
                                {
                                    FileInfo[] descriptorXml = fileInfo.Directory.GetFiles(Path.GetFileNameWithoutExtension(fileInfo.Name) + ".xml");
                                    XDocument doc = XDocument.Load(descriptorXml.First().FullName);

                                    string name = doc.Descendants().First(x => x.Name == "PluginName").Value;
                                    IPluginDescriptor descriptor = new PluginDescriptor(name);

                                    plugins.Add(descriptor, plugin);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return plugins;
        }

        public void StartAll()
        {
            foreach (KeyValuePair<IPluginDescriptor, IPlugin> plugin in Plugins)
            {
                Execute(plugin.Key, plugin.Value);
            }
        }

        public Queue<string> GetAllMessages()
        {
            Queue<string> messages = new Queue<string>();

            foreach (KeyValuePair<IPluginDescriptor, IPlugin> plugin in Plugins)
            {
                foreach (string message in GetMessages(plugin.Key))
                {
                    messages.Enqueue(message);
                }
            }

            return messages;
        }

        public void Execute(IPluginDescriptor descriptor, IPlugin plugin)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Reset();
            stopWatch.Start();
            plugin.Before(descriptor, Master);
            stopWatch.Stop();
            Console.WriteLine("Before took: " + stopWatch.ElapsedTicks + " ticks");

            stopWatch.Reset();
            stopWatch.Start();
            plugin.Main(descriptor, Master);
            stopWatch.Stop();
            Console.WriteLine("Main took: " + stopWatch.ElapsedTicks + " ticks");

            stopWatch.Reset();
            stopWatch.Start();
            plugin.After(descriptor, Master);
            stopWatch.Stop();
            Console.WriteLine("After took: " + stopWatch.ElapsedTicks + " ticks");
        }

        public Queue<string> GetMessages(IPluginDescriptor descriptor)
        {
            return descriptor.Messages;
        }
    }
}
