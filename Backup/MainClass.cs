using System;
using Clish.Logs.Logs;

namespace Clish
{
    ///<summary>
    /// Entry point for clish application.
    ///</summary>
    public class MainClass
    {
        private static Application m_app;

        public static void Main(String[] args)
        {
            try
            {
                m_app = new Application();
                m_app.Run(args);
            }
            catch (Exception ex)
            {
                Log.CoreLog.Error(ex);
            }
        }

//        private void TestXmlConfigurations()
//        {
//            ClishModule module = null;
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/startup.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/types.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/script-view.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/root-view.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/network-commands.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/named-view.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/global-commands.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/debug-view.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/debug-commands.xml"));
//            module = XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(FileUtility.ReadFile("xml-examples/clock.xml"));
//        }
    }
}