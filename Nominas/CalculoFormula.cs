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
                        formula = formula.Replace("[" + variables[i] + "]", vh.pagoVacacionesPrima(vp).ToString());
                        cnx.Close();

                        break;

                    case "SalarioMinimo":

                        Empleados.Core.EmpleadosHelper empsh = new Empleados.Core.EmpleadosHelper();
                        empsh.Command = cmd;

                        Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
                        sh.Command = cmd;
                        
                        cnx.Open();
                        int idsalario = (int)empsh.obtenerIdSalarioMinimo(idTrabajador);
                        cnx.Close();

                        Salario.Core.Salarios salario = new Salario.Core.Salarios();
                        salario.idsalario = idsalario;

                        cnx.Open();
                        formula = formula.Replace("[" + variables[i] + "]", sh.obtenerSalarioValor(salario).ToString());
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
                        formula = formula.Replace("[" + variables[i] + "]", fh.existeFalta(falta).ToString());
                        cnx.Close();

                        break;

                    case "DiasLaborados":
                        int existe = 0, diasBaja = 0, idperiodo = 0, diasPago = 0, diasMesLaborados = 0, diasFaltas = 0;
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

                        if (existe != 0)
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
                                formula = formula.Replace("[" + variables[i] + "]", (diasFaltas + diasBaja).ToString());
                            else
                                formula = formula.Replace("[" + variables[i] + "]", diasBaja.ToString());
                        }
                        else
                        {
                            cnx.Open();
                            idperiodo = (int)edlh.obtenerIdPeriodo(idTrabajador);
                            cnx.Close();

                            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                            ph.Command = cmd;

                            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                            p.idperiodo = idperiodo;

                            cnx.Open();
                            diasPago = (int)ph.DiasDePago(p);
                            cnx.Close();

                            if (diasPago == 7)
                                formula = formula.Replace("[" + variables[i] + "]", diasPago.ToString());
                            else
                            {
                                if (inicioPeriodo.Day <= 15)
                                {
                                    formula = formula.Replace("[" + variables[i] + "]", diasPago.ToString());
                                }
                                else
                                {
                                    diasMesLaborados = DateTime.DaysInMonth(inicioPeriodo.Year, inicioPeriodo.Month);
                                    diasMesLaborados = diasMesLaborados - 15;
                                    formula = formula.Replace("[" + variables[i] + "]", diasMesLaborados.ToString());
                                }
                            }
                        }
                            
                        break;

                    case "DiasIncapacidad":
                        object existeIncidencia = null, diasIncapacidad = 0;
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
                            formula = formula.Replace("[" + variables[i] + "]", diasIncapacidad.ToString());
                        }
                        else
                        {
                            diasIncapacidad = 0;
                            formula = formula.Replace("[" + variables[i] + "]", diasIncapacidad.ToString());
                        }
                        break;

                    case "Infonavit":
                        
                        List<string> variablesInfonavit = new List<string>();

                        Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                        infh.Command = cmd;
                        List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();

                        Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                        inf.idtrabajador = idTrabajador;
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
                            formula = ch.obtenerFormula(concepto).ToString();
                            cnx.Close();
                            
                            return calcularFormula();
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

                    case "PeriodoInfonavit":

                        Infonavit.Core.InfonavitHelper iph = new Infonavit.Core.InfonavitHelper();
                        iph.Command = cmd;

                        Infonavit.Core.Infonavit infP = new Infonavit.Core.Infonavit();
                        infP.idtrabajador = idTrabajador;
                        List<Infonavit.Core.Infonavit> lstPeriodoInfonavit = new List<Infonavit.Core.Infonavit>();

                        cnx.Open();
                        lstPeriodoInfonavit = iph.obtenerDiasInfonavit(infP);
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
                        int diasPI = (int)ppih.DiasDePago(pPI);
                        cnx.Close();

                        if (lstPeriodoInfonavit.Count == 1)
                            if (lstPeriodoInfonavit[0].fecha >= inicioPeriodo && lstPeriodoInfonavit[0].fecha <= finPeriodo)
                                formula = formula.Replace("[" + variables[i] + "]", lstPeriodoInfonavit[0].dias.ToString());
                            else
                            {
                                formula = formula.Replace("[" + variables[i] + "]", diasPI.ToString());
                            }
                             
                        else if (lstPeriodoInfonavit.Count == 2)
                            if (lstPeriodoInfonavit[0].fecha >= inicioPeriodo && lstPeriodoInfonavit[0].fecha <= finPeriodo)
                            {
                                string _formula = formula;
                                formula = formula.Replace("[" + variables[i] + "]", lstPeriodoInfonavit[0].dias.ToString());
                                _formula = _formula.Replace("[" + variables[i] + "]", lstPeriodoInfonavit[1].dias.ToString());
                                formula = formula + "+" + _formula;
                            }
                            else
                                formula = formula.Replace("[" + variables[i] + "]", diasPI.ToString());
                        break;
                    case "SeguroInfonavit":

                        Empleados.Core.EmpleadosHelper esih = new Empleados.Core.EmpleadosHelper();
                        esih.Command = cmd;

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
                        double excedente = 0, ImpMarginal = 0, isr = 0;
                        int idperiodoISR = 0, diasISR = 0;
                        List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
                        TablaIsr.Core.IsrHelper isrh = new TablaIsr.Core.IsrHelper();
                        isrh.Command = cmd;

                        double sueldo = lstPercepciones.Where(e => e.idtrabajador == idTrabajador && e.noconcepto == 1).Sum(e => e.cantidad);
                        if (sueldo != 0)
                        {
                            double baseGravableIsr = lstPercepciones.Where(e => e.idtrabajador == idTrabajador).Sum(e => e.gravado);

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
                            _isr.inferior = (baseGravableIsr / diasISR) * 30.4;

                            cnx.Open();
                            lstIsr = isrh.isrCorrespondiente(_isr);
                            cnx.Close();

                            excedente = ((baseGravableIsr / diasISR) * 30.4) - lstIsr[0].inferior;
                            ImpMarginal = excedente * (lstIsr[0].porcentaje / 100);
                            isr = ImpMarginal + lstIsr[0].cuota;

                            return isr;
                        }
                        else
                        {
                            double vacaciones = lstPercepciones.Where(e => e.idtrabajador == idTrabajador && e.noconcepto == 7).Sum(e => e.cantidad);
                            if (vacaciones != 0)
                            {
                                double baseGravableIsr = lstPercepciones.Where(e => e.idtrabajador == idTrabajador).Sum(e => e.gravado);

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
                                _isr.inferior = (baseGravableIsr / diasISR) * 30.4;

                                cnx.Open();
                                lstIsr = isrh.isrCorrespondiente(_isr);
                                cnx.Close();

                                excedente = ((baseGravableIsr / diasISR) * 30.4) - lstIsr[0].inferior;
                                ImpMarginal = excedente * (lstIsr[0].porcentaje / 100);
                                isr = ImpMarginal + lstIsr[0].cuota;

                                return isr;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    case "Subsidio Acreditado":
                        double sueldoSubsidio = lstPercepciones.Where(e => e.idtrabajador == idTrabajador && e.noconcepto == 1).Sum(e => e.cantidad);
                        int idperiodoSubsidio = 0, diasSubsidio = 0;

                        if (sueldoSubsidio != 0)
                        {
                            double baseGravableSubsidio = lstPercepciones.Where(e => e.idtrabajador == idTrabajador).Sum(e => e.gravado);

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
                            subsidio.desde = (baseGravableSubsidio / diasSubsidio) * 30.4;

                            double cantidad = 0;
                            cnx.Open();
                            cantidad = double.Parse(ts.obtenerCantidadSubsidio(subsidio).ToString());
                            cnx.Close();

                            return cantidad;
                        }
                        else
                        {
                            double vacacionesSubsidio = lstPercepciones.Where(e => e.idtrabajador == idTrabajador && e.noconcepto == 7).Sum(e => e.cantidad);
                            if (vacacionesSubsidio != 0)
                            {
                                double baseGravableSubsidio = lstPercepciones.Where(e => e.idtrabajador == idTrabajador).Sum(e => e.gravado);

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
                                subsidio.desde = (baseGravableSubsidio / diasSubsidio) * 30.4;

                                double cantidad = 0;
                                cnx.Open();
                                cantidad = double.Parse(ts.obtenerCantidadSubsidio(subsidio).ToString());
                                cnx.Close();

                                return cantidad;
                            }
                            else 
                            {
                                return 0;
                            }
                        }
                }
            }
            cnx.Dispose();
            MathParserTK.MathParser parser = new MathParserTK.MathParser();
            resultado = parser.Parse(formula.ToString());
            return resultado;
        }
    }
}
