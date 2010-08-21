using System;
using log4net;

namespace Clish.Logs.Logs
{
    public enum LogLevel
    {
        Debug = 1,
        Error,
        Fatal,
        Info,
        Warn
    }

    /// <summary>
    /// Class implements access to logger object for classes.
    /// </summary>
    public class Logger
    {
        private readonly ILog _log;

        private Logger(ILog ilog)
        {
            _log = ilog;
        }

        /// <summary>
        /// Obtains logger by type.
        /// </summary>
        /// <param name="type">Type which to log.</param>
        /// <returns></returns>
        public static Logger GetLogger(Type type)
        {
            var logger = new Logger(LogManager.GetLogger(type));
            return logger;
        }

        /// <summary>
        /// Logs informational message.
        /// </summary>
        /// <param name="msg">Message to be logged.</param>
        public void Info(string msg)
        {
            _log.Info(msg);
        }

        /// <summary>
        /// Logs debug message.
        /// </summary>
        /// <param name="msg">Message to be logged.</param>
        public void Debug(string msg)
        {
            _log.Debug(msg);
        }

        /// <summary>
        /// Logs debug message.
        /// </summary>
        /// <param name="msg">Message to be logged.</param>
        /// <param name="ex">Exception to be logged.</param>
        public void Debug(string msg, Exception ex)
        {
            _log.Debug(msg, ex);
        }

        /// <summary>
        /// Logs error message.
        /// </summary>
        /// <param name="ex">Exception to be logged.</param>
        public void Error(Exception ex)
        {
            _log.Error(ex);
        }

        /// <summary>
        /// Logs error message.
        /// </summary>
        /// <param name="msg">Message to be logged.</param>
        /// <param name="ex">Exception to be logged.</param>
        public void Error(string msg, Exception ex)
        {
            _log.Error(msg, ex);
        }

        /// <summary>
        /// Logs warning message.
        /// </summary>
        /// <param name="msg">Message to be logged.</param>
        public void Warning(string msg)
        {
            _log.Warn(msg);
        }

        /// <summary>
        /// Logs warning message.
        /// </summary>
        /// <param name="msg">Message to be logged.</param>
        /// <param name="ex">Exception to be logged.</param>
        public void Warning(string msg, Exception ex)
        {
            _log.Warn(msg, ex);
        }

        public void Message(LogLevel level, String format, params object[] values)
        {
            String message = String.Format(format, values);
            switch (level)
            {
                case LogLevel.Debug:
                    _log.Debug(message);
                    break;
                case LogLevel.Error:
                    _log.Error(message);
                    break;
                case LogLevel.Fatal:
                    _log.Fatal(message);
                    break;
                case LogLevel.Info:
                    _log.Info(message);
                    break;
                case LogLevel.Warn:
                    _log.Warn(message);
                    break;
            }
        }
    }
}