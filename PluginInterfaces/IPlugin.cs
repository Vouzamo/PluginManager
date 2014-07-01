namespace PluginInterfaces
{
    public interface IPlugin
    {
        void Before(IPluginDescriptor descriptor, IPluginMaster master);
        void Main(IPluginDescriptor descriptor, IPluginMaster master);
        void After(IPluginDescriptor descriptor, IPluginMaster master);
    }
}
