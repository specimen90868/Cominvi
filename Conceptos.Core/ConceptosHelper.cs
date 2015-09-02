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
                concepto.tipoconcepto = dtConceptos.Rows[i]["tipoconcepto"].ToString();
                concepto.formula = dtConceptos.Rows[i]["formula"].ToString();
                concepto.gruposat = dtConceptos.Rows[i]["gruposat"].ToString();
                lstConcepto.Add(concepto);
            }
            return lstConcepto;
        }

        public List<Conceptos> obtenerConcepto(Conceptos c)
        {
            List<Conceptos> lstConcepto = new List<Conceptos>();
            DataTable dtConceptos = new DataTable();
            Command.CommandText = "select * from conceptos where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", c.id);
            dtConceptos = SelectData(Command);
            for (int i = 0; i < dtConceptos.Rows.Count; i++)
            {
                Conceptos concepto = new Conceptos();
                concepto.id = int.Parse(dtConceptos.Rows[i]["id"].ToString());
                concepto.concepto = dtConceptos.Rows[i]["concepto"].ToString();
                concepto.tipoconcepto = dtConceptos.Rows[i]["tipoconcepto"].ToString();
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

        public int eliminarConcepto(Conceptos c)
        {
            Command.CommandText = "delete from conceptos where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", c.id);
            return Command.ExecuteNonQuery();
        }

        public List<ConceptoTrabajador> obtenerConceptosTrabajador(ConceptoTrabajador ct)
        {
            List<ConceptoTrabajador> lstConceptoTrabajador = new List<ConceptoTrabajador>();
            DataTable dtConceptos = new DataTable();
            Command.CommandText = "select * from ConceptoTrabajador where idempleado = @idempleado";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempleado", ct.idempleado);
            dtConceptos = SelectData(Command);
            for (int i = 0; i < dtConceptos.Rows.Count; i++)
            {
                ConceptoTrabajador concepto = new ConceptoTrabajador();
                concepto.id = int.Parse(dtConceptos.Rows[i]["id"].ToString());
                concepto.idempleado = int.Parse(dtConceptos.Rows[i]["idempleado"].ToString());
                concepto.idconcepto = int.Parse(dtConceptos.Rows[i]["idconcepto"].ToString());
                lstConceptoTrabajador.Add(concepto);
            }
            return lstConceptoTrabajador;
        }

        public object existeConceptoTrabajador(ConceptoTrabajador ct)
        {
            Command.CommandText = "select count(*) from ConceptoTrabajador where idempleado = @idempleado and idconcepto = @idconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempleado", ct.idempleado);
            Command.Parameters.AddWithValue("idconcepto", ct.idconcepto);
            object existe = Select(Command);
            return existe;
        }

        public int insertaConceptoTrabajador(ConceptoTrabajador ct)
        {
            Command.CommandText = "insert into ConceptoTrabajador (idempleado, idconcepto) values (@idempleado, @idconcepto)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempleado", ct.idempleado);
            Command.Parameters.AddWithValue("idconcepto", ct.idconcepto);
            return Command.ExecuteNonQuery();
        }

        public int eliminaConceptoTrabajador(ConceptoTrabajador ct)
        {
            Command.CommandText = "delete from ConceptoTrabajador where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", ct.id);
            return Command.ExecuteNonQuery();
        }

    }
}

