using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterfaces
{
    public interface IPluginDescriptor
    {
        string Name { get; set; }
        Queue<string> Messages { get; set; } 
    }
}
