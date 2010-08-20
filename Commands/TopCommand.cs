using Clish.Library;

namespace Clish.Commands
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