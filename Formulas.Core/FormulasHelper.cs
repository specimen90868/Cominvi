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
                lstFormulas.Add(formula);
            }
            return lstFormulas;
        }
    }
}
