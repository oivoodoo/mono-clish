using System;
using System.Collections.Generic;
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

        [XmlIgnore]
        public String ToRun { get; set; }

        [XmlIgnore]
        public List<String> ParsedParams { get; set; }

        /// <summary>
        /// Runs the specified raw command.
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        public virtual bool Run(String rawCommand)
        {
            if (IsValidCommand(rawCommand))
            {
                // SysCall
                // Session.Print()
                return true;
            }
            return false;
        }

        protected bool IsValidCommand(String rawCommand)
        {
            var builder = new CommandBuilder(rawCommand, (Command)this, Configuration.PTypes);
            try
            {
                ToRun = builder.BuildCommand();
                ParsedParams = builder.ParsedParams;
                var command = ((Command) this);
                if (!String.IsNullOrEmpty(command.ViewId))
                {
                    Session.UpdateSessionByViewParams(command.ViewId);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return !String.IsNullOrEmpty(ToRun);
        }
    }
}