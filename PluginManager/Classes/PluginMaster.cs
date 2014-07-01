using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PluginInterfaces;

namespace PluginManager.Classes
{
    public class PluginMaster : IPluginMaster
    {
        public Queue<string> Messages { get; set; }

        public PluginMaster()
        {
            Messages = new Queue<string>();
        }
    }
}
