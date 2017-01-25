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
                inf.valordescuento = decimal.Parse(dtInfonavit.Rows[i]["valordescuento"].ToString());
                inf.activo = bool.Parse(dtInfonavit.Rows[i]["activo"].ToString());
                inf.estatus = int.Parse(dtInfonavit.Rows[i]["estatus"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public List<Infonavit> obtenerInfonavit(Infonavit e)
        {
            List<Infonavit> lstInfonavit = new List<Infonavit>();
            DataTable dtInfonavit = new DataTable();
            Command.CommandText = "select * from infonavit where idtrabajador = @idtrabajador and activo = @activo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("activo", e.activo);
            dtInfonavit = SelectData(Command);
            for (int i = 0; i < dtInfonavit.Rows.Count; i++)
            {
                Infonavit inf = new Infonavit();
                inf.idinfonavit = int.Parse(dtInfonavit.Rows[i]["idinfonavit"].ToString());
                inf.idtrabajador = int.Parse(dtInfonavit.Rows[i]["idtrabajador"].ToString());
                inf.idempresa = int.Parse(dtInfonavit.Rows[i]["idempresa"].ToString());
                inf.credito = dtInfonavit.Rows[i]["credito"].ToString();
                inf.descuento = int.Parse(dtInfonavit.Rows[i]["descuento"].ToString());
                inf.valordescuento = decimal.Parse(dtInfonavit.Rows[i]["valordescuento"].ToString());
                inf.activo = bool.Parse(dtInfonavit.Rows[i]["activo"].ToString());
                inf.descripcion = dtInfonavit.Rows[i]["descripcion"].ToString();
                inf.fecha = DateTime.Parse(dtInfonavit.Rows[i]["fecha"].ToString());
                inf.inicio = DateTime.Parse(dtInfonavit.Rows[i]["inicio"].ToString());
                inf.fin = DateTime.Parse(dtInfonavit.Rows[i]["fin"].ToString());
                inf.estatus = int.Parse(dtInfonavit.Rows[i]["estatus"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public List<Infonavit> obtenerInfonavitTrabajador(Infonavit e)
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
                inf.valordescuento = decimal.Parse(dtInfonavit.Rows[i]["valordescuento"].ToString());
                inf.activo = bool.Parse(dtInfonavit.Rows[i]["activo"].ToString());
                inf.descripcion = dtInfonavit.Rows[i]["descripcion"].ToString();
                inf.fecha = DateTime.Parse(dtInfonavit.Rows[i]["fecha"].ToString());
                inf.inicio = DateTime.Parse(dtInfonavit.Rows[i]["inicio"].ToString());
                inf.fin = DateTime.Parse(dtInfonavit.Rows[i]["fin"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public List<Infonavit> obtenerInfonavitTrabajador(int idtrabajador)
        {
            List<Infonavit> lstInfonavit = new List<Infonavit>();
            DataTable dtInfonavit = new DataTable();
            Command.CommandText = "select top 1 * from infonavit where idtrabajador = @idtrabajador order by fecha desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            dtInfonavit = SelectData(Command);
            for (int i = 0; i < dtInfonavit.Rows.Count; i++)
            {
                Infonavit inf = new Infonavit();
                inf.idinfonavit = int.Parse(dtInfonavit.Rows[i]["idinfonavit"].ToString());
                inf.idtrabajador = int.Parse(dtInfonavit.Rows[i]["idtrabajador"].ToString());
                inf.idempresa = int.Parse(dtInfonavit.Rows[i]["idempresa"].ToString());
                inf.credito = dtInfonavit.Rows[i]["credito"].ToString();
                inf.descuento = int.Parse(dtInfonavit.Rows[i]["descuento"].ToString());
                inf.valordescuento = decimal.Parse(dtInfonavit.Rows[i]["valordescuento"].ToString());
                inf.activo = bool.Parse(dtInfonavit.Rows[i]["activo"].ToString());
                inf.descripcion = dtInfonavit.Rows[i]["descripcion"].ToString();
                inf.fecha = DateTime.Parse(dtInfonavit.Rows[i]["fecha"].ToString());
                inf.inicio = DateTime.Parse(dtInfonavit.Rows[i]["inicio"].ToString());
                inf.fin = DateTime.Parse(dtInfonavit.Rows[i]["fin"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public List<Infonavit> obtenerInfonavitCredito(Infonavit e)
        {
            List<Infonavit> lstInfonavit = new List<Infonavit>();
            DataTable dtInfonavit = new DataTable();
            Command.CommandText = "select * from infonavit where idtrabajador = @idtrabajador and credito = @credito";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("credito", e.credito);
            dtInfonavit = SelectData(Command);
            for (int i = 0; i < dtInfonavit.Rows.Count; i++)
            {
                Infonavit inf = new Infonavit();
                inf.idinfonavit = int.Parse(dtInfonavit.Rows[i]["idinfonavit"].ToString());
                inf.idtrabajador = int.Parse(dtInfonavit.Rows[i]["idtrabajador"].ToString());
                inf.idempresa = int.Parse(dtInfonavit.Rows[i]["idempresa"].ToString());
                inf.credito = dtInfonavit.Rows[i]["credito"].ToString();
                inf.descuento = int.Parse(dtInfonavit.Rows[i]["descuento"].ToString());
                inf.valordescuento = decimal.Parse(dtInfonavit.Rows[i]["valordescuento"].ToString());
                inf.activo = bool.Parse(dtInfonavit.Rows[i]["activo"].ToString());
                inf.descripcion = dtInfonavit.Rows[i]["descripcion"].ToString();
                inf.fecha = DateTime.Parse(dtInfonavit.Rows[i]["fecha"].ToString());
                inf.inicio = DateTime.Parse(dtInfonavit.Rows[i]["inicio"].ToString());
                inf.fin = DateTime.Parse(dtInfonavit.Rows[i]["fin"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public List<Infonavit> obtenerDiasInfonavit(Infonavit e)
        {
            List<Infonavit> lstInfonavit = new List<Infonavit>();
            DataTable dtInfonavit = new DataTable();
            Command.CommandText = "select top 2 dias, fecha, descuento from Infonavit where idtrabajador = @idtrabajador order by fecha desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            dtInfonavit = SelectData(Command);
            for (int i = 0; i < dtInfonavit.Rows.Count; i++)
            {
                Infonavit inf = new Infonavit();
                inf.dias = int.Parse(dtInfonavit.Rows[i]["dias"].ToString());
                inf.fecha = DateTime.Parse(dtInfonavit.Rows[i]["fecha"].ToString());
                inf.descuento = int.Parse(dtInfonavit.Rows[i]["descuento"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public object obtenerValorInfonavit(Infonavit e)
        {
            Command.CommandText = "select valordescuento from infonavit where idtrabajador = @idtrabajador and activo = 1";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerValorInfonavit(int idTrabajador)
        {
            Command.CommandText = "select top 1 valordescuento from infonavit where idtrabajador = @idtrabajador and activo = 0 order by fecha desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerIdInfonavit(Infonavit e)
        {
            Command.CommandText = "select idinfonavit from infonavit where idtrabajador = @idtrabajador and activo = 1";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object existeInfonavit(Infonavit e)
        {
            Command.CommandText = "select count(idtrabajador) from infonavit where idtrabajador = @idtrabajador and activo = 1";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object existeInfonavit(int idtrabajador)
        {
            Command.CommandText = "select count(idtrabajador) from infonavit where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object existeInfonavit(int idtrabajador, string credito)
        {
            Command.CommandText = "select count(idtrabajador) from infonavit where idtrabajador = @idtrabajador and credito = @credito";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("credito", credito);
            object dato = Select(Command);
            return dato;
        }

        public object activoInfonavit(Infonavit e)
        {
            Command.CommandText = "select top 1 activo from infonavit where idtrabajador = @idtrabajador and idempresa = @idempresa order by fecha desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            object dato = Select(Command);
            return dato;
        }

        public int insertaInfonavit(Infonavit i)
        {
            Command.CommandText = "insert into infonavit (idempresa,idtrabajador,credito,descuento,valordescuento, activo, descripcion, dias, fecha, inicio, fin, registro, idusuario, estatus) " +
                "values (@idempresa,@idtrabajador,@credito,@descuento,@valordescuento, @activo, @descripcion, @dias, @fecha, @inicio, @fin, @registro, @idusuario, @estatus)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", i.idempresa);
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("credito", i.credito);
            Command.Parameters.AddWithValue("descuento", i.descuento);
            Command.Parameters.AddWithValue("valordescuento", i.valordescuento);
            Command.Parameters.AddWithValue("activo", i.activo);
            Command.Parameters.AddWithValue("descripcion", i.descripcion);
            Command.Parameters.AddWithValue("dias", i.dias);
            Command.Parameters.AddWithValue("fecha", i.fecha);
            Command.Parameters.AddWithValue("inicio", i.inicio);
            Command.Parameters.AddWithValue("fin", i.fin);
            Command.Parameters.AddWithValue("registro", i.registro);
            Command.Parameters.AddWithValue("idusuario", i.idusuario);
            Command.Parameters.AddWithValue("estatus", i.estatus);
            return Command.ExecuteNonQuery();
        }

        public int actualizaInfonavit(Infonavit i)
        {
            Command.CommandText = "update infonavit set credito = @credito, descuento = @descuento, valordescuento = @valordescuento, activo = @activo, descripcion = @descripcion, " + 
                "dias = @dias, fecha = @fecha, inicio = @inicio, fin = @fin, registro = @registro, idusuario = @idusuario, estatus = @estatus where idinfonavit = @idinfonavit";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idinfonavit", i.idinfonavit);
            Command.Parameters.AddWithValue("credito", i.credito);
            Command.Parameters.AddWithValue("descuento", i.descuento);
            Command.Parameters.AddWithValue("valordescuento", i.valordescuento);
            Command.Parameters.AddWithValue("activo", i.activo);
            Command.Parameters.AddWithValue("descripcion", i.descripcion);
            Command.Parameters.AddWithValue("dias", i.dias);
            Command.Parameters.AddWithValue("fecha", i.fecha);
            Command.Parameters.AddWithValue("inicio", i.inicio);
            Command.Parameters.AddWithValue("fin", i.fin);
            Command.Parameters.AddWithValue("registro", i.registro);
            Command.Parameters.AddWithValue("idusuario", i.idusuario);
            Command.Parameters.AddWithValue("estatus", i.estatus);
            return Command.ExecuteNonQuery();
        }

        public int actualizaEstatusInfonavit(int id, DateTime registro, int idUsuario)
        {
            Command.CommandText = "update infonavit set activo = 0, estatus = 129, registro = @registro, idusuario = @idusuario where idinfonavit = @idinfonavit";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idinfonavit", id);
            Command.Parameters.AddWithValue("registro", registro);
            Command.Parameters.AddWithValue("idusuario", idUsuario);
            return Command.ExecuteNonQuery();
        }

        public int actualizaEstatusInfonavit(int id, int idtrabajador)
        {
            Command.CommandText = "update infonavit set activo = 1 where idinfonavit = @idinfonavit and idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idinfonavit", id);
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            return Command.ExecuteNonQuery();
        }

        public List<suaInfonavit> obtenerInfonavit(suaInfonavit e)
        {
            List<suaInfonavit> lstInfonavit = new List<suaInfonavit>();
            DataTable dtInfonavit = new DataTable();
            Command.CommandText = "select * from suaInfonavit where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            dtInfonavit = SelectData(Command);
            for (int i = 0; i < dtInfonavit.Rows.Count; i++)
            {
                suaInfonavit inf = new suaInfonavit();
                inf.id = int.Parse(dtInfonavit.Rows[i]["id"].ToString());
                inf.idtrabajador = int.Parse(dtInfonavit.Rows[i]["idtrabajador"].ToString());
                inf.idempresa = int.Parse(dtInfonavit.Rows[i]["idempresa"].ToString());
                inf.registropatronal = dtInfonavit.Rows[i]["registropatronal"].ToString();
                inf.nss = dtInfonavit.Rows[i]["nss"].ToString();
                inf.credito = dtInfonavit.Rows[i]["credito"].ToString();
                inf.modificacion = int.Parse(dtInfonavit.Rows[i]["modificacion"].ToString());
                inf.fecha = DateTime.Parse(dtInfonavit.Rows[i]["fecha"].ToString());
                inf.descuento = int.Parse(dtInfonavit.Rows[i]["descuento"].ToString());
                inf.valor = decimal.Parse(dtInfonavit.Rows[i]["valor"].ToString());
                lstInfonavit.Add(inf);
            }
            return lstInfonavit;
        }

        public object obtenerIdSuaInfonavit(suaInfonavit i)
        {
            Command.CommandText = "select id from suaInfonavit where idtrabajador = @idtrabajador and fecha = @fecha and nss = @nss";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("fecha", i.fecha);
            Command.Parameters.AddWithValue("nss", i.nss);
            object dato = Select(Command);
            return dato;
        }

        public int insertarInfonavitSua(suaInfonavit i)
        {
            Command.CommandText = "insert into suaInfonavit (idtrabajador, idempresa, registropatronal, nss, credito, modificacion, fecha, descuento, valor) " +
                "values (@idtrabajador, @idempresa, @registropatronal, @nss, @credito, @modificacion, @fecha, @descuento, @valor)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", i.idempresa);
            Command.Parameters.AddWithValue("registropatronal", i.registropatronal);
            Command.Parameters.AddWithValue("nss", i.nss);
            Command.Parameters.AddWithValue("credito", i.credito);
            Command.Parameters.AddWithValue("modificacion", i.modificacion);
            Command.Parameters.AddWithValue("fecha", i.fecha);
            Command.Parameters.AddWithValue("descuento", i.descuento);
            Command.Parameters.AddWithValue("valor", i.valor);
            return Command.ExecuteNonQuery();
        }

        public int actualizaInfonavitSua(suaInfonavit i)
        {
            Command.CommandText = "update suaInfonavit credito = @credito, modificacion = @modificacion, fecha = @fecha, descuento = @descuento, valor= @valor where " +
                "idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", i.idempresa);
            Command.Parameters.AddWithValue("registropatronal", i.registropatronal);
            Command.Parameters.AddWithValue("nss", i.nss);
            Command.Parameters.AddWithValue("credito", i.credito);
            Command.Parameters.AddWithValue("modificacion", i.modificacion);
            Command.Parameters.AddWithValue("fecha", i.fecha);
            Command.Parameters.AddWithValue("descuento", i.descuento);
            Command.Parameters.AddWithValue("valor", i.valor);
            return Command.ExecuteNonQuery();
        }

    }
}
