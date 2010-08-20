using System;
using System.IO;
using Clish.Logs.Logs;

namespace Clish.Library.Utilities
{
    /// <summary>
    /// Class implements basic access to files (read/write).
    /// </summary>
    public static class FileUtility
    {
        /// <summary>
        /// Method are using for reading files to end.
        /// </summary>
        public static String ReadFile(String filename)
        {
            Log.CoreLog.Info(String.Format("Reading file: {0}", filename));
            using (var reader = new StreamReader(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}