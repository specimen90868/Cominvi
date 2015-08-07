using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Factores.Core
{
    public class FactoresHelper : Data.Obj.DataObj
    {
        public List<Factores> obtenerFactores()
        {
            DataTable dtFactores = new DataTable();
            List<Factores> lstFactores = new List<Factores>();
            Command.CommandText = "select idfactor, anio, valor from factores";
            Command.Parameters.Clear();
            dtFactores = SelectData(Command);
            for (int i = 0; i < dtFactores.Rows.Count; i++)
            {
                Factores f = new Factores();
                f.idfactor = int.Parse(dtFactores.Rows[i]["idfactor"].ToString());
                f.anio = int.Parse(dtFactores.Rows[i]["anio"].ToString());
                f.valor = decimal.Parse(dtFactores.Rows[i]["valor"].ToString());
                lstFactores.Add(f);
            }
            return lstFactores;
        }

        public List<Factores> obtenerFactor(Factores f)
        {
            DataTable dtFactores = new DataTable();
            List<Factores> lstFactores = new List<Factores>();
            Command.CommandText = "select idfactor, anio, valor from factores where idfactor = @idfactor";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idfactor",f.idfactor);
            dtFactores = SelectData(Command);
            for (int i = 0; i < dtFactores.Rows.Count; i++)
            {
                Factores factor = new Factores();
                factor.idfactor = int.Parse(dtFactores.Rows[i]["idfactor"].ToString());
                factor.anio = int.Parse(dtFactores.Rows[i]["anio"].ToString());
                factor.valor = decimal.Parse(dtFactores.Rows[i]["valor"].ToString());
                lstFactores.Add(factor);
            }
            return lstFactores;
        }

        public object FactorDePago(Factores f)
        {
            Command.CommandText = "select top 1 valor from factores where anio <= @anio order by anio desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("anio", f.anio);
            object dato = Select(Command);
            return dato;
        }

        public int insertaFactor(Factores f)
        {
            Command.CommandText = "insert into factores (anio, valor) values (@anio, @valor)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("anio", f.anio);
            Command.Parameters.AddWithValue("valor", f.valor);
            return Command.ExecuteNonQuery();
        }

        public int actualizaFactor(Factores f)
        {
            Command.CommandText = "update factores set anio = @anio, valor = @valor where idfactor = @idfactor";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idfactor", f.idfactor);
            Command.Parameters.AddWithValue("anio", f.anio);
            Command.Parameters.AddWithValue("valor", f.valor);
            return Command.ExecuteNonQuery();
        }

        public int bajaFactor(Factores f)
        {
            Command.CommandText = "delete from factores where idfactor = @idfactor";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idfactor", f.idfactor);
            return Command.ExecuteNonQuery();
        }
    }
}
