using System;
using System.Collections.Generic;

namespace Clish.Library
{
    /// <summary>
    /// Class contains default tokens for clish terminal.
    /// </summary>
    public static class DefinedVariables
    {
        private static Dictionary<String, String> m_variables = new Dictionary<string, string>();

        public static Dictionary<String, String> Variables
        {
            get { return m_variables; }
        }

        static DefinedVariables()
        {
            Variables.Add("${SYSTEM_NAME}", Environment.MachineName);
        }
    }
}