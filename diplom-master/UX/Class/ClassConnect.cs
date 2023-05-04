using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MityaginaNP.UX.Class
{
    static class ClassConnect
    {
        public static string GetSQLConnString()
        {
            string sSqlServer = "MAIBOOK543\\SQLEXPRESS";
            string sDatabase = "MityaginaNP";

            string sConnection = string.Format(CultureInfo.InvariantCulture,
             "Data Source={0};Initial Catalog={1};Integrated Security=SSPI",
             sSqlServer, sDatabase);

            return sConnection;
        }
    }
}
