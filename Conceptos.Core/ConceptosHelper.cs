using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conceptos.Core
{
    public class ConceptosHelper : Data.Obj.DataObj
    {
        public List<Conceptos> obtenerConceptos(Conceptos c)
        {
            List<Conceptos> lstConcepto = new List<Conceptos>();
            DataTable dtConceptos = new DataTable();
            Command.CommandText = "select * from conceptos where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa",c.idempresa);
            dtConceptos = SelectData(Command);
            for (int i = 0; i < dtConceptos.Rows.Count; i++)
            {
                Conceptos concepto = new Conceptos();
                concepto.id = int.Parse(dtConceptos.Rows[i]["id"].ToString());
                concepto.concepto = dtConceptos.Rows[i]["concepto"].ToString();
                concepto.tipoconcepto = int.Parse(dtConceptos.Rows[i]["tipoconcepto"].ToString());
                concepto.formula = dtConceptos.Rows[i]["formula"].ToString();
                concepto.gruposat = dtConceptos.Rows[i]["gruposat"].ToString();
                lstConcepto.Add(concepto);
            }
            return lstConcepto;
        }

        public int insertaConcepto(Conceptos c)
        {
            Command.CommandText = "insert into conceptos (idempresa, concepto, tipoconcepto) " +
                "values (@idempresa, @concepto, @tipoconcepto)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa",c.idempresa);
            Command.Parameters.AddWithValue("concepto", c.concepto);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            return Command.ExecuteNonQuery();
        }

        public int actualizaConcepto(Conceptos c)
        {
            Command.CommandText = "update conceptos set concepto = @concepto, tipoconcepto = @tipoconcepto where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", c.id);
            Command.Parameters.AddWithValue("concepto", c.concepto);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            return Command.ExecuteNonQuery();
        }
    }
}
