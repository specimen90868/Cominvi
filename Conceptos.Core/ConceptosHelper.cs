﻿using System;
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
                concepto.noconcepto = int.Parse(dtConceptos.Rows[i]["noconcepto"].ToString());
                concepto.tipoconcepto = dtConceptos.Rows[i]["tipoconcepto"].ToString();
                concepto.formula = dtConceptos.Rows[i]["formula"].ToString();
                concepto.formulaexento = dtConceptos.Rows[i]["formulaexento"].ToString();
                concepto.gravado = bool.Parse(dtConceptos.Rows[i]["gravado"].ToString());
                concepto.exento = bool.Parse(dtConceptos.Rows[i]["exento"].ToString());
                concepto.gruposat = dtConceptos.Rows[i]["gruposat"].ToString();
                concepto.visible = bool.Parse(dtConceptos.Rows[i]["visible"].ToString());
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
                lstConcepto.Add(concepto);
            }
            return lstConcepto;
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

        public object obtenerIdConcepto(string concepto, int idempresa)
        {
            Command.CommandText = "select id from Conceptos where concepto = @concepto and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("concepto", concepto);
            Command.Parameters.AddWithValue("idempresa", idempresa);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerIdConcepto(int noconcepto, int idempresa)
        {
            Command.CommandText = "select id from Conceptos where noconcepto = @noconcepto and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("noconcepto", noconcepto);
            Command.Parameters.AddWithValue("idempresa", idempresa);
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
            Command.CommandText = "select formula from conceptos where noconcepto = @noconcepto and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
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
            Command.CommandText = "select count(*) from conceptos where idempresa = @idempresa and noconcepto = @noconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            object dato = Select(Command);
            return dato;
        }

        public int insertaConcepto(Conceptos c)
        {
            Command.CommandText = "insert into conceptos (idempresa, noconcepto, concepto, tipoconcepto, formula, formulaexento, gravado, exento, gruposat, visible) " +
                "values (@idempresa, @noconcepto, @concepto, @tipoconcepto, @formula, @formulaexento, @gravado, @exento, @gruposat, @visible)";
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
            return Command.ExecuteNonQuery();
        }

        public int actualizaConcepto(Conceptos c)
        {
            Command.CommandText = "update conceptos set noconcepto = @noconcepto, concepto = @concepto, tipoconcepto = @tipoconcepto, formula = @formula, formulaexento = @formulaexento, gravado = @gravado, " + 
                "exento = @exento, gruposat = @gruposat, visible = @visible where id = @id";
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
            Command.CommandText = "select gravado from conceptos where idempresa = @idempresa and noconcepto = @noconcepto and tipoconcepto = @tipoconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
            object dato = Select(Command);
            return dato;
        }

        public object exentaConcepto(Conceptos c)
        {
            Command.CommandText = "select exento from conceptos where idempresa = @idempresa and noconcepto = @noconcepto and tipoconcepto = @tipoconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", c.idempresa);
            Command.Parameters.AddWithValue("noconcepto", c.noconcepto);
            Command.Parameters.AddWithValue("tipoconcepto", c.tipoconcepto);
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

        #endregion

    }
}

