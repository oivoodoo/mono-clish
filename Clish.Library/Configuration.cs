using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Clish.Library.Models;
using Clish.Library.Utilities;

namespace Clish.Library
{
    /// <summary>
    ///     Class implements methods for loading configurations from directories
    /// and searching certain command.
    /// </summary>
    public class Configuration
    {
        public const String DefaultViewName = "DEFAULT_VIEW";
        public const String XmlFormat = ".xml";
        private static List<ClishModule> _mModules = new List<ClishModule>();
        private static Dictionary<String, PType> _mPtypes = new Dictionary<String, PType>();
        private readonly Dictionary<string, CommandNode> m_views = new Dictionary<string, CommandNode>();

        /// <summary>
        /// Load configuration settings from directory.
        /// </summary>
        /// <param name="path">to configuration directory</param>
        public Configuration(String path)
        {
            CreateConfigurationByDirectory(path);
        }

        /// <summary>
        /// Gets the views.
        /// </summary>
        /// <value>The views.</value>
        public Dictionary<String, CommandNode> Views
        {
            get { return m_views; }
        }

        /// <summary>
        /// Commands with various modules for clish terminal.
        /// </summary>
        public static List<ClishModule> Modules
        {
            get { return _mModules; }
            set { _mModules = value; }
        }

        /// <summary>
        /// Gets or sets the P types.
        /// </summary>
        /// <value>The P types.</value>
        public static Dictionary<String, PType> PTypes
        {
            get { return _mPtypes; }
            set { _mPtypes = value; }
        }

        /// <summary>
        /// Creates the configuration by directory.
        /// </summary>
        /// <param name="path">The path.</param>
        private void CreateConfigurationByDirectory(String path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (ClishModule module in from file in files
                                               where file.Contains(XmlFormat)
                                               select FileUtility.ReadFile(file)
                                               into content
                                               select XmlUtility.LoadFromXmlWithoutNamespaces<ClishModule>(content))
                {
                    Modules.Add(module);
                }
                MergeCollections();
            }
        }

        /// <summary>
        ///     Merge collections ptypes and commands for more simple object access
        /// when we will try to search command with params.
        /// </summary>
        private void MergeCollections()
        {
            foreach (ClishModule module in Modules)
            {
                if (module.Commands != null)
                {
                    AddCommands(module.Commands);
                    if (module.View != null)
                    {
                        AddCommands(module.View.Commands);
                    }
                }
                if (module.PTypes != null)
                {
                    foreach(PType type in module.PTypes)
                    {
                        PTypes.Add(type.Name, type);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the commands.
        /// </summary>
        /// <param name="commands">The commands.</param>
        private void AddCommands(Command[] commands)
        {
            foreach (Command command in commands)
            {
                String viewName = String.IsNullOrEmpty(command.View) ? DefaultViewName : command.View;
                AddToView(viewName, command);
            }
        }

        /// <summary>
        /// Adds to view.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="command">The command.</param>
        private void AddToView(String name, Command command)
        {
            if (!Views.ContainsKey(name))
            {
                Views.Add(name, new CommandNode());
            }
            Views[name].Add(command);
        }
    }
}