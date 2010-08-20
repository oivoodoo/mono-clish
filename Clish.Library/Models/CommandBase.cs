using System;
using System.Xml.Serialization;

namespace Clish.Library.Models
{
    public class CommandBase : ModelBase
    {
        public CommandBase()
        {
        }

        public CommandBase(Session session)
        {
            Session = session;
        }

        [XmlIgnore]
        public Session Session { get; set; }

        /// <summary>
        /// Runs the specified raw command.
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        public virtual void Run(String rawCommand)
        {
        }
    }
}