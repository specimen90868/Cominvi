using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonavit.Core
{
    public class InfonavitHelper : Data.Obj.DataObj
    {
        public List<Infonavit> obtenerInfonavits(Infonavit e)
        {
            List<Infonavit> lstInfonavit = new List<Infonavit>();
            DataTable dtInfonavit = new DataTable();
            Command.CommandText = "select * from infonavit where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            dtInfonavit = SelectData(Command);
            for (int i = 0; i < dtInfonavit.Rows.Count; i++)
            {
                Infonavit inf = new Infonavit();
                inf.idinfonavit = int.Parse(dtInfonavit.Rows[i]["idinfonavit"].ToString());
                inf.idtrabajador = int.Parse(dtInfonavit.Rows[i]["idtrabajador"].ToString());
                inf.idempresa = int.Parse(dtInfonavit.Rows[i]["idempresa"].ToString());
                inf.credito = dtInfonavit.Rows[i]["credito"].ToString();
                inf.descuento = int.Parse(dtInfonavit.Rows[i]["descuento"].ToString());
                inf.valordescuento = double.Parse(dtInfonavit.Rows[i]["valordescuento"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public List<Infonavit> obtenerInfonavit(Infonavit e)
        {
            List<Infonavit> lstInfonavit = new List<Infonavit>();
            DataTable dtInfonavit = new DataTable();
            Command.CommandText = "select * from infonavit where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            dtInfonavit = SelectData(Command);
            for (int i = 0; i < dtInfonavit.Rows.Count; i++)
            {
                Infonavit inf = new Infonavit();
                inf.idinfonavit = int.Parse(dtInfonavit.Rows[i]["idinfonavit"].ToString());
                inf.idtrabajador = int.Parse(dtInfonavit.Rows[i]["idtrabajador"].ToString());
                inf.idempresa = int.Parse(dtInfonavit.Rows[i]["idempresa"].ToString());
                inf.credito = dtInfonavit.Rows[i]["credito"].ToString();
                inf.descuento = int.Parse(dtInfonavit.Rows[i]["descuento"].ToString());
                inf.valordescuento = double.Parse(dtInfonavit.Rows[i]["valordescuento"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public object existeInfonavit(Infonavit e)
        {
            Command.CommandText = "select count(idtrabajador) from infonavit where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public int insertaInfonavit(Infonavit i)
        {
            Command.CommandText = "insert into infonavit (idempresa,idtrabajador,credito,descuento,valordescuento) " +
                "values (@idempresa,@idtrabajador,@credito,@descuento,@valordescuento)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", i.idempresa);
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("credito", i.credito);
            Command.Parameters.AddWithValue("descuento", i.descuento);
            Command.Parameters.AddWithValue("valordescuento", i.valordescuento);
            return Command.ExecuteNonQuery();
        }

        public int actualizaInfonavit(Infonavit i)
        {
            Command.CommandText = "update infonavit set credito = @credito, descuento = @descuento, valordescuento = @valordescuento where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("credito", i.credito);
            Command.Parameters.AddWithValue("descuento", i.descuento);
            Command.Parameters.AddWithValue("valordescuento", i.valordescuento);
            return Command.ExecuteNonQuery();
        }

    }
}
