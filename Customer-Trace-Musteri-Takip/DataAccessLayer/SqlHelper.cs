using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    abstract class SqlHelper
    {
        /// <summary>
        /// sql helper ksıımlarımızzzz
        /// </summary>
        /// <returns></returns>
       public static SqlConnection SqlHelpers()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-INVSUP5\SQLEXPRESS; initial Catalog=CustomerTrace;Integrated Security = True;");

            return sqlCon;
        }
    }
}
