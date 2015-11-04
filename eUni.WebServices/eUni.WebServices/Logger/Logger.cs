using System;
using log4net;
using log4net.Config;

namespace eUni.WebServices.Logger
{
    public class Logger
    {
        private static Logger _instance;
        private readonly ILog _log;

        Logger()
        {
            _log = LogManager.GetLogger(typeof (Logger));
            XmlConfigurator.Configure();
        }

        public static Logger Instance
        {
            get { return _instance ?? (_instance = new Logger()); }
        }

        public void LogError(Exception ex)
        {
            _log.Error(ex);
        }

        public void LogAction(string actionInfo)
        {
            _log.Info(actionInfo);
        }
    }
}