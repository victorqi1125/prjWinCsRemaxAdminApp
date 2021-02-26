using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace prjWinCsRemaxAdminApp.Datasource
{
    public static class clsGlobal
    {
        //Declaring global variables accessible around the whole project
        public static DataSet mySet;
        public static SqlConnection myCon;
        public static SqlDataAdapter adpHouse, adpEmployee,adpClient;
    }
}
