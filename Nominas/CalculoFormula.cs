using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominas
{
    public class CalculoFormula
    {
        private int idTrabajador;
        private DateTime inicioPeriodo;
        private DateTime finPeriodo;
        private string formula;
        private string formula2;
        private List<CalculoNomina.Core.tmpPagoNomina> lstPercepciones;

        public CalculoFormula(int _idTrabajador, DateTime _inicio, DateTime _fin, string _formula, List<CalculoNomina.Core.tmpPagoNomina> _lstPercepciones = null)
        {
            idTrabajador = _idTrabajador;
            inicioPeriodo = _inicio;
            finPeriodo = _fin;
            formula = _formula;
            lstPercepciones = _lstPercepciones;
        }

        public object calcularFormula() 
        {
            if (formula.Equals("0"))
                return 0;

            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;

            int idPeriodo = 0, diasPeriodo = 0;
            Empleados.Core.EmpleadosHelper empleadosHelper = new Empleados.Core.EmpleadosHelper();
            empleadosHelper.Command = cmd;

            cnx.Open();
            idPeriodo = int.Parse(empleadosHelper.obtenerIdPeriodo(idTrabajador).ToString());
            cnx.Close();

            Periodos.Core.PeriodosHelper periodoHelper = new Periodos.Core.PeriodosHelper();
            periodoHelper.Command = cmd;

            Periodos.Core.Periodos periodos = new Periodos.Core.Periodos();
            periodos.idperiodo = idPeriodo;

            cnx.Open();
            diasPeriodo = int.Parse(periodoHelper.DiasDePago(periodos).ToString());
            cnx.Close();

            List<string> variables = new List<string>();
            variables = GLOBALES.EXTRAEVARIABLES(formula, "[", "]");

            object resultado;

            for (int i = 0; i < variables.Count; i++)
            {
                switch (variables[i])
                {
                    case "SueldoDiario":
                        Empleados.Core.EmpleadosHelper esdh = new Empleados.Core.EmpleadosHelper();
                        esdh.Command = cmd;
                        Empleados.Core.Empleados esd = new Empleados.Core.Empleados();
                        esd.idtrabajador = idTrabajador;
                        cnx.Open();
                        formula = formula.Replace("[" + variables[i] + "]", esdh.obtenerSalarioDiario(esd).ToString());
                        cnx.Close();
                        break;

                    case "SBC":
                        Empleados.Core.EmpleadosHelper esdih = new Empleados.Core.EmpleadosHelper();
                        esdih.Command = cmd;
                        Empleados.Core.Empleados esdi = new Empleados.Core.Empleados();
                        esdi.idtrabajador = idTrabajador;
                        cnx.Open();
                        formula = formula.Replace("[" + variables[i] + "]", esdih.obtenerSalarioDiarioIntegrado(esdi).ToString());
                        cnx.Close();
                        break;

                    case "DiasPrima":
                      
                        Vacaciones.Core.VacacionesHelper pvh = new Vacaciones.Core.VacacionesHelper();
                        pvh.Command = cmd;
                        Vacaciones.Core.VacacionesPrima pv = new Vacaciones.Core.VacacionesPrima();
                        pv.idtrabajador = idTrabajador;
                        pv.idempresa = GLOBALES.IDEMPRESA;
                        pv.periodofin = finPeriodo;
                        pv.periodoinicio = inicioPeriodo;
                        pv.vacacionesprima = "P";

                        cnx.Open();
                        formula = formula.Replace("[" + variables[i] + "]", pvh.pagoVacacionesPrima(pv).ToString());
                        cnx.Close();
                        
                        break;

                    case "DiasVacaciones":

                        Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                        vh.Command = cmd;
                        Vacaciones.Core.VacacionesPrima vp = new Vacaciones.Core.VacacionesPrima();
                        vp.idtrabajador = idTrabajador;
                        vp.idempresa = GLOBALES.IDEMPRESA;
                        vp.periodofin = finPeriodo;
                        vp.periodoinicio = inicioPeriodo;
                        vp.vacacionesprima = "V";
                        
                        cnx.Open();
                        int v = (int)vh.pagoVacacionesPrima(vp);
                        cnx.Close();

                        if (v == 16)
                        {
                            v = v - 1;
                            formula = formula.Replace("[" + variables[i] + "]", v.ToString());
                        }
                        else
                            formula = formula.Replace("[" + variables[i] + "]", v.ToString());

                        break;

                    case "SalarioMinimo":

                        Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
                        sh.Command = cmd;

                        cnx.Open();
                        formula = formula.Replace("[" + variables[i] + "]", sh.obtenerSalarioValor().ToString());
                        cnx.Close();

                        break;

                    case "FactorDescuento":

                        FactorDescuento.Core.FactorDescuentoHelper fdh = new FactorDescuento.Core.FactorDescuentoHelper();
                        fdh.Command = cmd;

                        cnx.Open();
                        formula = formula.Replace("[" + variables[i] + "]", fdh.obtenerFactorDescuento().ToString());
                        cnx.Close();

                        break;

                    case "Faltas":
                        Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                        fh.Command = cmd;
                        Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                        falta.idtrabajador = idTrabajador;
                        falta.fechainicio = inicioPeriodo;
                        falta.fechafin = finPeriodo;

                        cnx.Open();
                        int f = (int)fh.existeFalta(falta);
                        cnx.Close();
                        
                        if (f == 16)
                        {
                            f = f - 1;
                            formula = formula.Replace("[" + variables[i] + "]", f.ToString());
                        }
                        else
                            formula = formula.Replace("[" + variables[i] + "]", f.ToString());

                        break;

                    case "DiasLaborados":
                        int existe = 0, diasBaja = 0, idperiodo = 0, diasPago = 0, diasFaltas = 0;
                        int existeAlta = 0, diasAlta = 0;
                        int existeReingreso = 0, diasReingreso = 0;

                        DateTime fechaAlta, fechaReingreso, fechaBaja;

                        Altas.Core.AltasHelper ah = new Altas.Core.AltasHelper();
                        ah.Command = cmd;
                        Altas.Core.Altas a = new Altas.Core.Altas();
                        a.idtrabajador = idTrabajador;
                        a.periodoInicio = inicioPeriodo;
                        a.periodoFin = finPeriodo;

                        cnx.Open();
                        existeAlta = (int)ah.existeAlta(a);
                        cnx.Close();

                        Reingreso.Core.ReingresoHelper rh = new Reingreso.Core.ReingresoHelper();
                        rh.Command = cmd;
                        Reingreso.Core.Reingresos r = new Reingreso.Core.Reingresos();
                        r.idtrabajador = idTrabajador;
                        r.periodoinicio = inicioPeriodo;
                        r.periodofin = finPeriodo;

                        cnx.Open();
                        existeReingreso = (int)rh.existeReingreso(r);
                        cnx.Close();

                        Bajas.Core.BajasHelper bh = new Bajas.Core.BajasHelper();
                        bh.Command = cmd;
                        Bajas.Core.Bajas baja = new Bajas.Core.Bajas();
                        baja.idtrabajador = idTrabajador;
                        baja.periodoinicio = inicioPeriodo;
                        baja.periodofin = finPeriodo;

                        cnx.Open();
                        existe = (int)bh.existeBaja(baja);
                        
                        cnx.Close();

                        Empleados.Core.EmpleadosHelper edlh = new Empleados.Core.EmpleadosHelper();
                        edlh.Command = cmd;

                        cnx.Open();
                        idperiodo = (int)edlh.obtenerIdPeriodo(idTrabajador);
                        cnx.Close();

                        Periodos.Core.PeriodosHelper pdlh = new Periodos.Core.PeriodosHelper();
                        pdlh.Command = cmd;
                        
                        Periodos.Core.Periodos pdl = new Periodos.Core.Periodos();
                        pdl.idperiodo = idperiodo;

                        cnx.Open();
                        diasPago = (int)pdlh.DiasDePago(pdl);
                        cnx.Close();

                        #region EXISTE ALTA Y BAJA PERO NO REINGRESO
                        if (existeAlta != 0 && existe != 0 && existeReingreso == 0)
                        {
                            int totalDias = 0;

                            cnx.Open();
                            diasAlta = (int)ah.diasProporcionales(a);
                            fechaAlta = DateTime.Parse(ah.fechaAlta(a).ToString());
                            cnx.Close();

                            cnx.Open();
                            diasBaja = (int)bh.diasProporcionales(baja);
                            fechaBaja = DateTime.Parse(bh.fechaBaja(baja).ToString());
                            cnx.Close();

                            if (fechaAlta <= fechaBaja) 
                                totalDias = diasPago - ((diasPago - diasAlta) + (diasPago - diasBaja));

                            if (fechaBaja < fechaAlta)
                                totalDias = diasAlta + diasBaja;

                            Faltas.Core.FaltasHelper faltaHelper = new Faltas.Core.FaltasHelper();
                            faltaHelper.Command = cmd;
                            Faltas.Core.Faltas faltasDL = new Faltas.Core.Faltas();
                            faltasDL.idtrabajador = idTrabajador;
                            faltasDL.fechainicio = inicioPeriodo;
                            faltasDL.fechafin = finPeriodo;

                            cnx.Open();
                            diasFaltas = (int)faltaHelper.existeFalta(faltasDL);
                            cnx.Close();

                            if (diasFaltas >= totalDias)
                                formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                            else
                                formula = formula.Replace("[" + variables[i] + "]", totalDias.ToString());
                        }
                        #endregion

                        #region EXISTE ALTA, NO EXISTE BAJA Y NO EXISTE REINGRESO
                        if (existeAlta != 0 && existe == 0 && existeReingreso == 0)
                        {
                            cnx.Open();
                            diasAlta = (int)ah.diasProporcionales(a);
                            cnx.Close();

                            Faltas.Core.FaltasHelper faltaHelper = new Faltas.Core.FaltasHelper();
                            faltaHelper.Command = cmd;
                            Faltas.Core.Faltas faltasDL = new Faltas.Core.Faltas();
                            faltasDL.idtrabajador = idTrabajador;
                            faltasDL.fechainicio = inicioPeriodo;
                            faltasDL.fechafin = finPeriodo;

                            cnx.Open();
                            diasFaltas = (int)faltaHelper.existeFalta(faltasDL);
                            cnx.Close();

                            if (diasFaltas >= diasAlta)
                            {
                                if (diasFaltas == 16)
                                {
                                    diasFaltas = diasFaltas - 1;
                                    formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                                }
                                else
                                    formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                            }
                            else
                               formula = formula.Replace("[" + variables[i] + "]", diasAlta.ToString());
                        }
                        #endregion

                        #region NO EXISTE ALTA, EXISTE BAJA Y NO EXISTE REINGRESO
                        if (existeAlta == 0 && existe != 0 && existeReingreso == 0)
                        {
                            cnx.Open();
                            diasBaja = (int)bh.diasProporcionales(baja);
                            cnx.Close();

                            Faltas.Core.FaltasHelper faltaHelper = new Faltas.Core.FaltasHelper();
                            faltaHelper.Command = cmd;
                            Faltas.Core.Faltas faltasDL = new Faltas.Core.Faltas();
                            faltasDL.idtrabajador = idTrabajador;
                            faltasDL.fechainicio = inicioPeriodo;
                            faltasDL.fechafin = finPeriodo;

                            cnx.Open();
                            diasFaltas = (int)faltaHelper.existeFalta(faltasDL);
                            cnx.Close();

                            if (diasFaltas >= diasBaja)
                                if (diasFaltas == 16)
                                {
                                    diasFaltas = diasFaltas - 1;
                                    formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                                }
                                else
                                    formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                            else
                               formula = formula.Replace("[" + variables[i] + "]", diasBaja.ToString());
                        }
                        #endregion

                        #region EXISTE REINGRESO Y BAJA PERO NO ALTA
                        if (existeReingreso != 0 && existe != 0 && existeAlta == 0)
                        {
                            int totalDias = 0;

                            cnx.Open();
                            diasReingreso = (int)rh.diasProporcionales(r);
                            fechaReingreso = DateTime.Parse(rh.fechaReingreso(r).ToString());
                            cnx.Close();

                            cnx.Open();
                            diasBaja = (int)bh.diasProporcionales(baja);
                            fechaBaja = DateTime.Parse(bh.fechaBaja(baja).ToString());
                            cnx.Close();

                            if (fechaBaja < fechaReingreso)
                                totalDias = diasReingreso + diasBaja;

                            if (fechaBaja == fechaReingreso)
                                totalDias = diasPago - (diasPago - (diasReingreso) + (diasPago - diasBaja));

                            if (fechaBaja > fechaReingreso)
                                totalDias = diasPago - (diasPago - (diasReingreso) + (diasPago - diasBaja));

                            Faltas.Core.FaltasHelper faltaHelper = new Faltas.Core.FaltasHelper();
                            faltaHelper.Command = cmd;
                            Faltas.Core.Faltas faltasDL = new Faltas.Core.Faltas();
                            faltasDL.idtrabajador = idTrabajador;
                            faltasDL.fechainicio = inicioPeriodo;
                            faltasDL.fechafin = finPeriodo;

                            cnx.Open();
                            diasFaltas = (int)faltaHelper.existeFalta(faltasDL);
                            cnx.Close();

                            if (diasFaltas >= totalDias)
                                formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                            else
                                formula = formula.Replace("[" + variables[i] + "]", totalDias.ToString());
                        }
                        #endregion

                        #region EXISTE REINGRESO, NO EXISTE BAJA Y NO EXISTE ALTA
                        if (existeReingreso != 0 && existe == 0 && existeAlta == 0)
                        {
                            cnx.Open();
                            diasReingreso = (int)rh.diasProporcionales(r);
                            fechaReingreso = DateTime.Parse(rh.fechaReingreso(r).ToString());
                            cnx.Close();

                            Faltas.Core.FaltasHelper faltaHelper = new Faltas.Core.FaltasHelper();
                            faltaHelper.Command = cmd;
                            Faltas.Core.Faltas faltasDL = new Faltas.Core.Faltas();
                            faltasDL.idtrabajador = idTrabajador;
                            faltasDL.fechainicio = inicioPeriodo;
                            faltasDL.fechafin = finPeriodo;

                            cnx.Open();
                            diasFaltas = (int)faltaHelper.existeFalta(faltasDL);
                            cnx.Close();

                            if (diasFaltas >= diasReingreso)
                                if (diasFaltas == 16)
                                {
                                    diasFaltas = diasFaltas - 1;
                                    formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                                }
                                else
                                    formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                            else
                               formula = formula.Replace("[" + variables[i] + "]", diasReingreso.ToString());
                        }
                        #endregion

                        #region NO EXISTE REINGRESO, EXISTE BAJA Y NO EXISTE ALTA
                        //if (existeReingreso == 0 && existe != 0 && existeAlta == 0)
                        //{
                        //    cnx.Open();
                        //    diasBaja = (int)bh.diasProporcionales(baja);
                        //    cnx.Close();

                        //    Faltas.Core.FaltasHelper faltaHelper = new Faltas.Core.FaltasHelper();
                        //    faltaHelper.Command = cmd;
                        //    Faltas.Core.Faltas faltasDL = new Faltas.Core.Faltas();
                        //    faltasDL.idtrabajador = idTrabajador;
                        //    faltasDL.fechainicio = inicioPeriodo;
                        //    faltasDL.fechafin = finPeriodo;

                        //    cnx.Open();
                        //    diasFaltas = (int)faltaHelper.existeFalta(faltasDL);
                        //    cnx.Close();

                        //    if (diasFaltas >= diasBaja)
                        //        if (diasFaltas == 16)
                        //        {
                        //            diasFaltas = diasFaltas - 1;
                        //            formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                        //        }
                        //        else
                        //            formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                        //    else
                        //        formula = formula.Replace("[" + variables[i] + "]", diasBaja.ToString());
                        //}
                        #endregion

                        #region NO EXISTE REINGRESO, BAJAS NI ALTAS
                        if (existeAlta == 0 && existe == 0 && existeReingreso == 0)
                        {
                            //int existeIncidenciaPago = 0, existeFaltaPago = 0;
                            //Se obtiene existencia de faltas
                            //Faltas.Core.FaltasHelper fPago = new Faltas.Core.FaltasHelper();
                            //fPago.Command = cmd;
                            //Faltas.Core.Faltas faltaPago = new Faltas.Core.Faltas();
                            //faltaPago.idtrabajador = idTrabajador;
                            //faltaPago.fechainicio = inicioPeriodo;
                            //faltaPago.fechafin = finPeriodo;

                            //cnx.Open();
                            //existeFaltaPago = (int)fPago.existeFalta(faltaPago);
                            //cnx.Close();

                            //Se obtiene existencia de incapacidades
                            //Incidencias.Core.IncidenciasHelper iPago = new Incidencias.Core.IncidenciasHelper();
                            //iPago.Command = cmd;

                            //Incidencias.Core.Incidencias incPago = new Incidencias.Core.Incidencias();
                            //incPago.idtrabajador = idTrabajador;
                            //incPago.periodoinicio = inicioPeriodo;
                            //incPago.periodofin = finPeriodo;

                            //cnx.Open();
                            //existeIncidenciaPago = (int)iPago.existeIncidencia(incPago);
                            //cnx.Close();

                            //Se obtiene los dias de pago.

                            //if (existeFaltaPago != 0 || existeIncidenciaPago != 0)
                            //{
                            //    if (diasPago == 15)
                            //    {
                            //        if (inicioPeriodo.Day > 15)
                            //        {
                            //            diasMesLaborados = DateTime.DaysInMonth(inicioPeriodo.Year, inicioPeriodo.Month);
                            //            diasMesLaborados = diasMesLaborados - 15;
                            //            diasPago = diasMesLaborados;
                            //        }
                            //    }
                            //}

                            formula = formula.Replace("[" + variables[i] + "]", diasPago.ToString());
                        }
                        #endregion

                        #region EXISTE ALTA, BAJA Y REINGRESO
                        if (existeAlta != 0 && existe != 0 && existeReingreso != 0)
                        {
                            int totalDias = 0;

                            cnx.Open();
                            diasAlta = (int)ah.diasProporcionales(a);
                            fechaAlta = DateTime.Parse(ah.fechaAlta(a).ToString());
                            cnx.Close();

                            cnx.Open();
                            diasBaja = (int)bh.diasProporcionales(baja);
                            fechaBaja = DateTime.Parse(bh.fechaBaja(baja).ToString());
                            cnx.Close();

                            cnx.Open();
                            diasReingreso = (int)rh.diasProporcionales(r);
                            fechaReingreso = DateTime.Parse(rh.fechaReingreso(r).ToString());
                            cnx.Close();

                            if (fechaAlta <= fechaBaja)
                                totalDias = diasPago - ((diasPago - diasAlta) + (diasPago - diasBaja));
                            if (fechaBaja < fechaReingreso)
                                totalDias = totalDias + diasReingreso;
                            if (fechaBaja == fechaReingreso)
                                totalDias = totalDias + diasReingreso - 1;

                            Faltas.Core.FaltasHelper faltaHelper = new Faltas.Core.FaltasHelper();
                            faltaHelper.Command = cmd;
                            Faltas.Core.Faltas faltasDL = new Faltas.Core.Faltas();
                            faltasDL.idtrabajador = idTrabajador;
                            faltasDL.fechainicio = inicioPeriodo;
                            faltasDL.fechafin = finPeriodo;

                            cnx.Open();
                            diasFaltas = (int)faltaHelper.existeFalta(faltasDL);
                            cnx.Close();

                            if (diasFaltas >= totalDias)
                                formula = formula.Replace("[" + variables[i] + "]", diasFaltas.ToString());
                            else
                                formula = formula.Replace("[" + variables[i] + "]", totalDias.ToString());
                        }
                        #endregion

                        break;

                    case "DiasIncapacidad":
                        object existeIncidencia = null;
                        int diasIncapacidad = 0;
                        Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
                        ih.Command = cmd;

                        Incidencias.Core.Incidencias inc = new Incidencias.Core.Incidencias();
                        inc.idtrabajador = idTrabajador;
                        inc.periodoinicio = inicioPeriodo;
                        inc.periodofin = finPeriodo;

                        cnx.Open();
                        existeIncidencia = ih.existeIncidencia(inc);
                        cnx.Close();

                        if ((int)existeIncidencia != 0)
                        {
                            cnx.Open();
                            diasIncapacidad = (int)ih.diasIncidencia(inc);
                            cnx.Close();
                            if (diasIncapacidad == 16)
                            {
                                diasIncapacidad = diasIncapacidad - 1;
                                formula = formula.Replace("[" + variables[i] + "]", diasIncapacidad.ToString());
                            }
                            else
                                formula = formula.Replace("[" + variables[i] + "]", diasIncapacidad.ToString());
                        }
                        else
                        {
                            diasIncapacidad = 0;
                            formula = formula.Replace("[" + variables[i] + "]", diasIncapacidad.ToString());
                        }
                        break;

                    case "Infonavit":

                        Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                        infh.Command = cmd;
                        List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();

                        Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                        inf.idtrabajador = idTrabajador;
                        inf.activo = true;

                        List<Infonavit.Core.Infonavit> _lstPeriodoInfonavit = new List<Infonavit.Core.Infonavit>();
                     
                        cnx.Open();
                        lstInfonavit = infh.obtenerInfonavit(inf);
                        cnx.Close();
                        
                        if (lstInfonavit.Count != 0)
                        {
                            if (lstInfonavit[0].fecha <= finPeriodo)
                            {
                                cnx.Open();
                                _lstPeriodoInfonavit = infh.obtenerDiasInfonavit(inf);
                                cnx.Close();

                                Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
                                ch.Command = cmd;
                                Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                                concepto.idempresa = GLOBALES.IDEMPRESA;
                                concepto.periodo = diasPeriodo;

                                switch (lstInfonavit[0].descuento)
                                {
                                    case 1: concepto.noconcepto = 10; //CONCEPTO INFONAVIT PORCENTAJE
                                        break;
                                    case 2: concepto.noconcepto = 12; //CONCEPTO INFONAVIT PESOS
                                        break;
                                    case 3: concepto.noconcepto = 11; //CONCEPTO INFONAVIT VSMDF
                                        break;
                                }

                                if (_lstPeriodoInfonavit.Count == 2)
                                {
                                    if (_lstPeriodoInfonavit[0].fecha >= inicioPeriodo && _lstPeriodoInfonavit[0].fecha <= finPeriodo)
                                    {
                                        if (_lstPeriodoInfonavit[0].dias == diasPeriodo)
                                        {
                                            cnx.Open();
                                            formula = ch.obtenerFormula(concepto).ToString();
                                            cnx.Close();
                                        }

                                        if (_lstPeriodoInfonavit[0].dias < diasPeriodo)
                                        {
                                            if (_lstPeriodoInfonavit[0].descuento == _lstPeriodoInfonavit[1].descuento)
                                            {
                                                cnx.Open();
                                                formula = ch.obtenerFormula(concepto).ToString();
                                                cnx.Close();
                                                formula2 = formula;
                                                formula2 = formula2.Replace("ValorInfonavit", "ValorInfonavit2");
                                                formula2 = formula2.Replace("PeriodoInfonavit", "PeriodoInfonavit2");
                                                formula = string.Format("({0}) + ({1})", formula, formula2);
                                            }
                                            else
                                            {
                                                switch (_lstPeriodoInfonavit[0].descuento)
                                                {
                                                    case 1: concepto.noconcepto = 10;
                                                        break;
                                                    case 2: concepto.noconcepto = 12;
                                                        break;
                                                    case 3: concepto.noconcepto = 11;
                                                        break;
                                                }
                                                cnx.Open();
                                                formula = ch.obtenerFormula(concepto).ToString();
                                                cnx.Close();

                                                switch (_lstPeriodoInfonavit[1].descuento)
                                                {
                                                    case 1: concepto.noconcepto = 10;
                                                        break;
                                                    case 2: concepto.noconcepto = 12;
                                                        break;
                                                    case 3: concepto.noconcepto = 11;
                                                        break;
                                                }
                                                cnx.Open();
                                                formula2 = ch.obtenerFormula(concepto).ToString();
                                                cnx.Close();
                                                formula = string.Format("({0}) + ({1})", formula, formula2);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        cnx.Open();
                                        formula = ch.obtenerFormula(concepto).ToString();
                                        cnx.Close();
                                    }
                                }
                                else
                                {
                                    cnx.Open();
                                    formula = ch.obtenerFormula(concepto).ToString();
                                    cnx.Close();
                                }
                                #region COMENTARIOS INFONAVIT
                                //if (lstInfonavit[0].descuento == GLOBALES.dPORCENTAJE)
                                //{
                                //    concepto.noconcepto = 10; //INFONAVIT PORCENTAJE
                                    
                                //}


                                //if (lstInfonavit[0].descuento == GLOBALES.dVSMDF)
                                //{
                                //    concepto.noconcepto = 11; //INFONAVIT VSMDF
                                //    if (_lstPeriodoInfonavit.Count == 2)
                                //    {
                                //        if (_lstPeriodoInfonavit[0].fecha >= inicioPeriodo && _lstPeriodoInfonavit[0].fecha <= finPeriodo)
                                //        {
                                //            if (_lstPeriodoInfonavit[0].descuento == _lstPeriodoInfonavit[1].descuento)
                                //            {
                                //                cnx.Open();
                                //                formula = ch.obtenerFormula(concepto).ToString();
                                //                cnx.Close();
                                //                formula2 = formula;
                                //                formula2 = formula2.Replace("ValorInfonavit", "ValorInfonavit2");
                                //                formula2 = formula2.Replace("PeriodoInfonavit", "PeriodoInfonavit2");
                                //                formula = string.Format("({0}) + ({1})", formula, formula2);
                                //            }
                                //            else
                                //            {
                                //                switch (_lstPeriodoInfonavit[0].descuento)
                                //                {
                                //                    case 1: concepto.noconcepto = 10;
                                //                        break;
                                //                    case 2: concepto.noconcepto = 12;
                                //                        break;
                                //                    case 3: concepto.noconcepto = 11;
                                //                        break;
                                //                }
                                //                cnx.Open();
                                //                formula = ch.obtenerFormula(concepto).ToString();
                                //                cnx.Close();

                                //                switch (_lstPeriodoInfonavit[1].descuento)
                                //                {
                                //                    case 1: concepto.noconcepto = 10;
                                //                        break;
                                //                    case 2: concepto.noconcepto = 12;
                                //                        break;
                                //                    case 3: concepto.noconcepto = 11;
                                //                        break;
                                //                }
                                //                cnx.Open();
                                //                formula2 = ch.obtenerFormula(concepto).ToString();
                                //                cnx.Close();
                                //                formula = string.Format("({0}) + ({1})", formula, formula2);
                                //            }
                                //        }
                                //        else
                                //        {
                                //            cnx.Open();
                                //            formula = ch.obtenerFormula(concepto).ToString();
                                //            cnx.Close();
                                //        }
                                //    }
                                //    else
                                //    {
                                //        cnx.Open();
                                //        formula = ch.obtenerFormula(concepto).ToString();
                                //        cnx.Close();
                                //    }
                                //}


                                //if (lstInfonavit[0].descuento == GLOBALES.dPESOS)
                                //{
                                //    concepto.noconcepto = 12; //INFONAVIT FIJO
                                //    if (_lstPeriodoInfonavit.Count == 2)
                                //    {
                                //        if (_lstPeriodoInfonavit[0].fecha >= inicioPeriodo && _lstPeriodoInfonavit[0].fecha <= finPeriodo)
                                //        {
                                //            if (_lstPeriodoInfonavit[0].descuento == _lstPeriodoInfonavit[1].descuento)
                                //            {
                                //                cnx.Open();
                                //                formula = ch.obtenerFormula(concepto).ToString();
                                //                cnx.Close();
                                //                formula2 = formula;
                                //                formula2 = formula2.Replace("ValorInfonavit", "ValorInfonavit2");
                                //                formula2 = formula2.Replace("PeriodoInfonavit", "PeriodoInfonavit2");
                                //                formula = string.Format("({0}) + ({1})", formula, formula2);
                                //            }
                                //            else
                                //            {
                                //                switch (_lstPeriodoInfonavit[0].descuento)
                                //                {
                                //                    case 1: concepto.noconcepto = 10;
                                //                        break;
                                //                    case 2: concepto.noconcepto = 12;
                                //                        break;
                                //                    case 3: concepto.noconcepto = 11;
                                //                        break;
                                //                }
                                //                cnx.Open();
                                //                formula = ch.obtenerFormula(concepto).ToString();
                                //                cnx.Close();

                                //                switch (_lstPeriodoInfonavit[1].descuento)
                                //                {
                                //                    case 1: concepto.noconcepto = 10;
                                //                        break;
                                //                    case 2: concepto.noconcepto = 12;
                                //                        break;
                                //                    case 3: concepto.noconcepto = 11;
                                //                        break;
                                //                }
                                //                cnx.Open();
                                //                formula2 = ch.obtenerFormula(concepto).ToString();
                                //                cnx.Close();
                                //                formula = string.Format("({0}) + ({1})", formula, formula2);
                                //            }
                                //        }
                                //        else
                                //        {
                                //            cnx.Open();
                                //            formula = ch.obtenerFormula(concepto).ToString();
                                //            cnx.Close();
                                //        }
                                //    }
                                //    else
                                //    {
                                //        cnx.Open();
                                //        formula = ch.obtenerFormula(concepto).ToString();
                                //        cnx.Close();
                                //    }
                                //}
                                #endregion
                                return calcularFormula();
                            }
                            else
                                return 0;
                        }
                        else
                            return 0;

                    case "ValorInfonavit":
                        object valorInfonavit;
                        Infonavit.Core.InfonavitHelper vih = new Infonavit.Core.InfonavitHelper();
                        vih.Command = cmd;

                        Infonavit.Core.Infonavit infonavit = new Infonavit.Core.Infonavit();
                        infonavit.idtrabajador = idTrabajador;

                        cnx.Open();
                        valorInfonavit = vih.obtenerValorInfonavit(infonavit);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", valorInfonavit.ToString());
                        break;

                    case "ValorInfonavit2":
                        object valorInfonavit2;
                        Infonavit.Core.InfonavitHelper vih2 = new Infonavit.Core.InfonavitHelper();
                        vih2.Command = cmd;

                        cnx.Open();
                        valorInfonavit2 = vih2.obtenerValorInfonavit(idTrabajador);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", valorInfonavit2.ToString());
                        break;

                    case "DiasBimestre":
                        int diasBimestre = 0;
                        int mesBimestre = DateTime.Now.Month;
                        switch (mesBimestre)
                        {
                            case 1:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 1) + DateTime.DaysInMonth(DateTime.Now.Year, 2);
                                break;
                            case 2:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 1) + DateTime.DaysInMonth(DateTime.Now.Year, 2);
                                break;
                            case 3:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 3) + DateTime.DaysInMonth(DateTime.Now.Year, 4);
                                break;
                            case 4:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 3) + DateTime.DaysInMonth(DateTime.Now.Year, 4);
                                break;
                            case 5:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 5) + DateTime.DaysInMonth(DateTime.Now.Year, 6);
                                break;
                            case 6:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 5) + DateTime.DaysInMonth(DateTime.Now.Year, 6);
                                break;
                            case 7:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 7) + DateTime.DaysInMonth(DateTime.Now.Year, 8);
                                break;
                            case 8:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 7) + DateTime.DaysInMonth(DateTime.Now.Year, 8);
                                break;
                            case 9:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 9) + DateTime.DaysInMonth(DateTime.Now.Year, 10);
                                break;
                            case 10:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 9) + DateTime.DaysInMonth(DateTime.Now.Year, 10);
                                break;
                            case 11:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 11) + DateTime.DaysInMonth(DateTime.Now.Year, 12);
                                break;
                            case 12:
                                diasBimestre = DateTime.DaysInMonth(DateTime.Now.Year, 11) + DateTime.DaysInMonth(DateTime.Now.Year, 12);
                                break;
                        }

                        formula = formula.Replace("[" + variables[i] + "]", diasBimestre.ToString());
                        break;

                    case "DiasMes":
                        int diasMes = 0;
                        int mes = DateTime.Now.Month;
                        switch (mes)
                        {
                            case 1:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 1);
                                break;
                            case 2:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 2);
                                break;
                            case 3:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 3);
                                break;
                            case 4:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 4);
                                break;
                            case 5:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 5);
                                break;
                            case 6:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 6);
                                break;
                            case 7:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 7);
                                break;
                            case 8:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 8);
                                break;
                            case 9:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 9);
                                break;
                            case 10:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 10);
                                break;
                            case 11:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 11);
                                break;
                            case 12:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 12);
                                break;
                        }

                        formula = formula.Replace("[" + variables[i] + "]", (30.4).ToString());
                        break;

                    case "PeriodoInfonavit":
                        
                        int diasPI = 0;
                        Infonavit.Core.InfonavitHelper iph = new Infonavit.Core.InfonavitHelper();
                        iph.Command = cmd;

                        Infonavit.Core.Infonavit infP = new Infonavit.Core.Infonavit();
                        infP.idtrabajador = idTrabajador;

                        List<Infonavit.Core.Infonavit> lstPeriodoInfonavit = new List<Infonavit.Core.Infonavit>();

                        cnx.Open();
                        lstPeriodoInfonavit = iph.obtenerDiasInfonavit(infP);
                        cnx.Close();

                        //Empleados.Core.EmpleadosHelper epih = new Empleados.Core.EmpleadosHelper();
                        //epih.Command = cmd;

                        //cnx.Open();
                        //int idperiodoPI = (int)epih.obtenerIdPeriodo(idTrabajador);
                        //cnx.Close();

                        //Periodos.Core.PeriodosHelper ppih = new Periodos.Core.PeriodosHelper();
                        //ppih.Command = cmd;

                        //Periodos.Core.Periodos pPI = new Periodos.Core.Periodos();
                        //pPI.idperiodo = idperiodoPI;

                        //cnx.Open();
                        //int diasPI = (int)ppih.DiasDePago(pPI);
                        //cnx.Close();

                        //if (lstPeriodoInfonavit.Count == 1)
                        if (lstPeriodoInfonavit[0].fecha >= inicioPeriodo && lstPeriodoInfonavit[0].fecha <= finPeriodo)
                        {
                            int faltaDentroInfonavit = 0;
                            Faltas.Core.FaltasHelper fih = new Faltas.Core.FaltasHelper();
                            fih.Command = cmd;
                            Faltas.Core.Faltas fi = new Faltas.Core.Faltas();
                            fi.idempresa = GLOBALES.IDEMPRESA;
                            fi.idtrabajador = idTrabajador;
                            fi.fechainicio = inicioPeriodo;
                            fi.fechafin = finPeriodo;
                            List<Faltas.Core.Faltas> lstFaltasInf = new List<Faltas.Core.Faltas>();
                            cnx.Open();
                            lstFaltasInf = fih.obtenerFaltaTrabajador(fi);
                            cnx.Close();
                            if (lstFaltasInf.Count != 0)
                                for (int x = 0; x < lstFaltasInf.Count; x++)
                                {
                                    if (lstFaltasInf[x].fecha >= lstPeriodoInfonavit[0].fecha)
                                    {
                                        faltaDentroInfonavit += 1;
                                    }
                                }

                            formula = formula.Replace("[" + variables[i] + "]", (lstPeriodoInfonavit[0].dias - faltaDentroInfonavit).ToString());
                        }
                        else
                        {
                            CalculoFormula cf = new CalculoFormula(idTrabajador, inicioPeriodo, finPeriodo, "[DiasLaborados]-[Faltas]-[DiasIncapacidad]");
                            diasPI = int.Parse(cf.calcularFormula().ToString());
                            formula = formula.Replace("[" + variables[i] + "]", diasPI.ToString());
                        }
                             
                        //else if (lstPeriodoInfonavit.Count == 2)
                        //    if (lstPeriodoInfonavit[0].fecha >= inicioPeriodo && lstPeriodoInfonavit[0].fecha <= finPeriodo)
                        //        formula = formula.Replace("[" + variables[i] + "]", lstPeriodoInfonavit[0].dias.ToString());
                        //    else
                        //    {
                        //        CalculoFormula cf = new CalculoFormula(idTrabajador, inicioPeriodo, finPeriodo, "[DiasLaborados]-[Faltas]-[DiasIncapacidad]");
                        //        diasPI = int.Parse(cf.calcularFormula().ToString());
                        //        formula = formula.Replace("[" + variables[i] + "]", diasPI.ToString());
                        //    }
                            
                        break;
                    case "PeriodoInfonavit2":

                        int diasPI2 = 0;
                        Infonavit.Core.InfonavitHelper iph2 = new Infonavit.Core.InfonavitHelper();
                        iph2.Command = cmd;

                        Infonavit.Core.Infonavit infP2 = new Infonavit.Core.Infonavit();
                        infP2.idtrabajador = idTrabajador;

                        List<Infonavit.Core.Infonavit> lstPeriodoInfonavit2 = new List<Infonavit.Core.Infonavit>();

                        cnx.Open();
                        lstPeriodoInfonavit2 = iph2.obtenerDiasInfonavit(infP2);
                        cnx.Close();

                        Empleados.Core.EmpleadosHelper epih = new Empleados.Core.EmpleadosHelper();
                        epih.Command = cmd;

                        cnx.Open();
                        int idperiodoPI = (int)epih.obtenerIdPeriodo(idTrabajador);
                        cnx.Close();

                        Periodos.Core.PeriodosHelper ppih = new Periodos.Core.PeriodosHelper();
                        ppih.Command = cmd;

                        Periodos.Core.Periodos pPI = new Periodos.Core.Periodos();
                        pPI.idperiodo = idperiodoPI;

                        cnx.Open();
                        diasPI2 = (int)ppih.DiasDePago(pPI);
                        cnx.Close();

                        diasPI2 = diasPI2 - lstPeriodoInfonavit2[0].dias;

                        formula = formula.Replace("[" + variables[i] + "]", diasPI2.ToString());
                        
                        break;
                    case "SeguroInfonavit":

                        Infonavit.Core.InfonavitHelper infhSI = new Infonavit.Core.InfonavitHelper();
                        infhSI.Command = cmd;

                        Empleados.Core.EmpleadosHelper esih = new Empleados.Core.EmpleadosHelper();
                        esih.Command = cmd;

                        List<Infonavit.Core.Infonavit> lstInfonavitSI = new List<Infonavit.Core.Infonavit>();

                        Infonavit.Core.Infonavit infSI = new Infonavit.Core.Infonavit();
                        infSI.idtrabajador = idTrabajador;
                        infSI.activo = true;
                     
                        cnx.Open();
                        lstInfonavitSI = infhSI.obtenerInfonavit(infSI);
                        cnx.Close();

                        if (lstInfonavitSI.Count != 0)
                        {
                            if (lstInfonavitSI[0].fecha <= finPeriodo)
                            {
                                cnx.Open();
                                int idperiodoSI = (int)esih.obtenerIdPeriodo(idTrabajador);
                                cnx.Close();

                                Periodos.Core.PeriodosHelper psih = new Periodos.Core.PeriodosHelper();
                                psih.Command = cmd;

                                Periodos.Core.Periodos psi = new Periodos.Core.Periodos();
                                psi.idperiodo = idperiodoSI;

                                cnx.Open();
                                int diasSI = (int)psih.DiasDePago(psi);
                                cnx.Close();

                                
                                if (diasSI == 7)
                                    formula = formula.Replace("[" + variables[i] + "]", (1.5).ToString());
                                else
                                    formula = formula.Replace("[" + variables[i] + "]", (3).ToString());
                                
                            }
                            else
                                return 0;
                        }
                        else
                            return 0;

                        break;
                    case "CuotaFija":

                        Infonavit.Core.InfonavitHelper cuotaFija = new Infonavit.Core.InfonavitHelper();
                        cuotaFija.Command = cmd;

                        Infonavit.Core.Infonavit valor = new Infonavit.Core.Infonavit();
                        valor.idtrabajador = idTrabajador;

                        cnx.Open();
                        object cuota = cuotaFija.obtenerValorInfonavit(valor);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", cuota.ToString());
 
                        break;

                    case "ISR ASE":
                        decimal excedente = 0, ImpMarginal = 0, isr = 0;
                        int idperiodoISR = 0, diasISR = 0;
                        List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
                        TablaIsr.Core.IsrHelper isrh = new TablaIsr.Core.IsrHelper();
                        isrh.Command = cmd;

                        decimal percepciones = lstPercepciones.Where(e => e.idtrabajador == idTrabajador && e.tipoconcepto == "P").Sum(e => e.cantidad);
                        if (percepciones != 0)
                        {
                            decimal baseGravableIsr = lstPercepciones.Where(e => e.idtrabajador == idTrabajador).Sum(e => e.gravado);

                            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                            eh.Command = cmd;

                            cnx.Open();
                            idperiodoISR = (int)eh.obtenerIdPeriodo(idTrabajador);
                            cnx.Close();

                            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                            ph.Command = cmd;

                            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                            p.idperiodo = idperiodoISR;

                            cnx.Open();
                            diasISR = (int)ph.DiasDePago(p);
                            cnx.Close();

                            TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
                            _isr.inferior = (baseGravableIsr / diasISR) * decimal.Parse((30.4).ToString());

                            cnx.Open();
                            lstIsr = isrh.isrCorrespondiente(_isr);
                            cnx.Close();

                            excedente = ((baseGravableIsr / diasISR) * decimal.Parse((30.4).ToString())) - lstIsr[0].inferior;
                            ImpMarginal = excedente * (lstIsr[0].porcentaje / 100);
                            isr = ImpMarginal + lstIsr[0].cuota;

                            return (isr / decimal.Parse((30.4).ToString())) * diasISR;
                        }
                        else
                        {
                            return 0;
                        }

                    case "Subsidio Acreditado":
                        decimal percepcionSubsidio = lstPercepciones.Where(e => e.idtrabajador == idTrabajador && e.tipoconcepto == "P").Sum(e => e.cantidad);
                        int idperiodoSubsidio = 0, diasSubsidio = 0;

                        if (percepcionSubsidio != 0)
                        {
                            decimal baseGravableSubsidio = lstPercepciones.Where(e => e.idtrabajador == idTrabajador).Sum(e => e.gravado);

                            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                            eh.Command = cmd;

                            cnx.Open();
                            idperiodoSubsidio = (int)eh.obtenerIdPeriodo(idTrabajador);
                            cnx.Close();

                            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                            ph.Command = cmd;

                            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                            p.idperiodo = idperiodoSubsidio;

                            cnx.Open();
                            diasSubsidio = (int)ph.DiasDePago(p);
                            cnx.Close();

                            TablaSubsidio.Core.SubsidioHelper ts = new TablaSubsidio.Core.SubsidioHelper();
                            ts.Command = cmd;
                            TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
                            subsidio.desde = (baseGravableSubsidio / diasSubsidio) * decimal.Parse((30.4).ToString());

                            decimal cantidad = 0;
                            cnx.Open();
                            cantidad = decimal.Parse(ts.obtenerCantidadSubsidio(subsidio).ToString());
                            cnx.Close();

                            return (cantidad / decimal.Parse((30.4).ToString())) * diasSubsidio;
                        }
                        else
                        {
                            return 0;
                        }

                    case "ISR":
                        int noConceptoISR = 0, noConceptoSubsidio = 0, existeConceptoISR = 0, existeConceptoSubisdio = 0;
                        decimal percepcionISR = lstPercepciones.Where(e => e.idtrabajador == idTrabajador && e.tipoconcepto == "P").Sum(e => e.cantidad);

                        if (percepcionISR != 0)
                        {
                            CalculoNomina.Core.NominaHelper cnh = new CalculoNomina.Core.NominaHelper();
                            cnh.Command = cmd;

                            Conceptos.Core.ConceptosHelper cisrh = new Conceptos.Core.ConceptosHelper();
                            cisrh.Command = cmd;
                            Conceptos.Core.Conceptos cisr = new Conceptos.Core.Conceptos();
                            cisr.formula = "[ISR ASE]";
                            cisr.idempresa = GLOBALES.IDEMPRESA;

                            cnx.Open();
                            noConceptoISR = (int)cisrh.obtenerNoConcepto(cisr);
                            cnx.Close();

                            CalculoNomina.Core.tmpPagoNomina cpnIsr = new CalculoNomina.Core.tmpPagoNomina();
                            cpnIsr.idtrabajador = idTrabajador;
                            cpnIsr.idempresa = GLOBALES.IDEMPRESA;
                            cpnIsr.fechainicio = inicioPeriodo;
                            cpnIsr.fechafin = finPeriodo;
                            cpnIsr.noconcepto = noConceptoISR;

                            cnx.Open();
                            existeConceptoISR = (int)cnh.existeConcepto(cpnIsr);
                            cnx.Close();

                            
                            Conceptos.Core.ConceptosHelper csubsidioh = new Conceptos.Core.ConceptosHelper();
                            csubsidioh.Command = cmd;
                            Conceptos.Core.Conceptos csubsidio = new Conceptos.Core.Conceptos();
                            csubsidio.formula = "[Subsidio Acreditado]";
                            csubsidio.idempresa = GLOBALES.IDEMPRESA;

                            cnx.Open();
                            noConceptoSubsidio = (int)csubsidioh.obtenerNoConcepto(csubsidio);
                            cnx.Close();

                            CalculoNomina.Core.tmpPagoNomina cpnSubsidio = new CalculoNomina.Core.tmpPagoNomina();
                            cpnSubsidio.idtrabajador = idTrabajador;
                            cpnSubsidio.idempresa = GLOBALES.IDEMPRESA;
                            cpnSubsidio.fechainicio = inicioPeriodo;
                            cpnSubsidio.fechafin = finPeriodo;
                            cpnSubsidio.noconcepto = noConceptoSubsidio;

                            cnx.Open();
                            existeConceptoSubisdio = (int)cnh.existeConcepto(cpnSubsidio);
                            cnx.Close();

                            
                            if (existeConceptoISR != 0 && existeConceptoSubisdio != 0)
                            {
                                cnx.Open();
                                decimal cantidadIsr = decimal.Parse(cnh.obtenerImporteConcepto(cpnIsr).ToString());
                                decimal cantidadSubsidio = decimal.Parse(cnh.obtenerImporteConcepto(cpnSubsidio).ToString());
                                cnx.Close();

                                Empleados.Core.EmpleadosHelper eih = new Empleados.Core.EmpleadosHelper();
                                eih.Command = cmd;

                                cnx.Open();
                                int idperiodoIsr = (int)eih.obtenerIdPeriodo(idTrabajador);
                                cnx.Close();

                                Periodos.Core.PeriodosHelper pih = new Periodos.Core.PeriodosHelper();
                                pih.Command = cmd;

                                Periodos.Core.Periodos pi = new Periodos.Core.Periodos();
                                pi.idperiodo = idperiodoIsr;

                                cnx.Open();
                                int diasIsr = (int)pih.DiasDePago(pi);
                                cnx.Close();

                                if (cantidadSubsidio > cantidadIsr)
                                {
                                    formula = formula.Replace("[" + variables[i] + "]", (0).ToString());
                                }
                                else
                                {
                                    formula = formula.Replace("[" + variables[i] + "]", (cantidadIsr - cantidadSubsidio).ToString());
                                }
                            }
                            else
                                formula = formula.Replace("[" + variables[i] + "]", (0).ToString());
                        }
                        else
                            formula = formula.Replace("[" + variables[i] + "]", (0).ToString());
                        break;

                    case "Subsidio":
                        decimal percepcionSubs = lstPercepciones.Where(e => e.idtrabajador == idTrabajador && e.tipoconcepto == "P").Sum(e => e.cantidad);
                        int noConceptoSub = 0, noConceptoISRSAE = 0, existeConceptoSub = 0, existeConceptoISRSAE = 0;

                        if (percepcionSubs != 0)
                        {
                            CalculoNomina.Core.NominaHelper cnh = new CalculoNomina.Core.NominaHelper();
                            cnh.Command = cmd;

                            Conceptos.Core.ConceptosHelper cisrh = new Conceptos.Core.ConceptosHelper();
                            cisrh.Command = cmd;
                            Conceptos.Core.Conceptos cisr = new Conceptos.Core.Conceptos();
                            cisr.formula = "[ISR ASE]";
                            cisr.idempresa = GLOBALES.IDEMPRESA;

                            cnx.Open();
                            noConceptoISRSAE = (int)cisrh.obtenerNoConcepto(cisr);
                            cnx.Close();

                            CalculoNomina.Core.tmpPagoNomina cpnIsr = new CalculoNomina.Core.tmpPagoNomina();
                            cpnIsr.idtrabajador = idTrabajador;
                            cpnIsr.idempresa = GLOBALES.IDEMPRESA;
                            cpnIsr.fechainicio = inicioPeriodo;
                            cpnIsr.fechafin = finPeriodo;
                            cpnIsr.noconcepto = noConceptoISRSAE;

                            cnx.Open();
                            existeConceptoISRSAE = (int)cnh.existeConcepto(cpnIsr);
                            cnx.Close();


                            Conceptos.Core.ConceptosHelper csubsidioh = new Conceptos.Core.ConceptosHelper();
                            csubsidioh.Command = cmd;
                            Conceptos.Core.Conceptos csubsidio = new Conceptos.Core.Conceptos();
                            csubsidio.formula = "[Subsidio Acreditado]";
                            csubsidio.idempresa = GLOBALES.IDEMPRESA;

                            cnx.Open();
                            noConceptoSub = (int)csubsidioh.obtenerNoConcepto(csubsidio);
                            cnx.Close();

                            CalculoNomina.Core.tmpPagoNomina cpnSubsidio = new CalculoNomina.Core.tmpPagoNomina();
                            cpnSubsidio.idtrabajador = idTrabajador;
                            cpnSubsidio.idempresa = GLOBALES.IDEMPRESA;
                            cpnSubsidio.fechainicio = inicioPeriodo;
                            cpnSubsidio.fechafin = finPeriodo;
                            cpnSubsidio.noconcepto = noConceptoSub;

                            cnx.Open();
                            existeConceptoSub = (int)cnh.existeConcepto(cpnSubsidio);
                            cnx.Close();

                            if (existeConceptoSub != 0 && existeConceptoISRSAE != 0)
                            {
                                cnx.Open();
                                double cantidadIsr = double.Parse(cnh.obtenerImporteConcepto(cpnIsr).ToString());
                                double cantidadSubsidio = double.Parse(cnh.obtenerImporteConcepto(cpnSubsidio).ToString());
                                cnx.Close();

                                Empleados.Core.EmpleadosHelper eih = new Empleados.Core.EmpleadosHelper();
                                eih.Command = cmd;

                                cnx.Open();
                                int idperiodoIsr = (int)eih.obtenerIdPeriodo(idTrabajador);
                                cnx.Close();

                                Periodos.Core.PeriodosHelper pih = new Periodos.Core.PeriodosHelper();
                                pih.Command = cmd;

                                Periodos.Core.Periodos pi = new Periodos.Core.Periodos();
                                pi.idperiodo = idperiodoIsr;

                                cnx.Open();
                                int diasIsr = (int)pih.DiasDePago(pi);
                                cnx.Close();

                                if (cantidadSubsidio > cantidadIsr)
                                {
                                    formula = formula.Replace("[" + variables[i] + "]", (cantidadSubsidio - cantidadIsr).ToString());
                                }
                                else
                                {
                                    formula = formula.Replace("[" + variables[i] + "]", (0).ToString());
                                }
                            }
                        }
                       
                        break;

                    case "IMSS":
                        int vsmdf; //idsalario;
                        decimal porcentajeImss, excedenteVsmdf, sm, sdiTrabajador;
                        
                        Configuracion.Core.ConfiguracionHelper configh = new Configuracion.Core.ConfiguracionHelper();
                        configh.Command = cmd;

                        Imss.Core.ImssHelper imssh = new Imss.Core.ImssHelper();
                        imssh.Command = cmd;

                        Imss.Core.Imss imss = new Imss.Core.Imss();
                        imss.calculo = true;

                        Empleados.Core.EmpleadosHelper empleadosImssHelper = new Empleados.Core.EmpleadosHelper();
                        empleadosImssHelper.Command = cmd;

                        Empleados.Core.Empleados empleadoImss = new Empleados.Core.Empleados();
                        empleadoImss.idtrabajador = idTrabajador;

                        Salario.Core.SalariosHelper salarioimssh = new Salario.Core.SalariosHelper();
                        salarioimssh.Command = cmd;
                        //Salario.Core.Salarios salario = new Salario.Core.Salarios();
                        
                        cnx.Open();
                        vsmdf = int.Parse(configh.obtenerValorConfiguracion("VSMDF").ToString());
                        porcentajeImss = imssh.CuotaObreroPatronal(imss);
                        excedenteVsmdf = imssh.ExcedenteVSM(5);
                        //idsalario = int.Parse(empleadosHelper.obtenerIdSalarioMinimo(lstConceptosDeducciones[i].idtrabajador).ToString());
                        //salario.idsalario = idsalario;
                        sm = decimal.Parse(salarioimssh.obtenerSalarioValor().ToString());
                        sdiTrabajador = decimal.Parse(empleadosHelper.obtenerSalarioDiarioIntegrado(empleadoImss).ToString());
                        cnx.Close();

                        string formulaDiasAPagar = "[DiasLaborados]-[Faltas]-[DiasIncapacidad]";
                        CalculoFormula cfImss = new CalculoFormula(idTrabajador, inicioPeriodo, finPeriodo, formulaDiasAPagar);
                        int diasAPagar = int.Parse(cfImss.calcularFormula().ToString());

                        decimal tresVSMG = vsmdf * sm;
                        decimal excedenteImss = 0;
                        decimal valorImss = (sdiTrabajador * (porcentajeImss / 100)) * diasAPagar;
                        decimal totalImss = 0;
                        if (sdiTrabajador > tresVSMG)
                        {
                            excedenteImss = (sdiTrabajador - tresVSMG) * (excedenteVsmdf / 100) * diasAPagar;
                            totalImss = valorImss + excedenteImss;
                        }
                        else
                            totalImss = valorImss;

                        formula = formula.Replace("[" + variables[i] + "]", totalImss.ToString());
                        
                        break;
                    case "GastosMedicos":
                        decimal porcentajeGM;
                        Imss.Core.ImssHelper igmh = new Imss.Core.ImssHelper();
                        igmh.Command = cmd;
                        cnx.Open();
                        porcentajeGM = igmh.ExcedenteVSM(1);
                        cnx.Close();
                        formula = formula.Replace("[" + variables[i] + "]", porcentajeGM.ToString());
                        break;

                    case "PrestacionDinero":
                        decimal porcentajePD;
                        Imss.Core.ImssHelper ipdh = new Imss.Core.ImssHelper();
                        ipdh.Command = cmd;
                        cnx.Open();
                        porcentajePD = ipdh.ExcedenteVSM(2);
                        cnx.Close();
                        formula = formula.Replace("[" + variables[i] + "]", porcentajePD.ToString());
                        break;

                    case "InvalidezVida": 
                        decimal porcentajeIV;
                        Imss.Core.ImssHelper iivh = new Imss.Core.ImssHelper();
                        iivh.Command = cmd;
                        cnx.Open();
                        porcentajeIV = iivh.ExcedenteVSM(3);
                        cnx.Close();
                        formula = formula.Replace("[" + variables[i] + "]", porcentajeIV.ToString());
                        break;

                    case "CesantiaVejez": 
                        decimal porcentajeCV;
                        Imss.Core.ImssHelper icvh = new Imss.Core.ImssHelper();
                        icvh.Command = cmd;
                        cnx.Open();
                        porcentajeCV = icvh.ExcedenteVSM(6);
                        cnx.Close();
                        formula = formula.Replace("[" + variables[i] + "]", porcentajeCV.ToString());
                        break;
                }
            }
            cnx.Dispose();
            try
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                resultado = parser.Parse(formula.ToString());
                return resultado;
            }
            catch (Exception error)
            {
                string err = "";
                err = "IdTrabajador: " + idTrabajador.ToString() + " " + error.Message.ToString();
                return err;
            }
        }
    }
}
