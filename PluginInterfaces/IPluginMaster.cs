using System.Collections.Generic;

namespace PluginInterfaces
{
    public interface IPluginMaster
    {
        Queue<string> Messages { get; set; }
    }
}