using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Catalogos.Core
{
    public class CatalogosHelper : Data.Obj.DataObj
    {
        public List<Catalogo> obtenerCatalogos()
        {
            DataTable dtCatalogo = new DataTable();
            List<Catalogo> lstCatalogo = new List<Catalogo>();
            Command.CommandText = "select id, valor, descripcion, grupo, grupodescripcion from catalogo";
            Command.Parameters.Clear();
            dtCatalogo = SelectData(Command);
            for (int i = 0; i < dtCatalogo.Rows.Count; i++)
            {
                Catalogo cat = new Catalogo();
                cat.id = int.Parse(dtCatalogo.Rows[i]["id"].ToString());
                cat.valor = dtCatalogo.Rows[i]["valor"].ToString();
                cat.descripcion = dtCatalogo.Rows[i]["descripcion"].ToString();
                cat.grupo = int.Parse(dtCatalogo.Rows[i]["grupo"].ToString());
                cat.grupodescripcion = dtCatalogo.Rows[i]["grupodescripcion"].ToString();
                lstCatalogo.Add(cat);
            }
            return lstCatalogo;
        }

        public List<Catalogo> obtenerGrupo(Catalogo c)
        {
            DataTable dtCatalogo = new DataTable();
            List<Catalogo> lstCatalogo = new List<Catalogo>();
            Command.CommandText = "select id, descripcion from catalogo where grupodescripcion = @grupodescripcion";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("grupodescripcion",c.grupodescripcion);
            dtCatalogo = SelectData(Command);
            for (int i = 0; i < dtCatalogo.Rows.Count; i++)
            {
                Catalogo cat = new Catalogo();
                cat.id = int.Parse(dtCatalogo.Rows[i]["id"].ToString());
                cat.descripcion = dtCatalogo.Rows[i]["descripcion"].ToString();
                lstCatalogo.Add(cat);
            }
            return lstCatalogo;
        }

        public List<Catalogo> obtenerGrupo(string grupo)
        {
            DataTable dtCatalogo = new DataTable();
            List<Catalogo> lstCatalogo = new List<Catalogo>();
            Command.CommandText = "select valor, valor + ' - ' + descripcion as descripcion from catalogo where grupodescripcion = @grupodescripcion";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("grupodescripcion", grupo);
            dtCatalogo = SelectData(Command);
            for (int i = 0; i < dtCatalogo.Rows.Count; i++)
            {
                Catalogo cat = new Catalogo();
                cat.valor = dtCatalogo.Rows[i]["valor"].ToString();
                cat.descripcion = dtCatalogo.Rows[i]["descripcion"].ToString();
                lstCatalogo.Add(cat);
            }
            return lstCatalogo;
        }

        public List<Catalogo> obtenerControlIncapacidad()
        {
            DataTable dtCatalogo = new DataTable();
            List<Catalogo> lstCatalogo = new List<Catalogo>();
            Command.CommandText = "select id, descripcion from catalogo where grupo in (15,16)";
            Command.Parameters.Clear();
            dtCatalogo = SelectData(Command);
            for (int i = 0; i < dtCatalogo.Rows.Count; i++)
            {
                Catalogo cat = new Catalogo();
                cat.id = int.Parse(dtCatalogo.Rows[i]["id"].ToString());
                cat.descripcion = dtCatalogo.Rows[i]["descripcion"].ToString();
                lstCatalogo.Add(cat);
            }
            return lstCatalogo;
        }

        public List<Catalogo> obtenerTipoIncapacidad()
        {
            DataTable dtCatalogo = new DataTable();
            List<Catalogo> lstCatalogo = new List<Catalogo>();
            Command.CommandText = "select id, descripcion from catalogo where grupo in (12,13)";
            Command.Parameters.Clear();
            dtCatalogo = SelectData(Command);
            for (int i = 0; i < dtCatalogo.Rows.Count; i++)
            {
                Catalogo cat = new Catalogo();
                cat.id = int.Parse(dtCatalogo.Rows[i]["id"].ToString());
                cat.descripcion = dtCatalogo.Rows[i]["descripcion"].ToString();
                lstCatalogo.Add(cat);
            }
            return lstCatalogo;
        }
    }
}
