using Clish.Library.Models;

namespace Clish.Library.Commands
{
    /// <summary>
    /// Base class for custom terminal clish commands such as 
    /// clish_close, history, top or another.
    /// </summary>
    public class TerminalCommand : Command
    {
        public TerminalCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminalCommand"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public TerminalCommand(Session session) : base(session)
        {
        }

        /// <summary>
        /// Runs the specified raw command.
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        public override bool Run(Session session, string rawCommand)
        {
            if (View != null)
            {
                Session.UpdateSession(View, ViewId);
            }
            return true;
        }
    }
}