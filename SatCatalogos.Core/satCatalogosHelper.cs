using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatCatalogos.Core
{
    public class satCatalogosHelper : Data.Obj.DataObj
    {
        public List<satRegimenFiscal> obtenerRegimenes()
        {
            DataTable dt = new DataTable();
            List<satRegimenFiscal> lstRegimenFiscal = new List<satRegimenFiscal>();
            Command.CommandText = "select id, descripcion from satRegimenFiscal";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satRegimenFiscal rf = new satRegimenFiscal();
                rf.id = int.Parse(dt.Rows[i]["id"].ToString());
                rf.descripcion = dt.Rows[i]["descripcion"].ToString();
                lstRegimenFiscal.Add(rf);
            }
            return lstRegimenFiscal;
        }

        public List<satEstados> obtenerEstados()
        {
            DataTable dt = new DataTable();
            List<satEstados> lstEstado = new List<satEstados>();
            Command.CommandText = "select idestado, estado + '-' + nombreestado as estado from satEstados";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satEstados edo = new satEstados();
                edo.idestado = int.Parse(dt.Rows[i]["idestado"].ToString());
                edo.estado = dt.Rows[i]["estado"].ToString();
                lstEstado.Add(edo);
            }
            return lstEstado;
        }

        public List<satMetodoPago> obtenerMetodoPago()
        {
            DataTable dt = new DataTable();
            List<satMetodoPago> lstMetodoPago = new List<satMetodoPago>();
            Command.CommandText = "select idmetodopago, descripcion from satMetodoPago";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satMetodoPago mp = new satMetodoPago();
                mp.idmetodopago = int.Parse(dt.Rows[i]["idmetodopago"].ToString());
                mp.descripcion = dt.Rows[i]["descripcion"].ToString();
                lstMetodoPago.Add(mp);
            }
            return lstMetodoPago;
        }

        public List<satRiesgoPuesto> obtenerRiesgoPuesto()
        {
            DataTable dt = new DataTable();
            List<satRiesgoPuesto> lstRiesgoPuesto = new List<satRiesgoPuesto>();
            Command.CommandText = "select id, descripcion from satRiesgoPuesto";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satRiesgoPuesto rp = new satRiesgoPuesto();
                rp.id = int.Parse(dt.Rows[i]["id"].ToString());
                rp.descripcion = dt.Rows[i]["descripcion"].ToString();
                lstRiesgoPuesto.Add(rp);
            }
            return lstRiesgoPuesto;
        }

        public List<satTipoContrato> obtenerTipoContrato()
        {
            DataTable dt = new DataTable();
            List<satTipoContrato> lstTipoContrato = new List<satTipoContrato>();
            Command.CommandText = "select id, descripcion from satTipoContrato";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satTipoContrato tp = new satTipoContrato();
                tp.id = int.Parse(dt.Rows[i]["id"].ToString());
                tp.descripcion = dt.Rows[i]["descripcion"].ToString();
                lstTipoContrato.Add(tp);
            }
            return lstTipoContrato;
        }

        public List<satTipoRegimen> obtenerTipoRegimen()
        {
            DataTable dt = new DataTable();
            List<satTipoRegimen> lstTipoRegimen = new List<satTipoRegimen>();
            Command.CommandText = "select idregimen, descripcion from satTipoRegimen";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satTipoRegimen tr = new satTipoRegimen();
                tr.idregimen = int.Parse(dt.Rows[i]["idregimen"].ToString());
                tr.descripcion = dt.Rows[i]["descripcion"].ToString();
                lstTipoRegimen.Add(tr);
            }
            return lstTipoRegimen;
        }

        public List<satTipoDeduccion> obtenerTipoDeduccion()
        {
            DataTable dt = new DataTable();
            List<satTipoDeduccion> lstTipoDeduccion = new List<satTipoDeduccion>();
            Command.CommandText = "select * from satTipoDeduccion";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satTipoDeduccion td = new satTipoDeduccion();
                td.id = int.Parse(dt.Rows[i]["id"].ToString());
                td.tipodeduccion = dt.Rows[i]["tipodeduccion"].ToString();
                td.descripcion = dt.Rows[i]["descripcion"].ToString();
                lstTipoDeduccion.Add(td);
            }
            return lstTipoDeduccion;
        }


        public List<satTipoPercepcion> obtenerTipoPercepcion()
        {
            DataTable dt = new DataTable();
            List<satTipoPercepcion> lstTipoPercepcion = new List<satTipoPercepcion>();
            Command.CommandText = "select * from satTipoPercepcion";
            Command.Parameters.Clear();
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                satTipoPercepcion tp = new satTipoPercepcion();
                tp.id = int.Parse(dt.Rows[i]["id"].ToString());
                tp.tipopercepcion = dt.Rows[i]["tipopercepcion"].ToString();
                tp.descripcion = dt.Rows[i]["descripcion"].ToString();
                lstTipoPercepcion.Add(tp);
            }
            return lstTipoPercepcion;
        }
    }
}
