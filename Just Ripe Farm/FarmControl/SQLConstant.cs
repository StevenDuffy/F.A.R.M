using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmControl
{
    class SQLConstant
    {
        public static string getUserDetails = "SELECT password, privilege_Level FROM dbo.Employee WHERE user_name = @username";
        public static string getFutureHarvests = "SELECT * FROM Field WHERE harvest_date = '2018-11-20'";
    }
}
