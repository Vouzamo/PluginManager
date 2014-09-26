using System.Composition;
using System.Threading;
using PluginInterfaces;

namespace Plugin3
{
    [Export(typeof(IPlugin))]
    public class Plugin3 : IBasicPlugin
    {
        public IPluginMaster Master { get; set; }
        public string Name { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }

        public Plugin3()
        {
            Name = "Plugin 3";
            MajorVersion = 2;
            MinorVersion = 5;
        }

        public void Before()
        {
            Master.Messages.Enqueue(Name + " ENTRE");
        }

        public void Main()
        {
            Thread.Sleep(3000);
        }

        public void After()
        {
            Master.Messages.Enqueue(Name + " FIN");
        }

        public void DoSomethingBasic()
        {
            Master.Messages.Enqueue("I did something BASIC!");
        }
    }
}
