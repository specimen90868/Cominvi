using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data.Obj
{
    public class DataObj
    {
        public SqlCommand Command { get; set; }
        public SqlBulkCopy bulkCommand { get; set; }

        public DataTable SelectData(SqlCommand pCommand)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = pCommand;
            da.Fill(dt);
            return dt;
        }

        public object Select(SqlCommand pCommand)
        {
            return pCommand.ExecuteScalar();
        }
    }
}
