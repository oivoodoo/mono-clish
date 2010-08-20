using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    /// <summary>
    /// Class impelements entry point to xml configuration.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "CLISH_MODULE")]
    public class Module
    {
        /// <summary>
        /// Collection contains commands for clish terminal.
        /// </summary>
        private List<Command> m_commands = new List<Command>();

        /// <summary>
        /// Collection contains types for commands.
        /// </summary>
        private List<PType> m_ptypes = new List<PType>();

        /// <summary>
        /// Gets or sets the P types.
        /// </summary>
        /// <value>The P types.</value>
        [XmlArrayItem("PTYPE")]
        public List<PType> PTypes
        {
            get { return m_ptypes; }
            set { m_ptypes = value; }
        }

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>The commands.</value>
        [XmlArrayItem("COMMAND")]
        public List<Command> Commands
        {
            get { return m_commands; }
            set { m_commands = value; }
        }

        /// <summary>
        /// Gets or sets the overview.
        /// </summary>
        /// <value>The overview.</value>
        [XmlArrayItem("OVERVIEW")]
        public String Overview { get; set; }

        /// <summary>
        /// Gets or sets the startup.
        /// </summary>
        /// <value>The startup.</value>
        [XmlElement("STARTUP")]
        public Startup Startup { get; set; }
    }
}