using System;
using Clish.Library;

namespace Clish.Commands
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

        public override void Run(String rawCommand)
        {
            Console.WriteLine("");
            Environment.Exit(0);
        }
    }
}