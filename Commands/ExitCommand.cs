using System;
using System.Collections.Generic;
using Clish.Library.Models;

namespace Clish.Commands 
{

    /// <summary>
    /// Close session of the current view.
    /// </summary>
    public class ExitCommand : TerminalCommand 
    {
        public const String CommandName = "exit";

        public ExitCommand(Application application) : base(application)
        {
            Name = CommandName;
            Help = "Exit this CLI session";
        }

        public override void Run(String rawCommand,  Dictionary<String, PType> types)
        {
        }
    }
}
