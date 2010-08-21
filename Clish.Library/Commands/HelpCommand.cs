using System;

namespace Clish.Library.Commands
{
    public class HelpCommand : TerminalCommand
    {
        public const String CommandName = "help";

        public HelpCommand()
        {
        }

        public HelpCommand(Session session)
            : base(session)
        {
            Name = CommandName;
        }

        public override bool Run(Session session, String command)
        {
            if (IsValidCommand(command))
            {
                Session.Show(Constants.HelpTemplate);
                base.Run(Session, command);
                return true;
            }
            return false;
        }
    }
}