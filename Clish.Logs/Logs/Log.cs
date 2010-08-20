using Clish.Logs.Logs;

[assembly: ResourceFileConfigurator("Clish.Logs.Logs.log4net.xml.config")]

namespace Clish.Logs.Logs
{
    /// <summary>
    /// 	Class impelements basic access to logger object for logging 
    ///	to console or file.
    /// </summary>
    public class Log
    {
        private static readonly object Root = new object();

        private static Logger _coreLog;

        public static Logger CoreLog
        {
            get
            {
                if (_coreLog == null)
                {
                    lock (Root)
                    {
                        if (_coreLog == null)
                        {
                            _coreLog = Logger.GetLogger(typeof (Log));
                        }
                    }
                }
                return _coreLog;
            }
        }
    }
}