using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altas.Core
{
    public class AltasHelper : Data.Obj.DataObj
    {
        public List<Altas> obtenerAltas(Altas e)
        {
            DataTable dtAltas = new DataTable();
            List<Altas> lstAltas = new List<Altas>();
            Command.CommandText = "select * from suaAltas where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            dtAltas = SelectData(Command);

            for (int i = 0; i < dtAltas.Rows.Count; i++)
            {
                Altas alta = new Altas();
                alta.id = int.Parse(dtAltas.Rows[i]["id"].ToString());
                alta.idtrabajador = int.Parse(dtAltas.Rows[i]["idtrabajador"].ToString());
                alta.idempresa = int.Parse(dtAltas.Rows[i]["idempresa"].ToString());
                alta.registropatronal = dtAltas.Rows[i]["registropatronal"].ToString();
                alta.nss = dtAltas.Rows[i]["nss"].ToString();
                alta.rfc = dtAltas.Rows[i]["rfc"].ToString();
                alta.curp = dtAltas.Rows[i]["curp"].ToString();
                alta.paterno = dtAltas.Rows[i]["paterno"].ToString();
                alta.materno = dtAltas.Rows[i]["materno"].ToString();
                alta.nombre = dtAltas.Rows[i]["nombre"].ToString();
                alta.contrato = int.Parse(dtAltas.Rows[i]["contrato"].ToString());
                alta.jornada = int.Parse(dtAltas.Rows[i]["jornada"].ToString());
                alta.fechaingreso = DateTime.Parse(dtAltas.Rows[i]["fechaingreso"].ToString());
                alta.sdi = decimal.Parse(dtAltas.Rows[i]["sdi"].ToString());
                alta.cp = dtAltas.Rows[i]["cp"].ToString();
                alta.fechanacimiento = DateTime.Parse(dtAltas.Rows[i]["fechanacimiento"].ToString());
                alta.estado = dtAltas.Rows[i]["estado"].ToString();
                alta.noestado = int.Parse(dtAltas.Rows[i]["noestado"].ToString());
                alta.clinica = dtAltas.Rows[i]["clinica"].ToString();
                alta.sexo = dtAltas.Rows[i]["sexo"].ToString();
                lstAltas.Add(alta);
            }

            return lstAltas;
        }

        public List<Altas> obtenerAlta(Altas e)
        {
            DataTable dtAltas = new DataTable();
            List<Altas> lstAltas = new List<Altas>();
            Command.CommandText = @"select fechaingreso from suaAltas where idempresa = @idempresa and idtrabajador = @idtrabajador
                        and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("periodoinicio", e.periodoInicio);
            Command.Parameters.AddWithValue("periodofin", e.periodoFin);
            dtAltas = SelectData(Command);

            for (int i = 0; i < dtAltas.Rows.Count; i++)
            {
                Altas alta = new Altas();
                alta.fechaingreso = DateTime.Parse(dtAltas.Rows[i]["fechaingreso"].ToString());
                lstAltas.Add(alta);
            }

            return lstAltas;
        }

        public int existeAlta(int idEmpresa, int idTrabajador, DateTime inicio, DateTime fin)
        {
            Command.CommandText = @"select count(*) from suaAltas where idempresa = @idempresa and periodoinicio = @inicio and periodofin = @fin
                        and idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            int dato = int.Parse(Select(Command).ToString());
            return dato;
        }

        public int insertaAlta(Altas a)
        {
            Command.CommandText = "insert into suaAltas (idtrabajador,idempresa,registropatronal,nss,rfc,curp,paterno,materno,nombre,contrato,jornada,fechaingreso,diasproporcionales,sdi,fechanacimiento,estado,noestado,sexo,periodoinicio,periodofin) " +
                "values (@idtrabajador,@idempresa,@registropatronal,@nss,@rfc,@curp,@paterno,@materno,@nombre,@contrato,@jornada,@fechaingreso,@diasproporcionales,@sdi,@fechanacimiento,@estado,@noestado,@sexo,@periodoinicio,@periodofin)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",a.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", a.idempresa);
            Command.Parameters.AddWithValue("registropatronal", a.registropatronal);
            Command.Parameters.AddWithValue("nss", a.nss);
            Command.Parameters.AddWithValue("rfc", a.rfc);
            Command.Parameters.AddWithValue("curp", a.curp);
            Command.Parameters.AddWithValue("paterno", a.paterno);
            Command.Parameters.AddWithValue("materno", a.materno);
            Command.Parameters.AddWithValue("nombre", a.nombre);
            Command.Parameters.AddWithValue("contrato", a.contrato);
            Command.Parameters.AddWithValue("jornada", a.jornada);
            Command.Parameters.AddWithValue("fechaingreso", a.fechaingreso);
            Command.Parameters.AddWithValue("diasproporcionales", a.diasproporcionales);
            Command.Parameters.AddWithValue("sdi", a.sdi);
            Command.Parameters.AddWithValue("fechanacimiento", a.fechanacimiento);
            Command.Parameters.AddWithValue("estado", a.estado);
            Command.Parameters.AddWithValue("noestado", a.noestado);
            Command.Parameters.AddWithValue("sexo", a.sexo);
            Command.Parameters.AddWithValue("periodoinicio", a.periodoInicio);
            Command.Parameters.AddWithValue("periodofin", a.periodoFin);
            return Command.ExecuteNonQuery();
        }

        public int actualizaAlta(Altas a)
        {
            Command.CommandText = "update suaAltas set nss = @nss, rfc = @rfc, curp = @curp, paterno = @paterno, materno = @materno, nombre = @nombre," +
                "fechaingreso = @fechaingreso, diasproporcionales = @diasproporcionales, sdi = @sdi, fechanacimiento = @fechanacimiento, estado = @estado, noestado = @noestado, sexo = @sexo " + 
                "where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("nss", a.nss);
            Command.Parameters.AddWithValue("rfc", a.rfc);
            Command.Parameters.AddWithValue("curp", a.curp);
            Command.Parameters.AddWithValue("paterno", a.paterno);
            Command.Parameters.AddWithValue("materno", a.materno);
            Command.Parameters.AddWithValue("nombre", a.nombre);
            Command.Parameters.AddWithValue("fechaingreso", a.fechaingreso);
            Command.Parameters.AddWithValue("sdi", a.sdi);
            Command.Parameters.AddWithValue("fechanacimiento", a.fechanacimiento);
            Command.Parameters.AddWithValue("diasproporcionales", a.diasproporcionales);
            Command.Parameters.AddWithValue("estado", a.estado);
            Command.Parameters.AddWithValue("noestado", a.noestado);
            Command.Parameters.AddWithValue("sexo", a.sexo);
            return Command.ExecuteNonQuery();
        }

        public int actualizaAltaComplemento(Altas a)
        {
            Command.CommandText = "update suaAltas set jornada = @jornada, contrato = @contrato, cp = @cp, clinica = @clinica where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("jornada", a.jornada);
            Command.Parameters.AddWithValue("contrato", a.contrato);
            Command.Parameters.AddWithValue("cp", a.cp);
            Command.Parameters.AddWithValue("clinica", a.clinica);
            return Command.ExecuteNonQuery();
        }

        public object existeAlta(Altas a)
        {
            Command.CommandText = "select count(*) from suaAltas where idtrabajador = @idtrabajador and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("periodoinicio", a.periodoInicio);
            Command.Parameters.AddWithValue("periodofin", a.periodoFin);
            object dato = Select(Command);
            return dato;
        }

        public object diasProporcionales(Altas a)
        {
            Command.CommandText = "select diasproporcionales from suaaltas where idtrabajador = @idtrabajador and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("periodoinicio", a.periodoInicio);
            Command.Parameters.AddWithValue("periodofin", a.periodoFin);
            object dato = Select(Command);
            return dato;
        }

        public object fechaAlta(Altas a)
        {
            Command.CommandText = "select fechaingreso from suaAltas where idtrabajador = @idtrabajador and periodoInicio = @inicio and periodofin = @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("inicio", a.periodoInicio);
            Command.Parameters.AddWithValue("fin", a.periodoFin);
            object dato = Select(Command);
            return dato;
        }

        public int eliminaAlta(int idtrabajador)
        {
            Command.CommandText = "delete from suaAltas where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            return Command.ExecuteNonQuery();
        }
    }
}

