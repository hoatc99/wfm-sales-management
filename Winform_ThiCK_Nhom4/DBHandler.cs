using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Winform_ThiCK_Nhom4
{
    class DBHandler
    {
        private static DBHandler instance;
        
        public static DBHandler Instance
        {
            get { if (instance == null) instance = new DBHandler(); return instance; }
            private set { instance = value; }
        }

        private DBHandler() { }

        private SqlDataAdapter adapter;

        private DataSet ds;

        private string strConnection = "Data Source=.\\SQLEXPRESS; Database=WF_QuanLyCuaHang; Integrated Security=True";
        
        public DataTable Read(string query, string tableName = null, List<SqlParameter> paramList = null)
        {
            DataTable dt = new DataTable(tableName);

            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                conn.Open();

                adapter = new SqlDataAdapter(query, conn);

                if (paramList != null)
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddRange(paramList.ToArray<SqlParameter>());
                }

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Fill(dt);

                conn.Close();

                return dt;
            }
        }

        public int Update(DataSet ds, string tableName)
        {
            return adapter.Update(ds.Tables[tableName]);
        }
    }
}
