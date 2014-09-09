using System.ComponentModel.Composition;
using System.Threading;
using PluginInterfaces;

namespace Plugin1
{
    [Export(typeof(IPlugin))]
    public class Plugin1 : IPlugin
    {
        public IPluginMaster Master { get; set; }
        public string Name { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }

        public Plugin1()
        {
            Name = "Plugin 1";
            MajorVersion = 0;
            MinorVersion = 1;
        }

        public void Before()
        {
            Master.Messages.Enqueue(Name + " Before");
        }

        public void Main()
        {
            Thread.Sleep(1000);
        }

        public void After()
        {
            Master.Messages.Enqueue(Name + " After");
        }
    }
}
