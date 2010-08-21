using System;
using System.Collections.Generic;
using System.Linq;
using Clish.Library.Models;
using Mono.Terminal;

namespace Clish.Library
{
    /// <summary>
    /// Contains session information. Be default session is global.
    /// </summary>
    public class Session
    {
        #region [  Fields  ]

        public const String DefaultPromt = "> "; // we can set for another version.

        private readonly Dictionary<String, PType> m_ptypes = new Dictionary<string, PType>();

        private String m_prompt = String.Empty;

        #endregion

        #region [  Properties  ]

        /// <summary>
        /// Gets the P types.
        /// </summary>
        /// <value>The P types.</value>
        public Dictionary<String, PType> PTypes
        {
            get { return m_ptypes; }
        }

        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        /// <value>The name of the view.</value>
        public String ViewName { get; set; }

        public String ViewId { get; set; }

        /// <summary>
        /// Gets or sets the command node.
        /// That's contains top of the colleciton of commands for running by view.
        /// </summary>
        /// <value>The command node.</value>
        public CommandNode CommandNode { get; set; }

        /// <summary>
        /// Gets or sets the prompt.
        /// </summary>
        /// <value>The prompt.</value>
        public String Prompt
        {
            get
            {
                return m_prompt ?? DefaultPromt;
            }
            set{
                m_prompt = value;
            }
        }

        public LineEditor LineEditor { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the application.
        /// Contains all commands splitted by view name.
        /// </summary>
        /// <value>The configuration.</value>
        public Configuration Configuration { get; set;  }

        #endregion

        #region [  Constructors  ]

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        public Session(Configuration configuration, String viewName)
        {
            Prompt = DefaultPromt;
            Configuration = configuration;
            UpdateSession(viewName, null);
        }

        public bool UpdateSession(String viewName, String viewId)
        {
            ViewName = viewName;
            if (!String.IsNullOrEmpty(ViewName) &&
                Configuration.Views.ContainsKey(ViewName))
            {
                ViewId = viewId;
                // Select prompt for showing in the terminal.
                List <View[]> views = Configuration.Modules.Where(m => m.Views != null).Select(m => m.Views).ToList();
                View view = null;
                foreach (var vs in views)
                {
                    view = vs.Where(i => i.Name == ViewName).FirstOrDefault();
                    if (view != null)
                    {
                        break;
                    }
                }
                
                Prompt = view != null ? view.Prompt : DefaultPromt;
                // Set top node of the colleciton commands filtered by view name.
                CommandNode = Configuration.Views[ViewName];
                Prompt = ApplyViewParams(Prompt);
                return true;
            }
            return false;
        }

        #endregion

        #region [  Methods  ]

        /// <summary>
        /// Shows the available commands.
        /// </summary>
        /// <returns></returns>
        public List<String> ShowAvailableCommands()
        {
            List<String> results = new List<String>();
            foreach (CommandNode node in Configuration.Views[ViewName].LinearNodes)
            {
                results.Add(String.Format("{0} - {1}", node.Command.Name, node.Command.Help));
            }
            return results;
        }

        /// <summary>
        /// Method impelement insertation of params to prompt.
        /// </summary>
        /// <param name="viewId"></param>
        public void UpdateSessionByViewParams(string viewId)
        {
            //  Example of params line: viewid="name=${name};operation=add"
            ViewId = viewId;
            Prompt = ApplyViewParams(Prompt);
        }

        public String ApplyViewParams(String raw)
        {
            if (!String.IsNullOrEmpty(ViewId))
            {
                var ps = ViewId.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in ps)
                {
                    var values = line.Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2)
                    {
                        // Remove cool hack from this line, when we found another way to bind xml attributes of view.
                        if (values[1] == "${name}")
                        {
                            raw = raw.Replace("${" + values[0] + "}", ViewName);
                        }
                        else
                        {
                            raw = raw.Replace("${" + values[0] + "}", values[1]);
                        }
                    }
                }
            }
            foreach (KeyValuePair<string, string> pair in DefinedVariables.Variables)
            {
                raw = raw.Replace(pair.Key, pair.Value);
            }
            return raw;
        }

        #endregion

        public void Show(string template)
        {
            Console.WriteLine(template);
            LineEditor.Render();
        }
    }
}