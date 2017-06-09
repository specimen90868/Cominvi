using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominas
{
    public static class CALCULO
    {
        public static List<CalculoNomina.Core.tmpPagoNomina> PERCEPCIONES(List<CalculoNomina.Core.Nomina> lstConceptosPercepciones,
            DateTime inicio, DateTime fin, int tipoNomina)
        {

            #region VARIABLES GLOBALES
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            #endregion

            #region LISTA PARA DATOS DEL TRABAJADOR
            List<CalculoNomina.Core.tmpPagoNomina> lstValoresNomina;
            List<CalculoNomina.Core.tmpPagoNomina> lstValoresDefinitivos;
            #endregion

            #region CALCULO
            lstValoresNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
            for (int i = 0; i < lstConceptosPercepciones.Count; i++)
            {
                CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                vn.idtrabajador = lstConceptosPercepciones[i].idtrabajador;
                vn.idempresa = GLOBALES.IDEMPRESA;
                vn.idconcepto = lstConceptosPercepciones[i].id;
                vn.noconcepto = lstConceptosPercepciones[i].noconcepto;
                vn.tipoconcepto = lstConceptosPercepciones[i].tipoconcepto;
                vn.fechainicio = inicio.Date;
                vn.fechafin = fin.Date;
                vn.guardada = false;
                vn.tiponomina = tipoNomina;
                vn.modificado = false;

                CalculoFormula formula = new CalculoFormula(lstConceptosPercepciones[i].idtrabajador, inicio.Date, fin.Date, lstConceptosPercepciones[i].formula);
                vn.cantidad = decimal.Parse(formula.calcularFormula().ToString());

                CalculoFormula formulaExcento = new CalculoFormula(lstConceptosPercepciones[i].idtrabajador, inicio.Date, fin.Date, lstConceptosPercepciones[i].formulaexento);
                vn.exento = decimal.Parse(formulaExcento.calcularFormula().ToString());

                Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                cnx.Open();
                int idperiodo = (int)eh.obtenerIdPeriodo(lstConceptosPercepciones[i].idtrabajador);
                cnx.Close();

                Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                ph.Command = cmd;

                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                p.idperiodo = idperiodo;

                cnx.Open();
                int dias = (int)ph.DiasDePago(p);
                cnx.Close();

                Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
                ch.Command = cmd;

                Conceptos.Core.Conceptos c = new Conceptos.Core.Conceptos();
                c.idempresa = GLOBALES.IDEMPRESA;
                c.noconcepto = lstConceptosPercepciones[i].noconcepto;
                c.tipoconcepto = lstConceptosPercepciones[i].tipoconcepto;
                c.periodo = dias;

                cnx.Open();
                bool grava = (bool)ch.gravaConcepto(c);
                bool exenta = (bool)ch.exentaConcepto(c);
                cnx.Close();

                if (grava && !exenta)
                {
                    vn.gravado = vn.cantidad;
                }
                
                if (grava && exenta)
                {
                    if (vn.cantidad <= vn.exento)
                    {
                        vn.exento = vn.cantidad;
                        vn.gravado = 0;
                    }
                    else
                    {
                        vn.gravado = vn.cantidad - vn.exento;
                    }
                }

                if (!grava && exenta)
                {
                    vn.gravado = 0;
                    vn.exento = vn.cantidad;
                }

                #region SWITCH SUELDO CERO
                switch (lstConceptosPercepciones[i].noconcepto)
                {
                    case 1:
                        if (vn.cantidad == 0)
                        {
                            vn.gravado = 0;
                            lstValoresNomina.Add(vn);

                            Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                            vh.Command = cmd;
                            Vacaciones.Core.VacacionesPrima vp = new Vacaciones.Core.VacacionesPrima();
                            vp.idtrabajador = lstConceptosPercepciones[i].idtrabajador;
                            vp.idempresa = GLOBALES.IDEMPRESA;
                            vp.periodofin = fin.Date;
                            vp.periodoinicio = inicio.Date;
                            vp.vacacionesprima = "V";

                            cnx.Open();
                            int diasVacaciones = (int)vh.pagoVacacionesPrima(vp);
                            cnx.Close();

                            if (diasVacaciones == 0)
                            {
                                i++;
                                int contadorDatosNomina = i;
                                for (int j = i; j < lstConceptosPercepciones.Count; j++)
                                {
                                    contadorDatosNomina = j;
                                    if (lstConceptosPercepciones[j].idtrabajador == vn.idtrabajador)
                                    {
                                        CalculoNomina.Core.tmpPagoNomina vnCero = new CalculoNomina.Core.tmpPagoNomina();
                                        vnCero.idtrabajador = lstConceptosPercepciones[j].idtrabajador;
                                        vnCero.idempresa = GLOBALES.IDEMPRESA;
                                        vnCero.idconcepto = lstConceptosPercepciones[j].id;
                                        vnCero.noconcepto = lstConceptosPercepciones[j].noconcepto;
                                        vnCero.tipoconcepto = lstConceptosPercepciones[j].tipoconcepto;
                                        vnCero.fechainicio = inicio.Date;
                                        vnCero.fechafin = fin.Date;
                                        vnCero.guardada = false;
                                        vnCero.tiponomina = tipoNomina;
                                        vnCero.modificado = false;
                                        vnCero.cantidad = 0;
                                        vnCero.exento = 0;
                                        vnCero.gravado = 0;
                                        lstValoresNomina.Add(vnCero);
                                    }
                                    else
                                    {
                                        --contadorDatosNomina;
                                        break;
                                    }
                                }
                                i = contadorDatosNomina;
                            }
                        }
                        else
                            lstValoresNomina.Add(vn);
                        break;
                    default:
                        lstValoresNomina.Add(vn);
                        break;
                }
                #endregion
            }
            #endregion

            #region EXISTENCIA DEL CONCEPTO EN TABLA
            int existe = 0;
            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            lstValoresDefinitivos = new List<CalculoNomina.Core.tmpPagoNomina>();
            for (int i = 0; i < lstValoresNomina.Count; i++)
            {
                CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                vn.idtrabajador = lstValoresNomina[i].idtrabajador;
                vn.idempresa = GLOBALES.IDEMPRESA;
                vn.idconcepto = lstValoresNomina[i].idconcepto;
                vn.noconcepto = lstValoresNomina[i].noconcepto;
                vn.tipoconcepto = lstValoresNomina[i].tipoconcepto;
                vn.fechainicio = lstValoresNomina[i].fechainicio;
                vn.fechafin = lstValoresNomina[i].fechafin;
                vn.guardada = lstValoresNomina[i].guardada;
                vn.tiponomina = lstValoresNomina[i].tiponomina;
                vn.modificado = lstValoresNomina[i].modificado;
                vn.exento = lstValoresNomina[i].exento;
                vn.gravado = lstValoresNomina[i].gravado;
                vn.cantidad = lstValoresNomina[i].cantidad;

                cnx.Open();
                existe = (int)nh.existeConcepto(vn);
                cnx.Close();

                if (existe == 0)
                {
                    lstValoresDefinitivos.Add(vn);
                }
                else
                {
                    cnx.Open();
                    nh.actualizaConcepto(vn);
                    cnx.Close();
                }
            }
            #endregion

            return lstValoresDefinitivos;
        }

        public static List<CalculoNomina.Core.tmpPagoNomina> DEDUCCIONES(List<CalculoNomina.Core.Nomina> lstConceptosDeducciones,
            List<CalculoNomina.Core.tmpPagoNomina> lstPercepciones, DateTime inicio, DateTime fin, int tipoNomina)
        {
            #region VARIABLES GLOBALES
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            #endregion

            #region VARIABLES
            bool activoInfonavit = false;
            #endregion

            #region LISTA PARA DATOS DEL TRABAJADOR
            List<CalculoNomina.Core.tmpPagoNomina> lstValoresNomina;
            List<CalculoNomina.Core.tmpPagoNomina> lstValoresDefinitivos;
            #endregion

            #region CALCULO
            lstValoresNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
            decimal isrAntes = 0, subsidioAntes = 0;

            for (int i = 0; i < lstConceptosDeducciones.Count; i++)
            {
                decimal percepciones = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador && e.tipoconcepto == "P").Sum(e => e.cantidad);

                switch (lstConceptosDeducciones[i].noconcepto)
                {

                    #region CONCEPTO IMSS
                    case 99:
                        int vsmdf; //idsalario;
                        decimal porcentajeImss, excedenteVsmdf, sm, sdiTrabajador;
                        
                        Configuracion.Core.ConfiguracionHelper ch = new Configuracion.Core.ConfiguracionHelper();
                        ch.Command = cmd;

                        Imss.Core.ImssHelper ih = new Imss.Core.ImssHelper();
                        ih.Command = cmd;

                        Imss.Core.Imss imss = new Imss.Core.Imss();
                        imss.calculo = true;

                        Empleados.Core.EmpleadosHelper empleadosHelper = new Empleados.Core.EmpleadosHelper();
                        empleadosHelper.Command = cmd;
                        Empleados.Core.Empleados empleadoImss = new Empleados.Core.Empleados();
                        empleadoImss.idtrabajador = lstConceptosDeducciones[i].idtrabajador;

                        Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
                        sh.Command = cmd;
                        
                        cnx.Open();
                        vsmdf = int.Parse(ch.obtenerValorConfiguracion("VSMDF").ToString());
                        porcentajeImss = ih.CuotaObreroPatronal(imss);
                        excedenteVsmdf = ih.ExcedenteVSM(5);
                        
                        sm = decimal.Parse(sh.obtenerSalarioValor().ToString());
                        sdiTrabajador = decimal.Parse(empleadosHelper.obtenerSalarioDiarioIntegrado(empleadoImss).ToString());
                        cnx.Close();

                        string formulaDiasAPagar = "[DiasLaborados]-[Faltas]-[DiasIncapacidad]";
                        CalculoFormula cfImss = new CalculoFormula(lstConceptosDeducciones[i].idtrabajador, inicio, fin, formulaDiasAPagar);
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

                        CalculoNomina.Core.tmpPagoNomina imssNomina = new CalculoNomina.Core.tmpPagoNomina();
                        imssNomina.idtrabajador = lstConceptosDeducciones[i].idtrabajador;
                        imssNomina.idempresa = GLOBALES.IDEMPRESA;
                        imssNomina.idconcepto = lstConceptosDeducciones[i].id;
                        imssNomina.noconcepto = lstConceptosDeducciones[i].noconcepto;
                        imssNomina.tipoconcepto = lstConceptosDeducciones[i].tipoconcepto;
                        imssNomina.fechainicio = inicio.Date;
                        imssNomina.fechafin = fin.Date;
                        imssNomina.exento = 0;
                        imssNomina.gravado = 0;
                        imssNomina.cantidad = totalImss;
                        imssNomina.diaslaborados = 0;
                        imssNomina.guardada = false;
                        imssNomina.tiponomina = tipoNomina;
                        imssNomina.modificado = false;

                        lstValoresNomina.Add(imssNomina);
                        break;
                    #endregion

                    #region CONCEPTO ISR ANTES DE SUBSIDIO
                    case 8:

                        decimal excedente = 0, ImpMarginal = 0, isr = 0;
                        List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
                        TablaIsr.Core.IsrHelper isrh = new TablaIsr.Core.IsrHelper();
                        isrh.Command = cmd;

                        CalculoNomina.Core.tmpPagoNomina isrAntesSubsidio = new CalculoNomina.Core.tmpPagoNomina();
                        isrAntesSubsidio.idtrabajador = lstConceptosDeducciones[i].idtrabajador;
                        isrAntesSubsidio.idempresa = GLOBALES.IDEMPRESA;
                        isrAntesSubsidio.idconcepto = lstConceptosDeducciones[i].id;
                        isrAntesSubsidio.noconcepto = lstConceptosDeducciones[i].noconcepto;
                        isrAntesSubsidio.tipoconcepto = lstConceptosDeducciones[i].tipoconcepto;
                        isrAntesSubsidio.fechainicio = inicio.Date;
                        isrAntesSubsidio.fechafin = fin.Date;
                        isrAntesSubsidio.exento = 0;
                        isrAntesSubsidio.gravado = 0;
                        

                        if (percepciones != 0)
                        {
                            decimal baseGravableIsr = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador).Sum(e => e.gravado);

                            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                            eh.Command = cmd;

                            cnx.Open();
                            int idperiodo = (int)eh.obtenerIdPeriodo(lstConceptosDeducciones[i].idtrabajador);
                            cnx.Close();

                            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                            ph.Command = cmd;

                            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                            p.idperiodo = idperiodo;

                            cnx.Open();
                            int dias = (int)ph.DiasDePago(p);
                            cnx.Close();

                            TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
                            _isr.inferior = (baseGravableIsr / dias) * decimal.Parse((30.4).ToString());

                            cnx.Open();
                            lstIsr = isrh.isrCorrespondiente(_isr);
                            cnx.Close();

                            excedente = ((baseGravableIsr / dias) * decimal.Parse((30.4).ToString())) - lstIsr[0].inferior;
                            ImpMarginal = excedente * (lstIsr[0].porcentaje / 100);
                            isr = ImpMarginal + lstIsr[0].cuota;
                            
                            isrAntesSubsidio.cantidad = (isr / decimal.Parse((30.4).ToString())) * dias;
                            isrAntes = (isr / decimal.Parse((30.4).ToString())) * dias;
                        }
                        else
                        {
                            isrAntes = 0;
                            isrAntesSubsidio.cantidad = 0;
                            //double vacaciones = lstPercepciones.Where(e => e.idtrabajador == lstPercepciones[i].idtrabajador && e.noconcepto == 7).Sum(e => e.cantidad);
                            //if (vacaciones != 0)
                            //{
                            //    double baseGravableIsr = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador).Sum(e => e.gravado);

                            //    Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                            //    eh.Command = cmd;

                            //    cnx.Open();
                            //    int idperiodo = (int)eh.obtenerIdPeriodo(lstConceptosDeducciones[i].idtrabajador);
                            //    cnx.Close();

                            //    Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                            //    ph.Command = cmd;

                            //    Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                            //    p.idperiodo = idperiodo;

                            //    cnx.Open();
                            //    int dias = (int)ph.DiasDePago(p);
                            //    cnx.Close();

                            //    TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
                            //    _isr.inferior = (baseGravableIsr / dias) * 30.4;

                            //    cnx.Open();
                            //    lstIsr = isrh.isrCorrespondiente(_isr);
                            //    cnx.Close();

                            //    excedente = ((baseGravableIsr / dias) * 30.4) - lstIsr[0].inferior;
                            //    ImpMarginal = excedente * (lstIsr[0].porcentaje / 100);
                            //    isr = ImpMarginal + lstIsr[0].cuota;

                            //    isrAntesSubsidio.cantidad = isr;
                            //    isrAntes = isr;
                            //}
                            //else
                            //{
                            //    isrAntes = 0;
                            //    isrAntesSubsidio.cantidad = 0;
                            //}
                        }

                        isrAntesSubsidio.guardada = false;
                        isrAntesSubsidio.tiponomina = tipoNomina;
                        isrAntesSubsidio.modificado = false;
                        lstValoresNomina.Add(isrAntesSubsidio);
                        break;
                    #endregion

                    #region SUBSIDIO
                    case 15:
                        //double sueldoSubsidio = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);

                        CalculoNomina.Core.tmpPagoNomina subsidioNomina = new CalculoNomina.Core.tmpPagoNomina();
                        subsidioNomina.idtrabajador = lstConceptosDeducciones[i].idtrabajador;
                        subsidioNomina.idempresa = GLOBALES.IDEMPRESA;
                        subsidioNomina.idconcepto = lstConceptosDeducciones[i].id;
                        subsidioNomina.noconcepto = lstConceptosDeducciones[i].noconcepto;
                        subsidioNomina.tipoconcepto = lstConceptosDeducciones[i].tipoconcepto;
                        subsidioNomina.fechainicio = inicio.Date;
                        subsidioNomina.fechafin = fin.Date;
                        subsidioNomina.exento = 0;
                        subsidioNomina.gravado = 0;

                        if (percepciones != 0)
                        {
                            decimal baseGravableSubsidio = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador).Sum(e => e.gravado);

                            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                            eh.Command = cmd;

                            cnx.Open();
                            int idperiodo = (int)eh.obtenerIdPeriodo(lstConceptosDeducciones[i].idtrabajador);
                            cnx.Close();

                            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                            ph.Command = cmd;

                            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                            p.idperiodo = idperiodo;

                            cnx.Open();
                            int dias = (int)ph.DiasDePago(p);
                            cnx.Close();

                            TablaSubsidio.Core.SubsidioHelper ts = new TablaSubsidio.Core.SubsidioHelper();
                            ts.Command = cmd;
                            TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
                            subsidio.desde = (baseGravableSubsidio / dias) * decimal.Parse((30.4).ToString());

                            decimal cantidad = 0;
                            cnx.Open();
                            cantidad = decimal.Parse(ts.obtenerCantidadSubsidio(subsidio).ToString());
                            cnx.Close();

                            subsidioNomina.cantidad = (cantidad / decimal.Parse((30.4).ToString())) * dias;
                            subsidioAntes = (cantidad / decimal.Parse((30.4).ToString())) * dias;
                        }
                        else
                        {
                            subsidioNomina.cantidad = 0;
                            subsidioAntes = 0;
                            //double vacacionesSubsidio = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador && e.noconcepto == 7).Sum(e => e.cantidad);
                            //if (vacacionesSubsidio != 0)
                            //{
                            //    double baseGravableSubsidio = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador).Sum(e => e.gravado);

                            //    Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                            //    eh.Command = cmd;

                            //    cnx.Open();
                            //    int idperiodo = (int)eh.obtenerIdPeriodo(lstConceptosDeducciones[i].idtrabajador);
                            //    cnx.Close();

                            //    Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                            //    ph.Command = cmd;

                            //    Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                            //    p.idperiodo = idperiodo;

                            //    cnx.Open();
                            //    int dias = (int)ph.DiasDePago(p);
                            //    cnx.Close();

                            //    TablaSubsidio.Core.SubsidioHelper ts = new TablaSubsidio.Core.SubsidioHelper();
                            //    ts.Command = cmd;
                            //    TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
                            //    subsidio.desde = (baseGravableSubsidio / dias) * 30.4;

                            //    double cantidad = 0;
                            //    cnx.Open();
                            //    cantidad = double.Parse(ts.obtenerCantidadSubsidio(subsidio).ToString());
                            //    cnx.Close();

                            //    subsidioNomina.cantidad = cantidad;
                            //    subsidioAntes = cantidad;
                            //}
                            //else
                            //{
                            //    subsidioNomina.cantidad = 0;
                            //    subsidioAntes = 0;
                            //}
                        }

                        subsidioNomina.guardada = false;
                        subsidioNomina.tiponomina = tipoNomina;
                        subsidioNomina.modificado = false;
                        lstValoresNomina.Add(subsidioNomina);
                        break;
                    #endregion

                    #region SUBSIDIO DEFINITIVO
                    case 16:
                        CalculoNomina.Core.tmpPagoNomina subsidioDefinitivo = new CalculoNomina.Core.tmpPagoNomina();
                        subsidioDefinitivo.idtrabajador = lstConceptosDeducciones[i].idtrabajador;
                        subsidioDefinitivo.idempresa = GLOBALES.IDEMPRESA;
                        subsidioDefinitivo.idconcepto = lstConceptosDeducciones[i].id;
                        subsidioDefinitivo.noconcepto = lstConceptosDeducciones[i].noconcepto;
                        subsidioDefinitivo.tipoconcepto = lstConceptosDeducciones[i].tipoconcepto;
                        subsidioDefinitivo.fechainicio = inicio.Date;
                        subsidioDefinitivo.fechafin = fin.Date;
                        subsidioDefinitivo.exento = 0;
                        subsidioDefinitivo.gravado = 0;

                        //double sueldoSubsidioDefinitivo = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);

                        if (percepciones != 0)
                        {
                            //Empleados.Core.EmpleadosHelper esh = new Empleados.Core.EmpleadosHelper();
                            //esh.Command = cmd;

                            //cnx.Open();
                            //int idperiodoSubsidio = (int)esh.obtenerIdPeriodo(lstConceptosDeducciones[i].idtrabajador);
                            //cnx.Close();

                            //Periodos.Core.PeriodosHelper psh = new Periodos.Core.PeriodosHelper();
                            //psh.Command = cmd;

                            //Periodos.Core.Periodos ps = new Periodos.Core.Periodos();
                            //ps.idperiodo = idperiodoSubsidio;

                            //cnx.Open();
                            //int diasSubsidio = (int)psh.DiasDePago(ps);
                            //cnx.Close();

                            if (subsidioAntes > isrAntes)
                                subsidioDefinitivo.cantidad = subsidioAntes - isrAntes;
                            else
                                subsidioDefinitivo.cantidad = 0;
                        }
                        else
                        {
                            subsidioDefinitivo.cantidad = 0;
                            //double vacacionSubsidioDefinitivo = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador && e.noconcepto == 7).Sum(e => e.cantidad);
                            //if (vacacionSubsidioDefinitivo != 0)
                            //{
                            //    Empleados.Core.EmpleadosHelper esh = new Empleados.Core.EmpleadosHelper();
                            //    esh.Command = cmd;

                            //    cnx.Open();
                            //    int idperiodoSubsidio = (int)esh.obtenerIdPeriodo(lstConceptosDeducciones[i].idtrabajador);
                            //    cnx.Close();

                            //    Periodos.Core.PeriodosHelper psh = new Periodos.Core.PeriodosHelper();
                            //    psh.Command = cmd;

                            //    Periodos.Core.Periodos ps = new Periodos.Core.Periodos();
                            //    ps.idperiodo = idperiodoSubsidio;

                            //    cnx.Open();
                            //    int diasSubsidio = (int)psh.DiasDePago(ps);
                            //    cnx.Close();

                            //    if (subsidioAntes > isrAntes)
                            //        subsidioDefinitivo.cantidad = subsidioAntes - isrAntes;
                            //    else
                            //        subsidioDefinitivo.cantidad = 0;
                            //}
                            //else
                            //    subsidioDefinitivo.cantidad = 0;
                        }

                        subsidioDefinitivo.guardada = false;
                        subsidioDefinitivo.tiponomina = tipoNomina;
                        subsidioDefinitivo.modificado = false;
                        lstValoresNomina.Add(subsidioDefinitivo);
                        break;
                    #endregion

                    #region ISR DEFINITIVO
                    case 17:
                        CalculoNomina.Core.tmpPagoNomina isrDefinitivo = new CalculoNomina.Core.tmpPagoNomina();
                        isrDefinitivo.idtrabajador = lstConceptosDeducciones[i].idtrabajador;
                        isrDefinitivo.idempresa = GLOBALES.IDEMPRESA;
                        isrDefinitivo.idconcepto = lstConceptosDeducciones[i].id;
                        isrDefinitivo.noconcepto = lstConceptosDeducciones[i].noconcepto;
                        isrDefinitivo.tipoconcepto = lstConceptosDeducciones[i].tipoconcepto;
                        isrDefinitivo.fechainicio = inicio.Date;
                        isrDefinitivo.fechafin = fin.Date;
                        isrDefinitivo.exento = 0;
                        isrDefinitivo.gravado = 0;

                        //double sueldoIsrDefinitivo = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);

                        if (percepciones != 0)
                        {
                            //Empleados.Core.EmpleadosHelper eih = new Empleados.Core.EmpleadosHelper();
                            //eih.Command = cmd;

                            //cnx.Open();
                            //int idperiodoIsr = (int)eih.obtenerIdPeriodo(lstConceptosDeducciones[i].idtrabajador);
                            //cnx.Close();

                            //Periodos.Core.PeriodosHelper pih = new Periodos.Core.PeriodosHelper();
                            //pih.Command = cmd;

                            //Periodos.Core.Periodos pi = new Periodos.Core.Periodos();
                            //pi.idperiodo = idperiodoIsr;

                            //cnx.Open();
                            //int diasIsr = (int)pih.DiasDePago(pi);
                            //cnx.Close();

                            if (subsidioAntes > isrAntes)
                            {
                                isrDefinitivo.cantidad = 0;
                            }
                            else
                            {
                                isrDefinitivo.cantidad = isrAntes - subsidioAntes;
                            }
                        }
                        else
                        {
                            isrDefinitivo.cantidad = 0;
                            //double vacacionIsrDefinitivo = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador && e.noconcepto == 7).Sum(e => e.cantidad);
                            //if (vacacionIsrDefinitivo != 0)
                            //{
                            //    Empleados.Core.EmpleadosHelper eih = new Empleados.Core.EmpleadosHelper();
                            //    eih.Command = cmd;

                            //    cnx.Open();
                            //    int idperiodoIsr = (int)eih.obtenerIdPeriodo(lstConceptosDeducciones[i].idtrabajador);
                            //    cnx.Close();

                            //    Periodos.Core.PeriodosHelper pih = new Periodos.Core.PeriodosHelper();
                            //    pih.Command = cmd;

                            //    Periodos.Core.Periodos pi = new Periodos.Core.Periodos();
                            //    pi.idperiodo = idperiodoIsr;

                            //    cnx.Open();
                            //    int diasIsr = (int)pih.DiasDePago(pi);
                            //    cnx.Close();

                            //    double isptIsr = 0;
                            //    if (subsidioAntes > isrAntes)
                            //    {
                            //        isrDefinitivo.cantidad = 0;
                            //    }
                            //    else
                            //    {
                            //        isptIsr = ((isrAntes - subsidioAntes) / 30.4) * diasIsr;

                            //        if (isptIsr <= 0)
                            //        {
                            //            isrDefinitivo.cantidad = 0;
                            //        }
                            //        else
                            //        {
                            //            isrDefinitivo.cantidad = isptIsr;
                            //        }
                            //    }
                            //}
                            //else
                            //    isrDefinitivo.cantidad = 0;
                        }

                        isrDefinitivo.guardada = false;
                        isrDefinitivo.tiponomina = tipoNomina;
                        isrDefinitivo.modificado = false;
                        lstValoresNomina.Add(isrDefinitivo);
                        break;
                    #endregion

                    #region OTRAS DEDUCCIONES
                    default:
                        //double sueldoDeducciones = lstPercepciones.Where(e => e.idtrabajador == lstConceptosDeducciones[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);

                        CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                        vn.idtrabajador = lstConceptosDeducciones[i].idtrabajador;
                        vn.idempresa = GLOBALES.IDEMPRESA;
                        vn.idconcepto = lstConceptosDeducciones[i].id;
                        vn.noconcepto = lstConceptosDeducciones[i].noconcepto;
                        vn.tipoconcepto = lstConceptosDeducciones[i].tipoconcepto;
                        vn.fechainicio = inicio.Date;
                        vn.fechafin = fin.Date;
                        vn.guardada = false;
                        vn.tiponomina = tipoNomina;
                        vn.modificado = false;

                        #region SUELDO DIFERENTE DE CERO
                        if (percepciones != 0)
                        {
                            Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                            infh.Command = cmd;

                            Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                            inf.idtrabajador = lstConceptosDeducciones[i].idtrabajador;
                            inf.idempresa = GLOBALES.IDEMPRESA;

                            if (lstConceptosDeducciones[i].noconcepto == 9 || lstConceptosDeducciones[i].noconcepto == 21)
                            {
                                cnx.Open();
                                activoInfonavit = (bool)infh.activoInfonavit(inf);
                                cnx.Close();

                                if (activoInfonavit)
                                {
                                    CalculoFormula cf = new CalculoFormula(lstConceptosDeducciones[i].idtrabajador, inicio.Date, fin.Date, lstConceptosDeducciones[i].formula);
                                    vn.cantidad = decimal.Parse(cf.calcularFormula().ToString());
                                    vn.exento = 0;
                                    vn.gravado = 0;
                                }
                                else
                                {
                                    
                                    vn.cantidad = 0;
                                    vn.exento = 0;
                                    vn.gravado = 0;
                                }
                            }
                            else
                            {
                                CalculoFormula cf = new CalculoFormula(lstConceptosDeducciones[i].idtrabajador, inicio.Date, fin.Date, lstConceptosDeducciones[i].formula);
                                vn.cantidad = decimal.Parse(cf.calcularFormula().ToString());
                                vn.exento = 0;
                                vn.gravado = 0;
                            }
                                lstValoresNomina.Add(vn);
                        }
                        else
                        {
                            vn.cantidad = 0;
                            vn.exento = 0;
                            vn.gravado = 0;
                            lstValoresNomina.Add(vn);
                        }
                        break;
                        #endregion
                    #endregion
                }
            }
            #endregion

            #region EXISTENCIA DEL CONCEPTO EN TABLA
            int existe = 0;
            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            lstValoresDefinitivos = new List<CalculoNomina.Core.tmpPagoNomina>();
            for (int i = 0; i < lstValoresNomina.Count; i++)
            {
                CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                vn.idtrabajador = lstValoresNomina[i].idtrabajador;
                vn.idempresa = GLOBALES.IDEMPRESA;
                vn.idconcepto = lstValoresNomina[i].idconcepto;
                vn.noconcepto = lstValoresNomina[i].noconcepto;
                vn.tipoconcepto = lstValoresNomina[i].tipoconcepto;
                vn.fechainicio = lstValoresNomina[i].fechainicio;
                vn.fechafin = lstValoresNomina[i].fechafin;
                vn.guardada = lstValoresNomina[i].guardada;
                vn.tiponomina = lstValoresNomina[i].tiponomina;
                vn.modificado = lstValoresNomina[i].modificado;
                vn.exento = lstValoresNomina[i].exento;
                vn.gravado = lstValoresNomina[i].gravado;
                vn.cantidad = lstValoresNomina[i].cantidad;

                cnx.Open();
                existe = (int)nh.existeConcepto(vn);
                cnx.Close();

                if (existe == 0)
                {
                    lstValoresDefinitivos.Add(vn);
                }
                else
                {
                    cnx.Open();
                    nh.actualizaConcepto(vn);
                    cnx.Close();
                }
            }
            #endregion

            return lstValoresDefinitivos;
        }
    }
}
