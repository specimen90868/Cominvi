using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satTrabajadorNomina.Core
{
    public class satTrabajadorNominaHelper : Data.Obj.DataObj
    {
        public List<satTrabajadorNomina> obtenerTrabajadorNomina(int idtrabajador)
        {
            DataTable dt = new DataTable();
            List<satTrabajadorNomina> lstTrabajadores = new List<satTrabajadorNomina>();
            Command.CommandText = "select * from satTrabajadorNomina where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satTrabajadorNomina stn = new satTrabajadorNomina();
                stn.idtrabajador = int.Parse(dt.Rows[i]["idtrabajador"].ToString());
                stn.idriesgopuesto = int.Parse(dt.Rows[i]["idriesgopuesto"].ToString());
                stn.idtipocontrato = int.Parse(dt.Rows[i]["idtipocontrato"].ToString());
                stn.idestado = int.Parse(dt.Rows[i]["idestado"].ToString());
                stn.idtiporegimen = int.Parse(dt.Rows[i]["idtiporegimen"].ToString());
                stn.idmetodopago = int.Parse(dt.Rows[i]["idmetodopago"].ToString());
                lstTrabajadores.Add(stn);
            }
            return lstTrabajadores;
        }

        public int insertaTrabajadorNomina(satTrabajadorNomina stn)
        {
            Command.CommandText = @"insert into satTrabajadorNomina (idtrabajador, idempresa, idriesgopuesto, idtipocontrato, idestado, idtiporegimen, idmetodopago) values (
                                    @idtrabajador, @idempresa, @idriesgopuesto, @idtipocontrato, @idestado, @idtiporegimen, @idmetodopago)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", stn.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", stn.idempresa);
            Command.Parameters.AddWithValue("idriesgopuesto", stn.idriesgopuesto);
            Command.Parameters.AddWithValue("idtipocontrato", stn.idtiporegimen);
            Command.Parameters.AddWithValue("idestado", stn.idestado);
            Command.Parameters.AddWithValue("idtiporegimen", stn.idtiporegimen);
            Command.Parameters.AddWithValue("idmetodopago", stn.idmetodopago);
            return Command.ExecuteNonQuery();
        }

        public int actualizaTrabajadorNomina(satTrabajadorNomina stn)
        {
            Command.CommandText = @"update satTrabajadorNomina set idriesgopuesto = @idriesgopuesto, idtipocontrato = @idtipocontrato, idestado = @idestado,
                                    idtiporegimen = @idtiporegimen, idmetodopago = @idmetodopago where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", stn.idtrabajador);
            Command.Parameters.AddWithValue("idriesgopuesto", stn.idriesgopuesto);
            Command.Parameters.AddWithValue("idtipocontrato", stn.idtipocontrato);
            Command.Parameters.AddWithValue("idestado", stn.idestado);
            Command.Parameters.AddWithValue("idtiporegimen", stn.idtiporegimen);
            Command.Parameters.AddWithValue("idmetodopago", stn.idmetodopago);
            return Command.ExecuteNonQuery();
        }
    }
}
