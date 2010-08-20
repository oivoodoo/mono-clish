using Clish.Library.Models;

namespace Clish.Commands
{
    /// <summary>
    /// Base class for custom terminal clish commands such as 
    /// clish_close, history, top or another.
    /// </summary>
    public class TerminalCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerminalCommand"/> class.
        /// </summary>
        /// <param name="application">The application.</param>
        public TerminalCommand(Application application)
        {
            Application = application;
        }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>The application.</value>
        protected Application Application { get; set; }

        /// <summary>
        /// Runs the specified raw command.
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        public override void Run(string rawCommand)
        {
            base.Run(rawCommand, Application.CurrentSession.PTypes);
        }
    }
}