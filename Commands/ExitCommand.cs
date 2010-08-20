using System;
using Clish.Library;

namespace Clish.Commands
{
    /// <summary>
    /// Close session of the current view.
    /// </summary>
    public class ExitCommand : TerminalCommand
    {
        public const String CommandName = "exit";

        public ExitCommand(Session application) : base(application)
        {
            Name = CommandName;
            Help = "Exit this CLI session";
        }

        public override void Run(String rawCommand)
        {
        }
    }
}