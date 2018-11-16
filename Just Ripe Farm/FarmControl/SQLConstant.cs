using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F.A.R.M
{
    class SQLConstant
    {
        public static string getUserDetails = "SELECT password, privilege_Level FROM dbo.Employee WHERE user_name = @username"; 
    }
}
