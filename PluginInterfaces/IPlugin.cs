namespace PluginInterfaces
{
    public interface IPlugin
    {
        IPluginMaster Master { get; set; }
        string Name { get; set; }
        int MajorVersion { get; set; }
        int MinorVersion { get; set; }

        void Before();
        void Main();
        void After();
    }
}
