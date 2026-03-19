using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylRecordsApplication_2.Classes
{
    public class DBConnection
    {
        public static DataTable Connection(string SQL)
        {
            DataTable dataTable = new DataTable("Datatable");
            SqlConnection conn = new SqlConnection("server=localhost;Trusted_Connection=No;DataBase=***;User=root;PWD=");
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = SQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
