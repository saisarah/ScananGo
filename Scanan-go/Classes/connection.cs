using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Scanan_go.Classes
{
    public class connection
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-AFRII5QM;Initial Catalog=ScananGo;Integrated Security=True");

        public SqlConnection Activecon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
    }
}
