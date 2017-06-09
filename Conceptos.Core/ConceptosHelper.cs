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
        public List<Conceptos> obtenerConceptos(Conceptos c, int periodo)
        {
            List<Conceptos> lstConcepto = new List<Conceptos>();
            DataTable dtConceptos = new DataTable();
            Command.CommandText = "select * from conceptos where idempresa = @idempresa and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa",c.idempresa);
            Command.Parameters.AddWithValue("periodo", periodo);
            dtConceptos = SelectData(Command);
            for (int i = 0; i < dtConceptos.Rows.Count; i++)
            {
                Conceptos concepto = new Conceptos();
                concepto.id = int.Parse(dtConceptos.Rows[i]["id"].ToString());
                concepto.concepto = dtConceptos.Rows[i]["concepto"].ToString();
                concepto.noconcepto = int.Parse(dtConceptos.Rows[i]["noconcepto"].ToString());
                concepto.tipoconcepto = dtConceptos.Rows[i]["tipoconcepto"].ToString();
                concepto.formula = dtConceptos.Rows[i]["formula"].ToString();
                concepto.formulaexento = dtConceptos.Rows[i]["formulaexento"].ToString();
                concepto.gravado = bool.Parse(dtConceptos.Rows[i]["gravado"].ToString());
                concepto.exento = bool.Parse(dtConceptos.Rows[i]["exento"].ToString());
                concepto.gruposat = dtConceptos.Rows[i]["gruposat"].ToString();
                concepto.visible = bool.Parse(dtConceptos.Rows[i]["visible"].ToString());
                concepto.periodo = int.Parse(dtConceptos.Rows[i]["periodo"].ToString());
                lstConcepto.Add(concepto);
            }
            return lstConcepto;
        }

        public List<Conceptos> obtenerConceptosDeducciones(Conceptos c, int periodo)
        {
            List<Conceptos> lstConcepto = new List<Conceptos>();
            DataTable dtConceptos = new DataTable();
            Command.CommandText = "select * from conceptos where idempresa = @idempresa and tipoconcepto = @tipoconcepto and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            Command.Parameters.AddWithValue("periodo", periodo);
            dtConceptos = SelectData(Command);
            for (int i = 0; i < dtConceptos.Rows.Count; i++)
            {
                Conceptos concepto = new Conceptos();
                concepto.id = int.Parse(dtConceptos.Rows[i]["id"].ToString());
                concepto.concepto = dtConceptos.Rows[i]["concepto"].ToString();
                concepto.noconcepto = int.Parse(dtConceptos.Rows[i]["noconcepto"].ToString());
                concepto.tipoconcepto = dtConceptos.Rows[i]["tipoconcepto"].ToString();
                concepto.formula = dtConceptos.Rows[i]["formula"].ToString();
                concepto.formulaexento = dtConceptos.Rows[i]["formulaexento"].ToString();
                concepto.gravado = bool.Parse(dtConceptos.Rows[i]["gravado"].ToString());
                concepto.exento = bool.Parse(dtConceptos.Rows[i]["exento"].ToString());
                concepto.gruposat = dtConceptos.Rows[i]["gruposat"].ToString();
                concepto.visible = bool.Parse(dtConceptos.Rows[i]["visible"].ToString());
                concepto.periodo = int.Parse(dtConceptos.Rows[i]["periodo"].ToString());
                lstConcepto.Add(concepto);
            }
            return lstConcepto;
        }

        public List<Conceptos> obtenerConceptosDeducciones(Conceptos c)
        {
            List<Conceptos> lstConcepto = new List<Conceptos>();
            DataTable dtConceptos = new DataTable();
            Command.CommandText = "select * from conceptos where idempresa = @idempresa and tipoconcepto = @tipoconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            dtConceptos = SelectData(Command);
            for (int i = 0; i < dtConceptos.Rows.Count; i++)
            {
                Conceptos concepto = new Conceptos();
                concepto.id = int.Parse(dtConceptos.Rows[i]["id"].ToString());
                concepto.concepto = dtConceptos.Rows[i]["concepto"].ToString();
                concepto.noconcepto = int.Parse(dtConceptos.Rows[i]["noconcepto"].ToString());
                concepto.tipoconcepto = dtConceptos.Rows[i]["tipoconcepto"].ToString();
                concepto.formula = dtConceptos.Rows[i]["formula"].ToString();
                concepto.formulaexento = dtConceptos.Rows[i]["formulaexento"].ToString();
                concepto.gravado = bool.Parse(dtConceptos.Rows[i]["gravado"].ToString());
                concepto.exento = bool.Parse(dtConceptos.Rows[i]["exento"].ToString());
                concepto.gruposat = dtConceptos.Rows[i]["gruposat"].ToString();
                concepto.visible = bool.Parse(dtConceptos.Rows[i]["visible"].ToString());
                concepto.periodo = int.Parse(dtConceptos.Rows[i]["periodo"].ToString());
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
                concepto.noconcepto = int.Parse(dtConceptos.Rows[i]["noconcepto"].ToString());
                concepto.concepto = dtConceptos.Rows[i]["concepto"].ToString();
                concepto.tipoconcepto = dtConceptos.Rows[i]["tipoconcepto"].ToString();
                concepto.formula = dtConceptos.Rows[i]["formula"].ToString();
                concepto.formulaexento = dtConceptos.Rows[i]["formulaexento"].ToString();
                concepto.gravado = bool.Parse(dtConceptos.Rows[i]["gravado"].ToString());
                concepto.exento = bool.Parse(dtConceptos.Rows[i]["exento"].ToString());
                concepto.gruposat = dtConceptos.Rows[i]["gruposat"].ToString();
                concepto.visible = bool.Parse(dtConceptos.Rows[i]["visible"].ToString());
                concepto.periodo = int.Parse(dtConceptos.Rows[i]["periodo"].ToString());
                lstConcepto.Add(concepto);
            }
            return lstConcepto;
        }

        public int obtenerConcepto(int idempresa, int noconcepto, int periodo)
        {
            Command.CommandText = "select id from conceptos where idempresa = @idempresa and noconcepto = @noconcepto and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("noconcepto", noconcepto);
            Command.Parameters.AddWithValue("periodo", periodo);
            object valor = Select(Command);
            return int.Parse(valor.ToString());
        }

        public List<Conceptos> obtenerConceptoNomina(Conceptos c)
        {
            List<Conceptos> lstConcepto = new List<Conceptos>();
            DataTable dtConceptos = new DataTable();
            Command.CommandText = "select id, tipoconcepto from conceptos where noconcepto = @noconcepto and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            dtConceptos = SelectData(Command);
            for (int i = 0; i < dtConceptos.Rows.Count; i++)
            {
                Conceptos concepto = new Conceptos();
                concepto.id = int.Parse(dtConceptos.Rows[i]["id"].ToString());
                concepto.tipoconcepto = dtConceptos.Rows[i]["tipoconcepto"].ToString();
                lstConcepto.Add(concepto);
            }
            return lstConcepto;
        }

        public object obtenerIdConcepto(string concepto, int idempresa, int periodo)
        {
            Command.CommandText = "select id from Conceptos where concepto = @concepto and idempresa = @idempresa and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("concepto", concepto);
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("periodo", periodo);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerIdConcepto(int noconcepto, int idempresa, int periodo)
        {
            Command.CommandText = "select id from Conceptos where noconcepto = @noconcepto and idempresa = @idempresa and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("noconcepto", noconcepto);
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("periodo", periodo);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerNoConcepto(Conceptos c)
        {
            Command.CommandText = "select noconcepto from Conceptos where formula = @formula and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("formula", c.formula);
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerFormula(Conceptos c)
        {
            Command.CommandText = "select formula from conceptos where noconcepto = @noconcepto and idempresa = @idempresa and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("periodo", c.periodo);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerFormulaExento(Conceptos c)
        {
            Command.CommandText = "select formulaexento from conceptos where noconcepto = @noconcepto and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            object dato = Select(Command);
            return dato;
        }

        public object existeNoConcepto(Conceptos c)
        {
            Command.CommandText = "select count(*) from conceptos where idempresa = @idempresa and noconcepto = @noconcepto and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("periodo", c.periodo);
            object dato = Select(Command);
            return dato;
        }

        public int insertaConcepto(Conceptos c)
        {
            Command.CommandText = "insert into conceptos (idempresa, noconcepto, concepto, tipoconcepto, formula, formulaexento, gravado, exento, gruposat, visible, periodo) " +
                "values (@idempresa, @noconcepto, @concepto, @tipoconcepto, @formula, @formulaexento, @gravado, @exento, @gruposat, @visible, @periodo)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa",c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("concepto", c.concepto);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            Command.Parameters.AddWithValue("formula",c.formula);
            Command.Parameters.AddWithValue("formulaexento", c.formulaexento);
            Command.Parameters.AddWithValue("gravado", c.gravado);
            Command.Parameters.AddWithValue("exento", c.exento);
            Command.Parameters.AddWithValue("gruposat", c.gruposat);
            Command.Parameters.AddWithValue("visible", c.visible);
            Command.Parameters.AddWithValue("periodo", c.periodo);
            return Command.ExecuteNonQuery();
        }

        public int actualizaConcepto(Conceptos c)
        {
            Command.CommandText = "update conceptos set noconcepto = @noconcepto, concepto = @concepto, tipoconcepto = @tipoconcepto, formula = @formula, formulaexento = @formulaexento, gravado = @gravado, " + 
                "exento = @exento, gruposat = @gruposat, visible = @visible, periodo = @periodo where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", c.id);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("concepto", c.concepto);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            Command.Parameters.AddWithValue("formula", c.formula);
            Command.Parameters.AddWithValue("formulaexento", c.formulaexento);
            Command.Parameters.AddWithValue("gravado", c.gravado);
            Command.Parameters.AddWithValue("exento", c.exento);
            Command.Parameters.AddWithValue("gruposat", c.gruposat);
            Command.Parameters.AddWithValue("visible", c.visible);
            Command.Parameters.AddWithValue("periodo", c.periodo);
            return Command.ExecuteNonQuery();
        }

        public int eliminarConcepto(Conceptos c)
        {
            Command.CommandText = "delete from conceptos where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", c.id);
            return Command.ExecuteNonQuery();
        }

        public object gravaConcepto(Conceptos c)
        {
            Command.CommandText = "select gravado from conceptos where idempresa = @idempresa and noconcepto = @noconcepto and tipoconcepto = @tipoconcepto and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            Command.Parameters.AddWithValue("periodo", c.periodo);
            object dato = Select(Command);
            return dato;
        }

        public object exentaConcepto(Conceptos c)
        {
            Command.CommandText = "select exento from conceptos where idempresa = @idempresa and noconcepto = @noconcepto and tipoconcepto = @tipoconcepto and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            Command.Parameters.AddWithValue("periodo", c.periodo);
            object dato = Select(Command);
            return dato;
        }

        #region RELACION TRABAJADOR - CONCEPTO

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

        public int eliminaConceptoTrabajador(int idtrabajador)
        {
            Command.CommandText = "delete from ConceptoTrabajador where idempleado = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            return Command.ExecuteNonQuery();
        }

        public int actualizaConceptoTrabajador(int idtrabajador, int idempresa, int idperiodo)
        {
            Command.CommandText = "exec stp_ActualizaConceptosTrabajador @idtrabajador, @idempresa, @idperiodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("idperiodo", idperiodo);
            return Command.ExecuteNonQuery();
        }

        #endregion

        public List<ConceptosEmpresa> obtenerConceptosEmpresa(ConceptosEmpresa ce)
        {
            DataTable dt = new DataTable();
            List<ConceptosEmpresa> lstConceptos = new List<ConceptosEmpresa>();
            Command.CommandText = "select * from ConceptosEmpresa where idempresa = @idempresa and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", ce.idempresa);
            Command.Parameters.AddWithValue("periodo", ce.periodo);
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ConceptosEmpresa concepto = new ConceptosEmpresa();
                concepto.id = int.Parse(dt.Rows[i]["id"].ToString());
                concepto.idempresa = int.Parse(dt.Rows[i]["idempresa"].ToString());
                concepto.idconcepto = int.Parse(dt.Rows[i]["idconcepto"].ToString());
                concepto.noconcepto = int.Parse(dt.Rows[i]["noconcepto"].ToString());
                concepto.asignacion = bool.Parse(dt.Rows[i]["asignacion"].ToString());
                concepto.periodo = int.Parse(dt.Rows[i]["periodo"].ToString());
                lstConceptos.Add(concepto);
            }
            return lstConceptos;
        }

        public int actualizaAsignacionConcepto(ConceptosEmpresa ce)
        {
            Command.CommandText = @"update ConceptosEmpresa set asignacion = @asignacion where idempresa = @idempresa and periodo = @periodo
                                    and idconcepto = @idconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("asignacion", ce.asignacion);
            Command.Parameters.AddWithValue("idempresa", ce.idempresa);
            Command.Parameters.AddWithValue("periodo", ce.periodo);
            Command.Parameters.AddWithValue("idconcepto", ce.idconcepto);
            return Command.ExecuteNonQuery();
        }

        public int eliminaConceptoEmpresa(ConceptosEmpresa ce)
        {
            Command.CommandText = @"delete from ConceptosEmpresa where idempresa = @idempresa and idconcepto = @idconcepto and periodo = @periodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", ce.idempresa);
            Command.Parameters.AddWithValue("periodo", ce.periodo);
            Command.Parameters.AddWithValue("idconcepto", ce.idconcepto);
            return Command.ExecuteNonQuery();
        }

        public int insertaConceptoEmpresa(ConceptosEmpresa ce)
        {
            Command.CommandText = @"insert into ConceptosEmpresa (idempresa, idconcepto, noconcepto, asignacion, periodo) values (@idempresa, @idconcepto, @noconcepto, @asignacion, @periodo)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", ce.idempresa);
            Command.Parameters.AddWithValue("periodo", ce.periodo);
            Command.Parameters.AddWithValue("idconcepto", ce.idconcepto);
            Command.Parameters.AddWithValue("noconcepto", ce.noconcepto);
            Command.Parameters.AddWithValue("asignacion", ce.asignacion);
            return Command.ExecuteNonQuery();
        }

    }
}

