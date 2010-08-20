using System;
using System.Collections.Generic;
using Clish.Commands;
using Clish.Library;
using Clish.Library.Models;

namespace Clish.Terminal
{
    /// <summary>
    /// Clish native exit command.
    /// </summary>
    public class LogoutCommand : TerminalCommand
    {
        public const String CommandName = "logout";

        public LogoutCommand(Application application) : base(application)
        {
            Name = CommandName;
            Help = "Exit this CLI session";
        }

        public override void Run(String rawCommand,  Dictionary<String, PType> types)
        {
            Console.WriteLine("");
            Environment.Exit(0);
        }
    }
}