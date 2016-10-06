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
            Command.CommandText = "select campo, activo, orden from Exportacion where formulario = @formulario order by orden asc";
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
                ex.orden = int.Parse(dtExportacion.Rows[i]["orden"].ToString());
                lstExportacion.Add(ex);
            }
            return lstExportacion;
        }

        public DataTable datosExportar(int idEmpresa, string campos, string tablas)
        {
            Command.CommandText = "exec stp_DatosExportacionTrabajadores @idempresa, @campos, @tablas";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("campos", campos);
            Command.Parameters.AddWithValue("tablas", tablas);
            DataTable dtDatosExportar = new DataTable();
            dtDatosExportar = SelectData(Command);
            return dtDatosExportar;
        }

        public DataTable datosExportar(int idEmpresa, string campos, string tablas, int tiponomina, int periodo, DateTime inicio, DateTime fin)
        {
            Command.CommandText = "exec stp_DatosExportacionNomina @idempresa, @campos, @tablas, @tiponomina, @periodo, @inicio, @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("campos", campos);
            Command.Parameters.AddWithValue("tablas", tablas);
            Command.Parameters.AddWithValue("tiponomina", tiponomina);
            Command.Parameters.AddWithValue("periodo", periodo);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
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

        public int actualizaOrden(Exportacion e)
        {
            Command.CommandText = "update Exportacion set orden = @orden where campo = @campo and formulario = @formulario";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("orden", e.orden);
            Command.Parameters.AddWithValue("campo", e.campo);
            Command.Parameters.AddWithValue("formulario", e.formulario);
            return Command.ExecuteNonQuery();
        }
    }
}
