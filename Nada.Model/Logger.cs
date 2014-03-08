using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace Nada.Model
{
    #region Class Declaration
    /// <summary>
    /// Logging class wrapping Log4Net.
    /// </summary>
    public class Logger
    {
        #region Private Members
        private ILog logger = LogManager.GetLogger(typeof(Logger));
        private string caller = AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "").Replace(".vshost", "");
        #endregion

        #region Constructor
        static Logger()
        {
            XmlConfigurator.Configure();
        }
        #endregion

        public string Caller
        {
            get { return caller; }
            set { caller = value; }
        }

        #region Public Methods
        public void Debug(String log, Exception ex = null)
        {
            ThreadContext.Properties["Type"] = caller;
            logger.Debug(log, ex);
        }
        public void Error(String log, Exception ex = null)
        {
            ThreadContext.Properties["Type"] = caller;
            logger.Error(log, ex);
        }
        public void Info(String log, Exception ex = null)
        {
            ThreadContext.Properties["Type"] = caller;
            logger.Info(log, ex);
        }
        #endregion
    }
    #endregion
}
