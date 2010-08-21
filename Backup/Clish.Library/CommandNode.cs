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

        private String m_fullName = String.Empty;

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
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public String FullName
        {
            get
            {
                if (String.IsNullOrEmpty(m_fullName))
                {
                    m_fullName = GetFullName(m_fullName, this).Trim();
                }
                return m_fullName;
            }
        }

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

        private String GetFullName(String fullName, CommandNode parent)
        {
            if (parent.Parent != null)
            {
                fullName = GetFullName(fullName, parent.Parent);
            }
            return fullName + " " + parent.Name;
        }

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
                CommandNode created = new CommandNode(key, reference, node);
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
                    return (from pair in node.Nodes where pair.Key.IndexOf(splitted[0]) ==0 select pair.Value).ToList();
                }
                if (node.Nodes.ContainsKey(splitted[0]))
                {
                    return Search(node.Nodes[splitted[0]], line.Replace(splitted[0], String.Empty).Trim());
                }
            }
            return new List<CommandNode>();
        }

        /// <summary>
        /// Search command node on the one layer, don't go to deep of Nodes.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public List<CommandNode> OneSearch(String line)
        {
            return (from pair in Nodes where pair.Key.IndexOf(line) == 0 select pair.Value).ToList();
        }

        /// <summary>
        /// Search the deeper node by draft command line.
        /// </summary>
        /// <param name="line">The line  part of line, it's the command params.</param>
        /// <returns></returns>
        public CommandNode SearchDeeper(ref String line)
        {
            return SearchDeeper(this, ref line, String.Empty);
        }

        private CommandNode SearchDeeper(CommandNode node, ref String line, String command)
        {
            // We try to find maximum deep for typed command.
            var nodes = new List<CommandNode>();
            var result = node;
            var splitted = line.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length > 0)
            {
                command += " " + splitted[0];
                String[] cs = command.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                nodes = node.Nodes.Where(p => p.Key == cs[cs.Length - 1]).Select(p => p.Value).ToList();
                if (nodes.Count == 1)
                {
                    // Remove only the first entrance of line!
                    int index = line.IndexOf(cs[cs.Length - 1]);
                    if (index != -1)
                    {
                        line = line.Substring(cs[cs.Length - 1].Length).Trim();
                    }
                    result = SearchDeeper(nodes.First(), ref line, command);
                }
            }
            return result;
        }
    }
}