using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Clish.Library.Models;
using Clish.Logs.Logs;

namespace Clish.Library
{
    /// <summary>
    /// Class implements functionallity for replacement 
    /// macro statements
    /// </summary>
    public class CommandBuilder
    {
        private const String ArgumentPattern = @"\$\{\w+\}";
        private const String IntegerPattern = @"[+,-]{0,1}(\d+)";
        private readonly List<String> m_parsedParams = new List<String>();
        private const String StringPattern = @"([-\w]+)";

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBuilder"/> class.
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        /// <param name="command">The command.</param>
        /// <param name="ptypes">The ptypes.</param>
        public CommandBuilder(String rawCommand, Command command, Dictionary<String, PType> ptypes, Session session)
        {
            RawCommand = rawCommand;
            Command = command;
            PTypes = ptypes;
            Session = session;
        }

        public Session Session { get; set; }

        /// <summary>
        /// Gets or sets the raw command.
        /// </summary>
        /// <value>The raw command.</value>
        private String RawCommand { get; set; }
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        private Command Command { get; set; }
        /// <summary>
        /// Gets or sets the P types.
        /// </summary>
        /// <value>The P types.</value>
        private Dictionary<String, PType> PTypes { get; set; }

        /// <summary>
        /// Gets or sets the parse params.
        /// </summary>
        /// <value>The parse params.</value>
        public List<String> ParsedParams
        {
            get { return m_parsedParams;  }
        }

        /// <summary>
        /// Build command by the arguments from command line.
        /// </summary>
        /// <returns></returns>
        public String BuildCommand()
        {
            String result = Command.Action.Text;
            MatchCollection collection = Regex.Matches(Command.Action.Text, ArgumentPattern);
            try
            {
                String rawCommand = RawCommand;
                // if we have additional params from view id property for action text.
                if ((Command.Params == null || collection.Count != Command.Params.Length) &&
                    !String.IsNullOrEmpty(Session.ViewId))
                {
                    result = Session.ApplyViewParams(result);
                    collection = Regex.Matches(result, ArgumentPattern);
                }
                if (Command.Params != null)
                {
                    for (int i = 0; i < collection.Count; i++)
                    {
                        String token = collection[i].Value;
                        // INFO: We need to check it because 
                        // in this place we can have problems connecting with command arguments.
                        PType type = PTypes[Command.Params[i].PType];
                        // In this method we will resolve param and clear in the typing command.
                        String param = ResolveParam(rawCommand, type);
                        // Remove from raw command param, because in another interation,
                        // we can have same param pattern for searching.
                        rawCommand = rawCommand.Replace(param, "");
                        // Replace macro statement with param value, typed by user in terminal.
                        result = result.Replace(token, param);
                        ParsedParams.Add(param);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.CoreLog.Error(ex);
                throw new InvalidDataException("You have invalid command arguments, please try to type another params.");
            }
        }

        /// <summary>
        /// Resolves the param.
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static String ResolveParam(String rawCommand, PType type)
        {
            String param = String.Empty;
            switch (type.Method)
            {
                case MethodType.Integer:
                    param = GetParamByPattern(rawCommand, IntegerPattern);
                    param = !type.Numbers.Contains(param) ? String.Empty : Convert.ToInt32(param).ToString();
                    break;
                case MethodType.Regexp:
                    // INFO: We need to check this method for errors.
                    if (!String.IsNullOrEmpty(type.Pattern))
                    {
                        param = GetParamByPattern(rawCommand, type.Pattern);
                    }
                    else
                    {
                        param = GetParamByPattern(rawCommand, StringPattern);
                    }
                    break;
                    break;
                case MethodType.Select:
                    // INFO: what we need to write there for complete. !!!!!!!!!!!!
                    param = type.Selections.Where(rawCommand.Contains).FirstOrDefault();
                    break;
            }
            return param;
        }

        /// <summary>
        /// Gets the param by pattern.
        /// </summary>
        /// <param name="rawCommand">The raw command.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        private static String GetParamByPattern(String rawCommand, String pattern)
        {
            MatchCollection c = Regex.Matches(rawCommand, pattern);
            return c.Count > 0 ? c[0].Value : String.Empty;
        }
    }
}