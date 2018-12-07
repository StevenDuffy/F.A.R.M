using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FarmControl
{
    public class ActivityLogger
    {
        private StreamWriter activityRecorder;
        
            private static ActivityLogger logger;

        public static ActivityLogger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new ActivityLogger();
                }

                return logger;
            }   
        }
    }
}

