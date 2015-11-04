using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominas
{
    public class FormulasValores
    {
        private List<CalculoNomina.Core.Nomina> datosNomina;
        private List<Empleados.Core.Empleados> datosEmpleados;
        private DateTime fechainicio, fechafin;
        private string formula;
        private int diasDerechoV;
        
        public FormulasValores(List<CalculoNomina.Core.Nomina> _datosNomina, DateTime _inicio, DateTime _fin)
        {
            datosNomina = _datosNomina;
            fechainicio = _inicio;
            fechafin = _fin;
        }

        public FormulasValores(string _formula, List<Empleados.Core.Empleados> _datosEmpleados, DateTime _inicio, DateTime _fin, int _diasDerechoV = 0)
        {
            datosEmpleados = _datosEmpleados;
            fechainicio = _inicio;
            fechafin = _fin;
            formula = _formula;
            diasDerechoV = _diasDerechoV;
        }

        public object calcularFormula()
        {
            return evaluacionFormula(datosNomina[0].formula);
        }

        public object calcularFormulaExento()
        {
            return evaluacionFormula(datosNomina[0].formulaexento);
        }

        public object calcularFormulaVacaciones()
        {
            return evaluacionFormulaVacaciones(formula);
        }

        public object calcularFormulaVacacionesExento()
        {
            return evaluacionFormulaVacaciones(formula);
        }
        
        private object evaluacionFormula(string formula)
        {
            if (formula.Equals("0"))
                return 0;

            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;

            List<string> variables = new List<string>();
            variables = GLOBALES.EXTRAEVARIABLES(formula, "[", "]");

            object resultado;

            for (int i = 0; i < variables.Count; i++)
            {
                switch (variables[i])
                {
                    case "SueldoDiario":
                        formula = formula.Replace("[" + variables[i] + "]", datosNomina[0].sd.ToString());
                        break;

                    case "SBC":
                        formula = formula.Replace("[" + variables[i] + "]", datosNomina[0].sdi.ToString());
                        break;

                    case "DiasDerechoV":
                        object dias = null;
                        if (diasDerechoV == 0)
                        {
                            Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                            vh.Command = cmd;
                            Vacaciones.Core.DiasDerecho diasDerecho = new Vacaciones.Core.DiasDerecho();
                            diasDerecho.anio = datosNomina[0].antiguedadmod;

                            cnx.Open();
                            dias = vh.diasDerecho(diasDerecho).ToString();
                            cnx.Close();
                        }
                        else
                            dias = diasDerechoV;
                        formula = formula.Replace("[" + variables[i] + "]", dias.ToString());
                        break;

                    case "SalarioMinimo":
                        formula = formula.Replace("[" + variables[i] + "]", datosNomina[0].salariominimo.ToString());
                        break;

                    case "Faltas":
                        object noFaltas;
                        Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                        fh.Command = cmd;
                        Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                        falta.idtrabajador = datosNomina[0].idtrabajador;
                        falta.fechainicio = fechainicio;
                        falta.fechafin = fechafin;

                        cnx.Open();
                        noFaltas = fh.existeFalta(falta);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", noFaltas.ToString());
                        break;

                    case "DiasLaborados":
                        formula = formula.Replace("[" + variables[i] + "]", datosNomina[0].dias.ToString());
                        break;

                    case "DiasIncapacidad":
                        object diasIncapacidad;
                        Incapacidad.Core.IncapacidadHelper ih = new Incapacidad.Core.IncapacidadHelper();
                        ih.Command = cmd;
                        Incapacidad.Core.Incapacidades inc = new Incapacidad.Core.Incapacidades();
                        inc.idtrabajador = datosNomina[0].idtrabajador;
                        inc.fechainicio = fechainicio;
                        inc.fechafin = fechafin;

                        cnx.Open();
                        diasIncapacidad = ih.existeIncapacidad(inc);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", diasIncapacidad.ToString());
                        break;

                    case "ISR":
                        double excedente = 0, ImpMarginal = 0, isr = 0;
                        List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
                        TablaIsr.Core.IsrHelper isrh = new TablaIsr.Core.IsrHelper();
                        isrh.Command = cmd;

                        TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
                        _isr.periodo = datosNomina[0].dias;
                        _isr.inferior = datosNomina[0].sd * (double)datosNomina[0].dias;
                       
                        cnx.Open();
                        lstIsr = isrh.isrCorrespondiente(_isr);
                        cnx.Close();
                        
                        for (int j = 0; j < lstIsr.Count; j++)
                        {
                            excedente = (datosNomina[0].sd * (double)datosNomina[0].dias) - lstIsr[j].inferior;
                            ImpMarginal = excedente * (lstIsr[j].porcentaje / 100);
                            isr = ImpMarginal + lstIsr[j].cuota;
                        }
                        return isr;

                    case "Infonavit":
                        string formulaInfonavit = "";
                        List<string> variablesInfonavit = new List<string>();

                        Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                        infh.Command = cmd;
                        List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();

                        Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                        inf.idtrabajador = datosNomina[0].idtrabajador;
                        inf.activo = true;

                        cnx.Open();
                        lstInfonavit = infh.obtenerInfonavit(inf);
                        cnx.Close();

                        if (lstInfonavit.Count != 0)
                        {
                            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
                            ch.Command = cmd;
                            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                            concepto.idempresa = GLOBALES.IDEMPRESA;

                            if (lstInfonavit[0].descuento == GLOBALES.dPORCENTAJE)
                                concepto.noconcepto = 10; //INFONAVIT PORCENTAJE

                            if (lstInfonavit[0].descuento == GLOBALES.dVSMDF)
                                concepto.noconcepto = 11; //INFONAVIT VSMDF

                            if (lstInfonavit[0].descuento == GLOBALES.dPESOS)
                                concepto.noconcepto = 12; //INFONAVIT FIJO

                            cnx.Open();
                            formulaInfonavit = ch.obtenerFormula(concepto).ToString();
                            cnx.Close();

                            return evaluacionFormula(formulaInfonavit);
                        }
                        else
                            return 0;                        

                    case "ValorInfonavit":
                        object valorInfonavit;
                        Infonavit.Core.InfonavitHelper vih = new Infonavit.Core.InfonavitHelper();
                        vih.Command = cmd;
                        
                        Infonavit.Core.Infonavit infonavit = new Infonavit.Core.Infonavit();
                        infonavit.idtrabajador = datosNomina[0].idtrabajador;
                        
                        cnx.Open();
                        valorInfonavit = vih.obtenerValorInfonavit(infonavit);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", valorInfonavit.ToString());
                        break;
                        
                    case "DiasMes":
                        int diasMes = 0;
                        int mes = DateTime.Now.Month;
                        switch (mes)
                        {
                            case 1:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 1) + DateTime.DaysInMonth(DateTime.Now.Year, 2);
                                break;
                            case 2:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 1) + DateTime.DaysInMonth(DateTime.Now.Year, 2);
                                break;
                            case 3:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 3) + DateTime.DaysInMonth(DateTime.Now.Year, 4);
                                break;
                            case 4:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 3) + DateTime.DaysInMonth(DateTime.Now.Year, 4);
                                break;
                            case 5:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 5) + DateTime.DaysInMonth(DateTime.Now.Year, 6);
                                break;
                            case 6:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 5) + DateTime.DaysInMonth(DateTime.Now.Year, 6);
                                break;
                            case 7:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 7) + DateTime.DaysInMonth(DateTime.Now.Year, 8);
                                break;
                            case 8:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 7) + DateTime.DaysInMonth(DateTime.Now.Year, 8);
                                break;
                            case 9:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 9) + DateTime.DaysInMonth(DateTime.Now.Year, 10);
                                break;
                            case 10:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 9) + DateTime.DaysInMonth(DateTime.Now.Year, 10);
                                break;
                            case 11:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 11) + DateTime.DaysInMonth(DateTime.Now.Year, 12);
                                break;
                            case 12:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 11) + DateTime.DaysInMonth(DateTime.Now.Year, 12);
                                break;
                        }

                        formula = formula.Replace("[" + variables[i] + "]", diasMes.ToString());
                        break;
                    case "Vacaciones":
                        Vacaciones.Core.VacacionesHelper pvh = new Vacaciones.Core.VacacionesHelper();
                        pvh.Command = cmd;
                        Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
                        vacacion.idtrabajador = datosNomina[0].idtrabajador;
                        vacacion.inicio = fechainicio;
                        vacacion.fin = fechafin;
                        double pagovacaciones = 0;
                        cnx.Open();
                        pagovacaciones = double.Parse(pvh.vacacionesPagadas(vacacion).ToString());
                        cnx.Close();
                        cnx.Dispose();

                        formula = formula.Replace("[" + variables[i] + "]", pagovacaciones.ToString());
                        break;
                    case "Subsidio":
                        TablaSubsidio.Core.SubsidioHelper ts = new TablaSubsidio.Core.SubsidioHelper();
                        ts.Command = cmd;
                        TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
                        subsidio.anio = fechainicio.Year;
                        subsidio.periodo = datosNomina[0].dias;
                        subsidio.desde = (datosNomina[0].sd * datosNomina[0].dias);
                        double cantidad = 0;
                        cnx.Open();
                        cantidad = double.Parse(ts.obtenerCantidadSubsidio(subsidio).ToString());
                        cnx.Close();
                        cnx.Dispose();

                        formula = formula.Replace("[" + variables[i] + "]", cantidad.ToString());
                        break;
                }
            }
            cnx.Dispose();
            MathParserTK.MathParser parser = new MathParserTK.MathParser();
            resultado = parser.Parse(formula.ToString());
            return resultado;
        }

        private object evaluacionFormulaVacaciones(string formula)
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;

            List<string> variables = new List<string>();
            variables = GLOBALES.EXTRAEVARIABLES(formula, "[", "]");

            object resultado;

            for (int i = 0; i < variables.Count; i++)
            {
                switch (variables[i])
                {
                    case "SueldoDiario":
                        formula = formula.Replace("[" + variables[i] + "]", datosEmpleados[0].sd.ToString());
                        break;

                    case "SBC":
                        formula = formula.Replace("[" + variables[i] + "]", datosEmpleados[0].sdi.ToString());
                        break;

                    case "DiasDerechoV":
                        object dias = null;
                        if (diasDerechoV == 0)
                        {
                            Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                            vh.Command = cmd;
                            Vacaciones.Core.DiasDerecho diasDerecho = new Vacaciones.Core.DiasDerecho();
                            diasDerecho.anio = datosEmpleados[0].antiguedadmod;

                            cnx.Open();
                            dias = vh.diasDerecho(diasDerecho).ToString();
                            cnx.Close();
                        }
                        else
                            dias = diasDerechoV;

                        formula = formula.Replace("[" + variables[i] + "]", dias.ToString());
                        break;

                    case "SalarioMinimo":
                        object salarioValor;
                        Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
                        sh.Command = cmd;
                        Salario.Core.Salarios salario = new Salario.Core.Salarios();
                        salario.idsalario = datosEmpleados[0].idsalario;
                        cnx.Open();
                        salarioValor = sh.obtenerSalarioValor(salario);
                        cnx.Close();
                        formula = formula.Replace("[" + variables[i] + "]", salarioValor.ToString());
                        break;

                    case "Faltas":
                        object noFaltas;
                        Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                        fh.Command = cmd;
                        Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                        falta.idtrabajador = datosEmpleados[0].idtrabajador;
                        falta.fechainicio = fechainicio;
                        falta.fechafin = fechafin;

                        cnx.Open();
                        noFaltas = fh.existeFalta(falta);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", noFaltas.ToString());
                        break;

                    case "DiasLaborados":
                        object diasLaborados;
                        Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                        ph.Command = cmd;
                        Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
                        periodo.idperiodo = datosEmpleados[0].idperiodo;
                        cnx.Open();
                        diasLaborados = ph.DiasDePago(periodo);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", diasLaborados.ToString());
                        break;

                    case "DiasIncapacidad":
                        object diasIncapacidad;
                        Incapacidad.Core.IncapacidadHelper ih = new Incapacidad.Core.IncapacidadHelper();
                        ih.Command = cmd;
                        Incapacidad.Core.Incapacidades inc = new Incapacidad.Core.Incapacidades();
                        inc.idtrabajador = datosEmpleados[0].idtrabajador;
                        inc.fechainicio = fechainicio;
                        inc.fechafin = fechafin;

                        cnx.Open();
                        diasIncapacidad = ih.existeIncapacidad(inc);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", diasIncapacidad.ToString());
                        break;

                    case "ISR":
                        object diasLaboradosIsr;
                        Periodos.Core.PeriodosHelper phi = new Periodos.Core.PeriodosHelper();
                        phi.Command = cmd;
                        Periodos.Core.Periodos periodoIsr = new Periodos.Core.Periodos();
                        periodoIsr.idperiodo = datosEmpleados[0].idperiodo;
                        cnx.Open();
                        diasLaboradosIsr = phi.DiasDePago(periodoIsr);
                        cnx.Close();

                        double excedente = 0, ImpMarginal = 0, isr = 0;
                        List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
                        TablaIsr.Core.IsrHelper isrh = new TablaIsr.Core.IsrHelper();
                        isrh.Command = cmd;

                        TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
                        _isr.periodo = (int)diasLaboradosIsr;
                        _isr.inferior = datosEmpleados[0].sd * (double)diasLaboradosIsr;

                        cnx.Open();
                        lstIsr = isrh.isrCorrespondiente(_isr);
                        cnx.Close();

                        for (int j = 0; j < lstIsr.Count; j++)
                        {
                            excedente = (datosNomina[0].sd * (double)datosNomina[0].dias) - lstIsr[j].inferior;
                            ImpMarginal = excedente * (lstIsr[j].porcentaje / 100);
                            isr = ImpMarginal + lstIsr[j].cuota;
                        }
                        return isr;

                    case "Infonavit":
                        string formulaInfonavit = "";
                        List<string> variablesInfonavit = new List<string>();

                        Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                        infh.Command = cmd;
                        List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();

                        Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                        inf.idtrabajador = datosNomina[0].idtrabajador;

                        cnx.Open();
                        lstInfonavit = infh.obtenerInfonavit(inf);
                        cnx.Close();

                        Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
                        ch.Command = cmd;
                        Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                        concepto.idempresa = GLOBALES.IDEMPRESA;

                        if (lstInfonavit[0].descuento == GLOBALES.dPORCENTAJE)
                            concepto.noconcepto = 10; //INFONAVIT PORCENTAJE

                        if (lstInfonavit[0].descuento == GLOBALES.dVSMDF)
                            concepto.noconcepto = 11; //INFONAVIT VSMDF

                        if (lstInfonavit[0].descuento == GLOBALES.dPESOS)
                            concepto.noconcepto = 12; //INFONAVIT FIJO

                        cnx.Open();
                        formulaInfonavit = ch.obtenerFormula(concepto).ToString();
                        cnx.Close();

                        return evaluacionFormula(formulaInfonavit);

                    case "ValorInfonavit":
                        object valorInfonavit;
                        Infonavit.Core.InfonavitHelper vih = new Infonavit.Core.InfonavitHelper();
                        vih.Command = cmd;

                        Infonavit.Core.Infonavit infonavit = new Infonavit.Core.Infonavit();
                        infonavit.idtrabajador = datosNomina[0].idtrabajador;

                        cnx.Open();
                        valorInfonavit = vih.obtenerValorInfonavit(infonavit);
                        cnx.Close();

                        formula = formula.Replace("[" + variables[i] + "]", valorInfonavit.ToString());
                        break;

                    case "DiasMes":
                        int diasMes = 0;
                        int mes = DateTime.Now.Month;
                        switch (mes)
                        {
                            case 1:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 1) + DateTime.DaysInMonth(DateTime.Now.Year, 2);
                                break;
                            case 2:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 1) + DateTime.DaysInMonth(DateTime.Now.Year, 2);
                                break;
                            case 3:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 3) + DateTime.DaysInMonth(DateTime.Now.Year, 4);
                                break;
                            case 4:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 3) + DateTime.DaysInMonth(DateTime.Now.Year, 4);
                                break;
                            case 5:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 5) + DateTime.DaysInMonth(DateTime.Now.Year, 6);
                                break;
                            case 6:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 5) + DateTime.DaysInMonth(DateTime.Now.Year, 6);
                                break;
                            case 7:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 7) + DateTime.DaysInMonth(DateTime.Now.Year, 8);
                                break;
                            case 8:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 7) + DateTime.DaysInMonth(DateTime.Now.Year, 8);
                                break;
                            case 9:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 9) + DateTime.DaysInMonth(DateTime.Now.Year, 10);
                                break;
                            case 10:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 9) + DateTime.DaysInMonth(DateTime.Now.Year, 10);
                                break;
                            case 11:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 11) + DateTime.DaysInMonth(DateTime.Now.Year, 12);
                                break;
                            case 12:
                                diasMes = DateTime.DaysInMonth(DateTime.Now.Year, 11) + DateTime.DaysInMonth(DateTime.Now.Year, 12);
                                break;
                        }

                        formula = formula.Replace("[" + variables[i] + "]", diasMes.ToString());
                        break;
                }
            }
            cnx.Dispose();
            MathParserTK.MathParser parser = new MathParserTK.MathParser();
            resultado = parser.Parse(formula.ToString());
            return resultado;
        }
    }
}
