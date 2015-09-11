using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulas.Core
{
    public class FormulasHelper : Data.Obj.DataObj
    {
        public List<Formulas> obtenerFormulas()
        {
            List<Formulas> lstFormulas = new List<Formulas>();
            DataTable dtFormulas = new DataTable();
            Command.CommandText = "select * from treeFormulas";
            dtFormulas = SelectData(Command);
            for (int i = 0; i < dtFormulas.Rows.Count; i++)
            {
                Formulas formula = new Formulas();
                formula.id = int.Parse(dtFormulas.Rows[i]["id"].ToString());
                formula.nombre = dtFormulas.Rows[i]["nombre"].ToString();
                formula.padre = int.Parse(dtFormulas.Rows[i]["padre"].ToString());
                formula.tabla = dtFormulas.Rows[i]["tabla"].ToString();
                formula.campo = dtFormulas.Rows[i]["campo"].ToString();
                lstFormulas.Add(formula);
            }
            return lstFormulas;
        }

        public List<Formulas> obtenerTablaDatos(List<string> variables)
        {
            string commandText = "select tabla, campo, clausula from treeFormulas where nombre in ({0})";
            string[] paramNombre = variables.Select((s, i) => "@nombre" + i.ToString()).ToArray();
            string inClausula = string.Join(",", paramNombre);
            List<Formulas> lstFormulas = new List<Formulas>();
            DataTable dtFormulas = new DataTable();

            Command.CommandText = string.Format(commandText, inClausula);
            Command.Parameters.Clear();

            for (int i = 0; i < paramNombre.Length; i++)
            {
                Command.Parameters.AddWithValue(paramNombre[i], variables[i]);
            }
           
            dtFormulas = SelectData(Command);
            for (int i = 0; i < dtFormulas.Rows.Count; i++)
            {
                Formulas formula = new Formulas();              
                formula.tabla = dtFormulas.Rows[i]["tabla"].ToString();
                formula.campo = dtFormulas.Rows[i]["campo"].ToString();
                formula.clausula = dtFormulas.Rows[i]["clausula"].ToString();
                lstFormulas.Add(formula);
            }
            return lstFormulas;
        }

        public object obtenerValor(Formulas f, object valor)
        {
            Command.CommandText = "select @campo from @tabla where @clausula = @valor";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("campo", f.campo);
            Command.Parameters.AddWithValue("tabla", f.tabla);
            Command.Parameters.AddWithValue("clausula", f.clausula);
            Command.Parameters.AddWithValue("valor", valor);
            object dato = Select(Command);
            return dato;
        }
    }
}
