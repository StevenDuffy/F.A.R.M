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
        private readonly string activityLogFile = "ActivityLog.txt";

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



        public void RemoveCropStockFailed(short amount)
        {
            using (activityRecorder = new StreamWriter(activityLogFile, true))
            {
                activityRecorder.WriteLine("Attempt to remove {0}kgs from crop stock failed: {1}", amount, DateTime.Now);
            }
        }
        public void RemoveCropStockSucceeded(short amount)
        {
            using (activityRecorder = new StreamWriter(activityLogFile, true))
            {
                activityRecorder.WriteLine("Attempt to remove {0}kgs from crop stock succeeded: {1}", amount, DateTime.Now);
            }
        }

        public void AddCropStockFailed(short amount)
        {
            using (activityRecorder = new StreamWriter(activityLogFile, true))
            {
                activityRecorder.WriteLine("Attempt to add {0}kgs to crop stock failed: {1}", amount, DateTime.Now);
            }
        }

        public void AddCropStockSucceeded(short amount)
        {
            using (activityRecorder = new StreamWriter(activityLogFile, true))
            {
                activityRecorder.WriteLine("Attempt to add {0}kgs to crop stock succeeded: {1}", amount, DateTime.Now);
            }
        }



        public void RecordLoginAttempt()
        {
            using (activityRecorder = new StreamWriter(activityLogFile, true))
            {
                activityRecorder.WriteLine("Login Attempted: {0}", DateTime.Now);
            }
        }
       






    }
}

