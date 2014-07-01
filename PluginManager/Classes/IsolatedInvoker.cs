using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace PluginManager.Classes
{
    public static class IsolatedInvoker
    {
        // main Invoke method
        public static object Invoke(string assemblyFile, string typeName)
        {
            // resolve path
            assemblyFile = Path.Combine(Environment.CurrentDirectory, assemblyFile);
            Debug.Assert(assemblyFile != null);

            // get base path
            var appBasePath = Path.GetDirectoryName(assemblyFile);
            Debug.Assert(appBasePath != null);

            // change current directory
            var oldDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = appBasePath;
            try
            {
                // create new app domain
                var domain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), null, appBasePath, null, false);
                try
                {
                    // create instance
                    var invoker = (InvokerHelper)domain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location, typeof(InvokerHelper).FullName);

                    // invoke method
                    return invoker.InvokeHelper(assemblyFile, typeName);
                }
                finally
                {
                    // unload app domain
                    AppDomain.Unload(domain);
                }
            }
            finally
            {
                // revert current directory
                Environment.CurrentDirectory = oldDirectory;
            }
        }

        // This helper class is instantiated in an isolated app domain
        private class InvokerHelper : MarshalByRefObject
        {
            // This helper function is executed in an isolated app domain
            public object InvokeHelper(string assemblyFile, string typeName)
            {
                // create an instance of the target object
                var handle = Activator.CreateInstanceFrom(assemblyFile, typeName);

                // get the instance of the target object
                var instance = handle.Unwrap();

                // success
                return instance;
            }
        }
    }
}
