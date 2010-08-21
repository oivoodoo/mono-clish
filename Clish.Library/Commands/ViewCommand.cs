using System;
using System.Linq;
using Clish.Library.Models;
using Action = Clish.Library.Models.Action;

namespace Clish.Library.Commands
{
    /// <summary>
    /// Class imlements default command view for changing scope of commands.
    /// </summary>
    public class ViewCommand : TerminalCommand
    {
        public const String CommandName = "view";
        
        public ViewCommand()
        {
        }

        public ViewCommand(Session session) : base(session)
        {
            Name = CommandName;
            Params = new Param[1];
            var ptype = new PType {Help = "View name type", Method = MethodType.Regexp, Name = "view_name_string"};
            Configuration.PTypes.Add(ptype.Name, ptype);
            var param = new Param {Help = "View name for getting scope of commands.", Name = "viewname", PType = "view_name_string"};
            Params[0] = param;
            Action = new Action {Text = "view ${viewname}"};
        }

        public override bool Run(String command)
        {
            if (IsValidCommand(command))
            {
                return Session.UpdateSession(ParsedParams.First());
            }
            return false;
        }
    }
}