using System.Threading;
using PluginInterfaces;

namespace Plugin1
{
    public class Plugin1 : IPlugin
    {
        public void Before(IPluginDescriptor descriptor, IPluginMaster master)
        {
            descriptor.Messages.Enqueue(descriptor.Name + " Before");
        }

        public void Main(IPluginDescriptor descriptor, IPluginMaster master)
        {
            Thread.Sleep(1000);
        }

        public void After(IPluginDescriptor descriptor, IPluginMaster master)
        {
            descriptor.Messages.Enqueue(descriptor.Name + " After");
        }
    }
}
