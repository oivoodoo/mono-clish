namespace Clish.Commands
{
    public class TopCommand : TerminalCommand
    {
        public const string CommandName = "top";

        public TopCommand(Application application) : base(application)
        {
            Name = CommandName;
        }
    }
}