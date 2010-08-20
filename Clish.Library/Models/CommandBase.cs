using System;
using System.Collections.Generic;

namespace Clish.Library.Models
{
    public class CommandBase : ModelBase
    {
        /// <summary>
        /// TODO: May be we can make extinsion for this method for running via terminal or
        /// use another system calls (may be via sockets for
        /// transfernig command and gettings results).
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        /// <param name="types">The types.</param>
        public virtual void Run(string rawCommand, Dictionary<string, PType> types)
        {
            var builder = new CommandBuilder(rawCommand, (Command)this, types);
            String action = builder.BuildCommand();
            // TODO: Make sys call for run action.
        }

        /// <summary>
        /// Runs the specified raw command.
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        public virtual void Run(String rawCommand)
        {
        }
    }
}