using System;
using System.Collections.Generic;
using System.Linq;
using Clish.Library;
using Clish.Library.Models;

namespace Clish
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
            set
            {
                m_prompt = value;
            }
        }

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
            ViewName = viewName;
            if (!String.IsNullOrEmpty(ViewName) &&
                Configuration.Views.ContainsKey(ViewName))
            {
                // Select prompt for showing in the terminal.
                View view = Configuration.Modules.Where(m => m.View != null && m.View.Name == ViewName).Select(m => m.View).FirstOrDefault();
                Prompt = view != null ? view.Prompt : DefaultPromt;
                // Set top node of the colleciton commands filtered by view name.
                CommandNode = Configuration.Views[ViewName];
            }
        }

        #endregion
    }
}