using System.Threading;
using PluginInterfaces;

namespace Plugin2
{
    public class Plugin2 : IPlugin
    {
        public void Before(IPluginDescriptor descriptor, IPluginMaster master)
        {
            descriptor.Messages.Enqueue(descriptor.Name + " Start");
        }

        public void Main(IPluginDescriptor descriptor, IPluginMaster master)
        {
            Thread.Sleep(2000);
        }

        public void After(IPluginDescriptor descriptor, IPluginMaster master)
        {
            descriptor.Messages.Enqueue(descriptor.Name + " End");
        }
    }
}
