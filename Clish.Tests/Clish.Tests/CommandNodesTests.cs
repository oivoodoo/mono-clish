using System.Collections.Generic;
using Clish.Library;
using Clish.Library.Models;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clish.Tests
{
    /// <summary>
    /// Summary description for CommandNodesTests
    /// </summary>
    [TestClass]
    public class CommandNodesTests
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
        public void CreationOneLevelCommandTest()
        {
            var node = new CommandNode();
            var command = new Command() {Name = "show"};
            node.Add(command);

            Assert.IsTrue(node.Parent == null);
            Assert.IsNull(node.Command);
            Assert.IsTrue(node.Nodes.ContainsKey("show"));
            Assert.IsTrue(node.Nodes["show"].Command == command);
            // Assert.IsTrue(node.Nodes.First().Value.First().Command == "show");
        }

        [TestMethod]
        public void CreationTwoLevelCommandTest()
        {
            var node = new CommandNode();
            var command = new Command() { Name = "show config" };
            node.Add(command);

            Assert.IsTrue(node.Parent == null);
            Assert.IsNull(node.Command);
            Assert.IsTrue(node.Nodes.ContainsKey("show"));
            Assert.IsTrue(node.Nodes["show"].Command == null);


            Assert.IsTrue(node.Nodes["show"].Name == "show config");
            Assert.IsTrue(node.Nodes["show"].Nodes.ContainsKey("config"));
            Assert.IsTrue(node.Nodes["show"].Nodes["config"].Command == command);
            // Assert.IsTrue(node.Nodes.First().Value.First().Command == "show");
        }

        [TestMethod]
        public void CreationThreeLevelCommandTest1()
        {
            var node = new CommandNode();
            var command1 = new Command{ Name = "show config" };
            var command2 = new Command {Name = "show" };

            node.Add(command2);
            node.Add(command1);

            Assert.IsTrue(node.Parent == null);
            Assert.IsNull(node.Command);
            Assert.IsTrue(node.Nodes.ContainsKey("show"));
            Assert.IsTrue(node.Nodes["show"].Command == command2);

            Assert.IsTrue(node.Nodes["show"].Nodes.ContainsKey("config"));
            Assert.IsTrue(node.Nodes["show"].Nodes["config"].Command == command1);
        }

        [TestMethod]
        public void CreationThreeLevelCommandTest2()
        {
            var node = new CommandNode();
            var command1 = new Command { Name = "show config" };
            var command2 = new Command { Name = "show" };

            node.Add(command1);
            node.Add(command2);

            Assert.IsTrue(node.Parent == null);
            Assert.IsNull(node.Command);
            Assert.IsTrue(node.Nodes.ContainsKey("show"));
            Assert.IsTrue(node.Nodes["show"].Command == command2);

            Assert.IsTrue(node.Nodes["show"].Nodes.ContainsKey("config"));
            Assert.IsTrue(node.Nodes["show"].Nodes["config"].Command == command1);
        }

        [TestMethod]
        public void SearchThreeLevelCommandTest2()
        {
            var node = new CommandNode();
            var command1 = new Command { Name = "show config" };
            var command2 = new Command { Name = "show" };

            node.Add(command1);
            node.Add(command2);

            List<CommandNode> cn1 = node.Search("show");
            Assert.IsNotNull(cn1);
            Assert.IsTrue(cn1.Count == 1);
            Assert.IsTrue(cn1.First().Name == "show");
            Assert.IsNotNull(cn1.First().Command);

            List<CommandNode> cn2 = node.Search("test");
            Assert.IsNotNull(cn2);
            Assert.IsTrue(cn2.Count == 0);

            List<CommandNode> cn3 = node.Search("sh");
            Assert.IsNotNull(cn3);
            Assert.IsTrue(cn3.First().Name == "show");
            Assert.IsNotNull(cn3.Count == 1);

            List<CommandNode> cn4 = node.Search("show c");
            Assert.IsNotNull(cn4);
            Assert.IsTrue(cn4.First().Name == "config");
            Assert.IsNotNull(cn3.Count == 1);
        }
        
        [TestMethod]
        public void GetLinearNodesTest()
        {
            var node = new CommandNode();
            var command1 = new Command { Name = "show config" };
            var command2 = new Command { Name = "show" };
            var command3 = new Command { Name = "show test" };
            var command4 = new Command { Name = "linear" };

            node.Add(command1);
            node.Add(command2);
            node.Add(command3);
            node.Add(command4);

            int count = 0;
            foreach (var n in node.LinearNodes)
            {
                count++;
            }
            Assert.IsTrue(count == 4);
        }
    }
}