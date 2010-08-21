using System;
using Clish.Library;
using Clish.Library.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clish.Tests
{
    [TestClass]
    public class CommandBuilderTests
    {
         /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
        
        [TestMethod]
        public void ResolveParamTest1()
        {
            Configuration configuration =null;
            try
            {
                configuration = new Configuration("test");
            }
            catch
            {
            }
            var session = new Session(configuration, Configuration.DefaultViewName);

            try
            {
                if (!configuration.Views.ContainsKey(Configuration.DefaultViewName))
                {
                    configuration.Views.Add(Configuration.DefaultViewName, new CommandNode());
                }
                configuration.Views[Configuration.DefaultViewName].Add(new ViewCommand(session));
                String param = CommandBuilder.ResolveParam("debug", Configuration.PTypes["view_name_string"]);
                Assert.IsTrue(param == "debug");
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void ResolveParamTest2()
        {
            Configuration configuration = null;
            try
            {
                configuration = new Configuration("test");
            }
            catch
            {
            }
            var session = new Session(configuration, Configuration.DefaultViewName);

            try
            {
                if (!configuration.Views.ContainsKey(Configuration.DefaultViewName))
                {
                    configuration.Views.Add(Configuration.DefaultViewName, new CommandNode());
                }
                configuration.Views[Configuration.DefaultViewName].Add(new ViewCommand(session));
                String param = CommandBuilder.ResolveParam("root-view", Configuration.PTypes["view_name_string"]);
                Assert.IsTrue(param == "root-view");
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}