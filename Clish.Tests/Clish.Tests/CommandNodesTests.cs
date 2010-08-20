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

            CommandNode cn1 = node.Search("show");
            Assert.IsTrue(cn1.Name == "show");
            Assert.IsNotNull(cn1.Command);
            Assert.IsNotNull(cn1);

            CommandNode cn2 = node.Search("test");
            Assert.IsNull(cn2);

            CommandNode cn3 = node.Search("show config");
            Assert.IsNotNull(cn3);
            Assert.IsTrue(cn3.Name == "config");
            Assert.IsNotNull(cn3.Command);
        }
        
    }
}