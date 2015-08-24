using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expediente.Core
{
    public class ExpedienteHelper : Data.Obj.DataObj
    {
        public List<Expediente> obtenerExpedientes(Expediente e)
        {
            List<Expediente> lstExpediente = new List<Expediente>();
            DataTable dtEstados = new DataTable();
            Command.CommandText = "select * from expedientes where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa",e.idempresa);
            dtEstados = SelectData(Command);
            for (int i = 0; i < dtEstados.Rows.Count; i++)
            {
                Expediente exp = new Expediente();
                exp.id = int.Parse(dtEstados.Rows[i]["id"].ToString());
                exp.idtrabajador = int.Parse(dtEstados.Rows[i]["idtrabajador"].ToString());
                exp.idempresa = int.Parse(dtEstados.Rows[i]["idempresa"].ToString());
                exp.contrato = bool.Parse(dtEstados.Rows[i]["contrato"].ToString());
                exp.solicitud = bool.Parse(dtEstados.Rows[i]["solicitud"].ToString());
                exp.altaimss = bool.Parse(dtEstados.Rows[i]["altaimss"].ToString());
                exp.credencial = bool.Parse(dtEstados.Rows[i]["credencial"].ToString());
                exp.actanacimiento = bool.Parse(dtEstados.Rows[i]["actanacimiento"].ToString());
                exp.ife = bool.Parse(dtEstados.Rows[i]["ife"].ToString());
                exp.curp = bool.Parse(dtEstados.Rows[i]["curp"].ToString());
                exp.cdomicilio = bool.Parse(dtEstados.Rows[i]["cdomicilio"].ToString());
                exp.nss = bool.Parse(dtEstados.Rows[i]["nss"].ToString());
                exp.rfc = bool.Parse(dtEstados.Rows[i]["rfc"].ToString());
                exp.infonavit = bool.Parse(dtEstados.Rows[i]["infonavit"].ToString());
                exp.afore = bool.Parse(dtEstados.Rows[i]["afore"].ToString());
                exp.fotografias = bool.Parse(dtEstados.Rows[i]["fotografias"].ToString());
                exp.autorizacion = bool.Parse(dtEstados.Rows[i]["autorizacion"].ToString());
                exp.estatus = int.Parse(dtEstados.Rows[i]["estatus"].ToString());
                exp.observacion = dtEstados.Rows[i]["observacion"].ToString();
                lstExpediente.Add(exp);
            }
            return lstExpediente;
        }

        public List<Expediente> obtenerExpediente(Expediente e)
        {
            List<Expediente> lstExpediente = new List<Expediente>();
            DataTable dtExpediente = new DataTable();
            Command.CommandText = "select * from expedientes where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            dtExpediente = SelectData(Command);
            for (int i = 0; i < dtExpediente.Rows.Count; i++)
            {
                Expediente exp = new Expediente();
                exp.id = int.Parse(dtExpediente.Rows[i]["id"].ToString());
                exp.idtrabajador = int.Parse(dtExpediente.Rows[i]["idtrabajador"].ToString());
                exp.idempresa = int.Parse(dtExpediente.Rows[i]["idempresa"].ToString());
                exp.contrato = bool.Parse(dtExpediente.Rows[i]["contrato"].ToString());
                exp.solicitud = bool.Parse(dtExpediente.Rows[i]["solicitud"].ToString());
                exp.altaimss = bool.Parse(dtExpediente.Rows[i]["altaimss"].ToString());
                exp.credencial = bool.Parse(dtExpediente.Rows[i]["credencial"].ToString());
                exp.actanacimiento = bool.Parse(dtExpediente.Rows[i]["actanacimiento"].ToString());
                exp.ife = bool.Parse(dtExpediente.Rows[i]["ife"].ToString());
                exp.curp = bool.Parse(dtExpediente.Rows[i]["curp"].ToString());
                exp.cdomicilio = bool.Parse(dtExpediente.Rows[i]["cdomicilio"].ToString());
                exp.nss = bool.Parse(dtExpediente.Rows[i]["nss"].ToString());
                exp.rfc = bool.Parse(dtExpediente.Rows[i]["rfc"].ToString());
                exp.infonavit = bool.Parse(dtExpediente.Rows[i]["infonavit"].ToString());
                exp.afore = bool.Parse(dtExpediente.Rows[i]["afore"].ToString());
                exp.fotografias = bool.Parse(dtExpediente.Rows[i]["fotografias"].ToString());
                exp.autorizacion = bool.Parse(dtExpediente.Rows[i]["autorizacion"].ToString());
                exp.estatus = int.Parse(dtExpediente.Rows[i]["estatus"].ToString());
                exp.observacion = dtExpediente.Rows[i]["observacion"].ToString();
                lstExpediente.Add(exp);
            }
            return lstExpediente;
        }

        public object existeExpediente(Expediente e)
        {
            Command.CommandText = "select count(idtrabajador) from expedientes where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public int insertaExpediente(Expediente e)
        {
            Command.CommandText = "insert into expedientes (idtrabajador,idempresa,contrato,solicitud,altaimss,credencial,actanacimiento,ife,curp,cdomicilio,nss,rfc,infonavit,afore,fotografias,autorizacion,estatus,observacion) " +
                "values (@idtrabajador,@idempresa,@contrato,@solicitud,@altaimss,@credencial,@actanacimiento,@ife,@curp,@cdomicilio,@nss,@rfc,@infonavit,@afore,@fotografias,@autorizacion,@estatus,@observacion)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("contrato", e.contrato);
            Command.Parameters.AddWithValue("solicitud", e.solicitud);
            Command.Parameters.AddWithValue("altaimss", e.altaimss);
            Command.Parameters.AddWithValue("credencial", e.credencial);
            Command.Parameters.AddWithValue("actanacimiento", e.actanacimiento);
            Command.Parameters.AddWithValue("ife", e.ife);
            Command.Parameters.AddWithValue("curp", e.curp);
            Command.Parameters.AddWithValue("cdomicilio", e.cdomicilio);
            Command.Parameters.AddWithValue("nss", e.nss);
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("infonavit", e.infonavit);
            Command.Parameters.AddWithValue("afore", e.afore);
            Command.Parameters.AddWithValue("fotografias", e.fotografias);
            Command.Parameters.AddWithValue("autorizacion", e.autorizacion);
            Command.Parameters.AddWithValue("estatus", e.estatus);
            Command.Parameters.AddWithValue("observacion", e.observacion);
            return Command.ExecuteNonQuery();
        }

        public int actualizaExpediente(Expediente e)
        {
            Command.CommandText = "update expedientes set contrato = @contrato,solicitud = @solicitud,altaimss = @altaimss,credencial = @credencial,actanacimiento=@actanacimiento,ife=@ife," +
                "curp = @curp,cdomicilio = @cdomicilio,nss = @nss,rfc = @rfc,infonavit = @infonavit,afore = @afore,fotografias = @fotografias,autorizacion = @autorizacion,estatus = @estatus,observacion = @observacion " +
                "where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("contrato", e.contrato);
            Command.Parameters.AddWithValue("solicitud", e.solicitud);
            Command.Parameters.AddWithValue("altaimss", e.altaimss);
            Command.Parameters.AddWithValue("credencial", e.credencial);
            Command.Parameters.AddWithValue("actanacimiento", e.actanacimiento);
            Command.Parameters.AddWithValue("ife", e.ife);
            Command.Parameters.AddWithValue("curp", e.curp);
            Command.Parameters.AddWithValue("cdomicilio", e.cdomicilio);
            Command.Parameters.AddWithValue("nss", e.nss);
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("infonavit", e.infonavit);
            Command.Parameters.AddWithValue("afore", e.afore);
            Command.Parameters.AddWithValue("fotografias", e.fotografias);
            Command.Parameters.AddWithValue("autorizacion", e.autorizacion);
            Command.Parameters.AddWithValue("estatus", e.estatus);
            Command.Parameters.AddWithValue("observacion", e.observacion);
            return Command.ExecuteNonQuery();
        }

        public int eliminarExpediente(Expediente e)
        {
            Command.CommandText = "delete from expedientes where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            return Command.ExecuteNonQuery();
        }
    }
}
