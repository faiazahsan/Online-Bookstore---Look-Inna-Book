using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace InnaBookStore
{
    class db
    {
        public static SqlConnection connection = new SqlConnection("workstation id=newdbtest.mssql.somee.com;packet size=4096;user id=RGEDUPORTAL_SQLLogin_1;pwd=xfry15hty3;data source=newdbtest.mssql.somee.com;persist security info=False;initial catalog=newdbtest");

    }
}
