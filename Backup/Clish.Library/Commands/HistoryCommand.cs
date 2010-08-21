using System;

namespace Clish.Library.Commands
{
    public class HistoryCommand : TerminalCommand
    {
        public const String CommandName = "history";

        public HistoryCommand()
        {
        }

        public HistoryCommand(Session session)
            : base(session)
        {
            Name = CommandName;
        }

        public override bool Run(Session session, String command)
        {
            var values = command.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            String[] histories = new string[0];
            if (Session.LineEditor.history != null)
            {
                if (values.Length > 0)
                {
                    histories = Session.LineEditor.history.GetWithLimit(Convert.ToInt32(values[0]));
                }
                else
                {
                    histories = Session.LineEditor.history.GetWithLimit(Session.LineEditor.history.count);
                }
            }
            foreach (string history in histories)
            {
                Console.WriteLine(history);
            }
            // if we need to translate view to another view.
            base.Run(Session, command);
            return true;
        }
    }
}