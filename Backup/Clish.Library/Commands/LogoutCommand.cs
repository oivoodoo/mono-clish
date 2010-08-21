using System;

namespace Clish.Library.Commands
{
    /// <summary>
    /// Clish native exit command.
    /// </summary>
    public class LogoutCommand : TerminalCommand
    {
        public const String CommandName = "logout";

        public LogoutCommand(Session application) : base(application)
        {
            Name = CommandName;
            Help = "Exit this CLI session";
        }

        public override bool Run(Session session, String rawCommand)
        {
            if (IsValidCommand(rawCommand))
            {
                Console.WriteLine("");
                Environment.Exit(0);
            }
            return false;
        }
    }
}