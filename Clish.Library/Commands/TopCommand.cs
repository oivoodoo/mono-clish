namespace Clish.Library.Commands
{
    public class TopCommand : TerminalCommand
    {
        public const string CommandName = "top";

        public TopCommand(Session application) : base(application)
        {
            Name = CommandName;
        }
    }
}