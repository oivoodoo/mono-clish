using System;
using System.Collections.Generic;
using System.Linq;
using Clish.Library.Models;

namespace Clish.Library
{
    /// <summary>
    /// Node of the command.
    /// </summary>
    public class CommandNode
    {
        #region [  Fields  ]

        private readonly Dictionary<String, CommandNode> m_commands = new Dictionary<String, CommandNode>();

        #endregion

        #region [  Constructors  ]

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNode"/> class.
        /// </summary>
        public CommandNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="command">The command.</param>
        /// <param name="parent">The parent.</param>
        public CommandNode(string name, Command command, CommandNode parent)
        {
            Name = name;
            Command = command;
            Parent = parent;
        }

        #endregion

        #region [  Properties  ]

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public Command Command { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public CommandNode Parent { get; set; }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        public Dictionary<String, CommandNode> Nodes
        {
            get { return m_commands; }
        }

        /// <summary>
        /// Gets the keys of the nodes.
        /// </summary>
        /// <value>The keys.</value>
        public String[] Keys
        {
            get
            {
                return Nodes.Select(keyValuePair => keyValuePair.Key).ToArray();
            }
        }

        /// <summary>
        /// Gets the linear nodes.
        /// </summary>
        /// <value>The linear nodes.</value>
        public List<CommandNode> LinearNodes
        {
            get { return GetNodes(new List<CommandNode>(), this); }
        }

        #endregion

        private List<CommandNode> GetNodes(List<CommandNode> collection, CommandNode parent)
        {
            foreach (KeyValuePair<string, CommandNode> pair in parent.Nodes)
            {
                if (pair.Value.Command != null)
                {
                    collection.Add(pair.Value);
                }
                GetNodes(collection, pair.Value);
            }
            return collection;
        }

        /// <summary>
        /// Adds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Add(Command command)
        {
            CreateNode(this, command, command.Name);
        }

        /// <summary>
        /// Creates the node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="command">The command.</param>
        /// <param name="line">The line.</param>
        private void CreateNode(CommandNode node, Command command, String line)
        {
            string[] splitted = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length > 0)
            {
                String key = splitted[0];
                Command reference = splitted.Length == 1 ? command : null;
                CommandNode created = new CommandNode(key, reference, this);
                if (!node.Nodes.ContainsKey(key))
                {
                    node.Nodes.Add(key, created);
                }
                if (splitted.Length != 1) // it'sn't last node
                {
                    CreateNode(node.Nodes[key], command, line.Replace(key, String.Empty).Trim());
                }
                else // this is last node
                {
                    // We have to set command value, because this node can be create before we 
                    // had command.
                    node.Nodes[key].Command = reference; 
                }
            }
        }

        /// <summary>
        /// Adds the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="line">The line.</param>
        public void Add(Command command, String line)
        {
            CreateNode(this, command, line);
        }

        /// <summary>
        /// Searches the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public List<CommandNode> Search(String line)
        {
            return Search(this, line);
        }

        /// <summary>
        /// Searches the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public List<CommandNode> Search(CommandNode node, String line)
        {
            var splitted = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length > 0)
            {
                if (splitted.Length == 1)
                {
                    return (from pair in node.Nodes where pair.Key.Contains(splitted[0]) select pair.Value).ToList();
                }
                if (node.Nodes.ContainsKey(splitted[0]))
                {
                    return Search(node.Nodes[splitted[0]], line.Replace(splitted[0], String.Empty).Trim());
                }
            }
            return new List<CommandNode>();
        }
    }
}