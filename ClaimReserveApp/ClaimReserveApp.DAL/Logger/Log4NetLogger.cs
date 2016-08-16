using log4net;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserveApp.DAL.Logger
{
    public class Log4NetLogger : ILoggerFacade
    {
        private readonly ILog m_Logger = LogManager.GetLogger(typeof(Log4NetLogger));

        public Log4NetLogger()
        {

        }

        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    m_Logger.Debug(message);
                    break;
                case Category.Warn:
                    m_Logger.Warn(message);
                    break;
                case Category.Exception:
                    m_Logger.Error(message);
                    break;
                case Category.Info:
                    m_Logger.Info(message);
                    break;
            }
        }
    }
}

