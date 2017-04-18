using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorDescuento.Core
{
    public class FactorDescuentoHelper : Data.Obj.DataObj
    {
        public List<FactorDescuento> obtenerFactores()
        {
            DataTable dt = new DataTable();
            List<FactorDescuento> lstFactores = new List<FactorDescuento>();
            Command.CommandText = "select * from FactorDescuento";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FactorDescuento fd = new FactorDescuento();
                fd.idfactor = int.Parse(dt.Rows[i]["idfactor"].ToString());
                fd.factor = decimal.Parse(dt.Rows[i]["factor"].ToString());
                fd.fecha = DateTime.Parse(dt.Rows[i]["fecha"].ToString());
                lstFactores.Add(fd);
            }
            return lstFactores;
        }

        public List<FactorDescuento> obtenerFactor(FactorDescuento fd) 
        {
            DataTable dt = new DataTable();
            List<FactorDescuento> lstFactores = new List<FactorDescuento>();
            Command.CommandText = "select * from FactorDescuento where idfactor = @idfactor";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idfactor", fd.idfactor);
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FactorDescuento factor = new FactorDescuento();
                factor.idfactor = int.Parse(dt.Rows[i]["idfactor"].ToString());
                factor.factor = decimal.Parse(dt.Rows[i]["factor"].ToString());
                factor.fecha = DateTime.Parse(dt.Rows[i]["fecha"].ToString());
                lstFactores.Add(factor);
            }
            return lstFactores;
        }

        public int insertaFactor(FactorDescuento fd)
        {
            Command.CommandText = @"insert into factordescuento (factor, fecha) values (@factor,@fecha)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("factor", fd.factor);
            Command.Parameters.AddWithValue("fecha", fd.fecha);
            int dato = Command.ExecuteNonQuery();
            return dato;
        }

        public int actualizaFactor(FactorDescuento fd)
        {
            Command.CommandText = @"update factordescuento set factor = @factor, fecha = @fecha where idfactor = @idfactor";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idfactor", fd.idfactor);
            Command.Parameters.AddWithValue("factor", fd.factor);
            Command.Parameters.AddWithValue("fecha", fd.fecha);
            int dato = Command.ExecuteNonQuery();
            return dato;
        }

        public int borrarFactor(FactorDescuento fd)
        {
            Command.CommandText = @"delete from factordescuento where idfactor = @idfactor";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idfactor", fd.idfactor);
            int dato = Command.ExecuteNonQuery();
            return dato;
        }

        public object obtenerFactorDescuento()
        {
            Command.CommandText = "select top 1 factor from factordescuento order by fecha desc";
            Command.Parameters.Clear();
            object dato = Select(Command);
            return dato;
        }
    }
}
