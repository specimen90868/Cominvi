using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imss.Core
{
    public class ImssHelper : Data.Obj.DataObj
    {
        public List<Imss> ObtenerImss()
        {
            List<Imss> lstImss = new List<Imss>();
            DataTable dtImss = new DataTable();
            Command.CommandText = "select * from tablaImss";
            dtImss = SelectData(Command);
            for (int i = 0; i < dtImss.Rows.Count; i++)
            {
                Imss imss = new Imss();
                imss.id = int.Parse(dtImss.Rows[i]["id"].ToString());
                imss.prestacion = dtImss.Rows[i]["prestacion"].ToString();
                imss.porcentaje = decimal.Parse(dtImss.Rows[i]["porcentaje"].ToString());
                imss.calculo = bool.Parse(dtImss.Rows[i]["calculo"].ToString());
                lstImss.Add(imss);
            }
            return lstImss;
        }

        public List<Imss> ObtenerImss(Imss i)
        {
            List<Imss> lstImss = new List<Imss>();
            DataTable dtImss = new DataTable();
            Command.CommandText = "select * from tablaImss where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", i.id);
            dtImss = SelectData(Command);
            for (int j = 0; j < dtImss.Rows.Count; j++)
            {
                Imss imss = new Imss();
                imss.id = int.Parse(dtImss.Rows[j]["id"].ToString());
                imss.prestacion = dtImss.Rows[j]["prestacion"].ToString();
                imss.porcentaje = decimal.Parse(dtImss.Rows[j]["porcentaje"].ToString());
                imss.calculo = bool.Parse(dtImss.Rows[j]["calculo"].ToString());
                lstImss.Add(imss);
            }
            return lstImss;
        }

        public int insertaImss(Imss i)
        {
            Command.CommandText = "insert into tablaImss (prestacion, porcentaje, calculo) values (@prestacion, @porcentaje, @secalcula)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("prestacion", i.prestacion);
            Command.Parameters.AddWithValue("porcentaje", i.porcentaje);
            Command.Parameters.AddWithValue("secalcula", i.calculo);
            return Command.ExecuteNonQuery();
        }

        public int actualizaImss(Imss i)
        {
            Command.CommandText = "update tablaImss set prestacion = @prestacion, calculo = @porcentaje, secalcula = @secalcula where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", i.id);
            Command.Parameters.AddWithValue("prestacion", i.prestacion);
            Command.Parameters.AddWithValue("porcentaje", i.porcentaje);
            Command.Parameters.AddWithValue("secalcula", i.calculo);
            return Command.ExecuteNonQuery();
        }

        public int eliminarImss(Imss i)
        {
            Command.CommandText = "delete from tablaImss where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", i.id);
            return Command.ExecuteNonQuery();
        }

        public decimal CuotaObreroPatronal(Imss i)
        {
            Command.CommandText = @"select sum(porcentaje) from tablaImss where calculo = @calculo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("calculo", i.calculo);
            decimal suma = decimal.Parse(Select(Command).ToString());
            return suma;
        }

        public decimal ExcedenteVSM(int id)
        {
            Command.CommandText = @"select porcentaje from tablaImss where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", id);
            decimal porcentaje = decimal.Parse(Select(Command).ToString());
            return porcentaje;
        }
    }
}
