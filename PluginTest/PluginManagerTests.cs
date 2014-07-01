using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluginManager.Classes;

namespace PluginTest
{
    [TestClass]
    public class PluginManagerTests
    {
        private const string PluginDirectory = "C:\\Plugins";

        [TestMethod]
        public void StartAll()
        {
            PluginController pluginController = new PluginController(PluginDirectory);
            pluginController.StartAll();

            Queue<string> messages = pluginController.GetAllMessages();
            foreach (string message in messages)
            {
                Debug.WriteLine(message);
            }

            Assert.IsTrue(messages.Count > 0);
        }
    }
}
