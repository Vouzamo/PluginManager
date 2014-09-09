using System.ComponentModel.Composition;
using System.Threading;
using PluginInterfaces;

namespace Plugin2
{
    [Export(typeof(IPlugin))]
    public class Plugin2 : IPlugin
    {
        public IPluginMaster Master { get; set; }
        public string Name { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }

        public Plugin2()
        {
            Name = "Plugin 2";
            MajorVersion = 1;
            MinorVersion = 0;
        }

        public void Before()
        {
            Master.Messages.Enqueue(Name + " Start");
        }

        public void Main()
        {
            Thread.Sleep(2000);
        }

        public void After()
        {
            Master.Messages.Enqueue(Name + " End");
        }
    }
}
