using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bajas.Core
{
    public class BajasHelper : Data.Obj.DataObj
    {
        public List<Bajas> obtenerBajas(Bajas a)
        {
            List<Bajas> lstBaja = new List<Bajas>();
            DataTable dtBajas = new DataTable();
            Command.CommandText = "select * from suaBajas where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa",a.idempresa);
            dtBajas = SelectData(Command);
            for (int i = 0; i < dtBajas.Rows.Count; i++)
            {
                Bajas baja = new Bajas();
                baja.id = int.Parse(dtBajas.Rows[i]["id"].ToString());
                baja.idtrabajador = int.Parse(dtBajas.Rows[i]["idtrabajador"].ToString());
                baja.idempresa = int.Parse(dtBajas.Rows[i]["idempresa"].ToString());
                baja.registropatronal = dtBajas.Rows[i]["registropatronal"].ToString();
                baja.nss = dtBajas.Rows[i]["nss"].ToString();
                baja.motivo = int.Parse(dtBajas.Rows[i]["motivo"].ToString());
                baja.fecha = DateTime.Parse(dtBajas.Rows[i]["fecha"].ToString());
                lstBaja.Add(baja);
            }
            return lstBaja;
        }

        public int insertaBaja(Bajas a)
        {
            Command.CommandText = "insert into suaBajas (idtrabajador, idempresa, registropatronal, nss, motivo, fecha) " +
                "values (@idtrabajador, @idempresa, @registropatronal, @nss, @motivo, @fecha)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",a.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", a.idempresa);
            Command.Parameters.AddWithValue("registropatronal", a.registropatronal);
            Command.Parameters.AddWithValue("nss", a.nss);
            Command.Parameters.AddWithValue("motivo", a.motivo);
            Command.Parameters.AddWithValue("fecha", a.fecha);
            return Command.ExecuteNonQuery();
        }
    }
}
