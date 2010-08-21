namespace Clish.Library.Commands
{
    public class TopCommand : TerminalCommand
    {
        public const string CommandName = "top";

        public TopCommand(Session application) : base(application)
        {
            Name = CommandName;
        }

        public override bool Run(Session session, string rawCommand)
        {
            return base.Run(Session, rawCommand);
        }
    }
}