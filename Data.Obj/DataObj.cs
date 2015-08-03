using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Data.Obj
{
    public class DataObj
    {
        public MySqlCommand Command { get; set; }

        public DataTable SelectData(MySqlCommand pCommand)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = pCommand;
            da.Fill(dt);
            return dt;
        }

        public object Select(MySqlCommand pCommand)
        {
            return pCommand.ExecuteScalar();
        }
    }
}
