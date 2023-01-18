using log4net.Config;
using log4net.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace loremipsum
{
     public sealed class GymLogger
    {
        
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public GymLogger() { }
     
        public static ILog GetLog()
        {
            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);
            return logger;
        }
    }
}
