using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterfaces;

namespace PluginManager.Classes
{
    public class PluginDescriptor : IPluginDescriptor
    {
        public string Name { get; set; }
        public Queue<string> Messages { get; set; } 

        public PluginDescriptor(string name)
        {
            Name = name;
            Messages = new Queue<string>();
        }
    }
}
