namespace TalkingKeyboard.Shell.Logging
{
    using NLog;

    using Prism.Logging;

    public class NLogLogger : ILoggerFacade
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        /// <param name="priority">The priority of the entry (not used by NLog so we pass Priority.None)</param>
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    this.logger.Debug(message);
                    break;
                case Category.Exception:
                    this.logger.Error(message);
                    break;
                case Category.Info:
                    this.logger.Info(message);
                    break;
                case Category.Warn:
                    this.logger.Warn(message);
                    break;
                default:
                    this.logger.Info(message);
                    break;
            }
        }
    }
}