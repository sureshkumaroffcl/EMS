using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IssueTrackingSystem
{
    public class Connection
    {
        public static SqlConnection GetConnectionString()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ITS"].ConnectionString;
            return con;
        }
    }
}