using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exportacion.Core
{
    public class ExportacionHelper : Data.Obj.DataObj
    {
        public List<Exportacion> obtenerDatos(int idEmpresa, string formulario)
        {
            List<Exportacion> lstExportacion = new List<Exportacion>();
            DataTable dtExportacion = new DataTable();
            Command.CommandText = "select campo, activo from Exportacion where formulario = @formulario";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("formulario", formulario);
            dtExportacion = SelectData(Command);
            for (int i = 0; i < dtExportacion.Rows.Count; i++)
            {
                Exportacion ex = new Exportacion();
                //ex.id = int.Parse(dtExportacion.Rows[i]["id"].ToString());
                //ex.idempresa = int.Parse(dtExportacion.Rows[i]["idempresa"].ToString());
                //ex.formulario = dtExportacion.Rows[i]["formulario"].ToString();
                ex.campo = dtExportacion.Rows[i]["campo"].ToString();
                ex.activo = bool.Parse(dtExportacion.Rows[i]["activo"].ToString());
                lstExportacion.Add(ex);
            }
            return lstExportacion;
        }

        public DataTable datosExportar(int idEmpresa, string campos)
        {
            Command.CommandText = "exec stp_DatosExportacionTrabajadores @idempresa, @campos";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("campos", campos);
            DataTable dtDatosExportar = new DataTable();
            dtDatosExportar = SelectData(Command);
            return dtDatosExportar;
        }

        public int actualizaExportacion(Exportacion e)
        {
            Command.CommandText = "update Exportacion set activo = @activo where campo = @campo and formulario = @formulario";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("activo", e.activo);
            Command.Parameters.AddWithValue("campo", e.campo);
            Command.Parameters.AddWithValue("formulario", e.formulario);
            return Command.ExecuteNonQuery();
        }
    }
}
