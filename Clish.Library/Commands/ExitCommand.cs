using System;

namespace Clish.Library.Commands
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

        public override bool Run(Session session, String rawCommand)
        {
            if(IsValidCommand(rawCommand))
            {
                Session.UpdateSession(Configuration.DefaultViewName);
                base.Run(Session, rawCommand);
                return true;
            }
            return false;
        }
    }
}