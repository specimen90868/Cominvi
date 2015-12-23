using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominas
{
    public static class CALCULO
    {
        
        public static List<CalculoNomina.Core.tmpPagoNomina> PERCEPCIONES(List<CalculoNomina.Core.Nomina> lstDatosNomina, 
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
            List<CalculoNomina.Core.Nomina> lstDatoTrabajador;
            #endregion

            #region CALCULO
            lstValoresNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
            for (int i = 0; i < lstDatosNomina.Count; i++)
            {

                CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                vn.idtrabajador = lstDatosNomina[i].idtrabajador;
                vn.idempresa = GLOBALES.IDEMPRESA;
                vn.idconcepto = lstDatosNomina[i].id;
                vn.noconcepto = lstDatosNomina[i].noconcepto;
                vn.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                vn.fechainicio = inicio.Date;
                vn.fechafin = fin.Date;
                vn.guardada = false;
                vn.tiponomina = tipoNomina;
                vn.modificado = false;

                lstDatoTrabajador = new List<CalculoNomina.Core.Nomina>();
                CalculoNomina.Core.Nomina nt = new CalculoNomina.Core.Nomina();
                nt.idtrabajador = lstDatosNomina[i].idtrabajador;
                nt.dias = lstDatosNomina[i].dias;
                nt.salariominimo = lstDatosNomina[i].salariominimo;
                nt.antiguedadmod = lstDatosNomina[i].antiguedadmod;
                nt.sdi = lstDatosNomina[i].sdi;
                nt.sd = lstDatosNomina[i].sd;
                nt.id = lstDatosNomina[i].id;
                nt.noconcepto = lstDatosNomina[i].noconcepto;
                nt.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                nt.formula = lstDatosNomina[i].formula;
                nt.formulaexento = lstDatosNomina[i].formulaexento;
                lstDatoTrabajador.Add(nt);

                FormulasValores f = new FormulasValores(lstDatoTrabajador, inicio.Date, fin.Date);
                vn.cantidad = double.Parse(f.calcularFormula().ToString());
                vn.exento = double.Parse(f.calcularFormulaExento().ToString());
                vn.gravado = double.Parse(f.calcularFormula().ToString()) - double.Parse(f.calcularFormulaExento().ToString());

                switch (lstDatosNomina[i].noconcepto)
                {
                    case 1:
                        if (vn.cantidad == 0)
                        {
                            i++;
                            vn.gravado = 0;
                            lstValoresNomina.Add(vn);
                            int contadorDatosNomina = i;
                            for (int j = i; j < lstDatosNomina.Count; j++)
                            {
                                contadorDatosNomina = j;
                                if (lstDatosNomina[j].idtrabajador == vn.idtrabajador)
                                {
                                    CalculoNomina.Core.tmpPagoNomina vnCero = new CalculoNomina.Core.tmpPagoNomina();
                                    vnCero.idtrabajador = lstDatosNomina[j].idtrabajador;
                                    vnCero.idempresa = GLOBALES.IDEMPRESA;
                                    vnCero.idconcepto = lstDatosNomina[j].id;
                                    vnCero.noconcepto = lstDatosNomina[j].noconcepto;
                                    vnCero.tipoconcepto = lstDatosNomina[j].tipoconcepto;
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
                        else
                            lstValoresNomina.Add(vn);
                        break;
                    case 2: // HORAS EXTRAS DOBLES
                        if (vn.cantidad <= vn.exento)
                        {
                            vn.exento = vn.cantidad;
                            vn.gravado = 0;

                        }
                        lstValoresNomina.Add(vn);
                        break;
                    case 3: // PREMIO DE ASISTENCIA
                        if (vn.cantidad <= vn.exento)
                        {
                            vn.exento = vn.cantidad;
                            vn.gravado = 0;
                        }
                        lstValoresNomina.Add(vn);
                        break;
                    case 5: // PREMIO DE PUNTUALIDAD
                        if (vn.cantidad <= vn.exento)
                        {
                            vn.exento = vn.cantidad;
                            vn.gravado = 0;
                        }
                        lstValoresNomina.Add(vn);
                        break;
                    default:
                        lstValoresNomina.Add(vn);
                        break;
                }

            }
            #endregion
            return lstValoresNomina;
        }


        public static List<CalculoNomina.Core.tmpPagoNomina> ISR_SUBSIDIO(List<CalculoNomina.Core.Nomina> lstDatosNomina, 
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
            List<CalculoNomina.Core.Nomina> lstDatoTrabajador;
            #endregion

            #region CALCULO
            lstValoresNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
            for (int i = 0; i < lstDatosNomina.Count; i++)
            {
                switch (lstDatosNomina[i].noconcepto)
                {
                    #region CONCEPTO ISR ANTES DE SUBSIDIO
                    case 8:
                        
                        double excedente = 0, ImpMarginal = 0, isr = 0;
                        List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
                        TablaIsr.Core.IsrHelper isrh = new TablaIsr.Core.IsrHelper();
                        isrh.Command = cmd;

                        CalculoNomina.Core.tmpPagoNomina isrAntesSubsidio = new CalculoNomina.Core.tmpPagoNomina();
                        isrAntesSubsidio.idtrabajador = lstDatosNomina[i].idtrabajador;
                        isrAntesSubsidio.idempresa = GLOBALES.IDEMPRESA;
                        isrAntesSubsidio.idconcepto = lstDatosNomina[i].id;
                        isrAntesSubsidio.noconcepto = lstDatosNomina[i].noconcepto;
                        isrAntesSubsidio.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                        isrAntesSubsidio.fechainicio = inicio.Date;
                        isrAntesSubsidio.fechafin = fin.Date;
                        isrAntesSubsidio.exento = 0;
                        isrAntesSubsidio.gravado = 0;

                        double sueldo = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);
                        if (sueldo != 0)
                        {
                            double baseGravableIsr = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador).Sum(e => e.gravado);

                            TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
                            _isr.inferior = (baseGravableIsr / lstDatosNomina[i].dias) * 30.4;

                            cnx.Open();
                            lstIsr = isrh.isrCorrespondiente(_isr);
                            cnx.Close();

                            excedente = ((baseGravableIsr / lstDatosNomina[i].dias) * 30.4) - lstIsr[0].inferior;
                            ImpMarginal = excedente * (lstIsr[0].porcentaje / 100);
                            isr = ImpMarginal + lstIsr[0].cuota;

                            isrAntesSubsidio.cantidad = isr;
                        }
                        else
                            isrAntesSubsidio.cantidad = 0;

                        isrAntesSubsidio.guardada = false;
                        isrAntesSubsidio.tiponomina = tipoNomina;
                        isrAntesSubsidio.modificado = false;
                        lstValoresNomina.Add(isrAntesSubsidio);
                        break;
                    #endregion

                    #region SUBSIDIO
                    case 15:
                        double sueldoSubsidio = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);

                        CalculoNomina.Core.tmpPagoNomina subsidioNomina = new CalculoNomina.Core.tmpPagoNomina();
                        subsidioNomina.idtrabajador = lstDatosNomina[i].idtrabajador;
                        subsidioNomina.idempresa = GLOBALES.IDEMPRESA;
                        subsidioNomina.idconcepto = lstDatosNomina[i].id;
                        subsidioNomina.noconcepto = lstDatosNomina[i].noconcepto;
                        subsidioNomina.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                        subsidioNomina.fechainicio = inicio.Date;
                        subsidioNomina.fechafin = fin.Date;
                        subsidioNomina.exento = 0;
                        subsidioNomina.gravado = 0;

                        if (sueldoSubsidio != 0)
                        {
                            double baseGravableSubsidio = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador).Sum(e => e.gravado);

                            TablaSubsidio.Core.SubsidioHelper ts = new TablaSubsidio.Core.SubsidioHelper();
                            ts.Command = cmd;
                            TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
                            subsidio.desde = (baseGravableSubsidio / lstDatosNomina[i].dias) * 30.4;

                            double cantidad = 0;
                            cnx.Open();
                            cantidad = double.Parse(ts.obtenerCantidadSubsidio(subsidio).ToString());
                            cnx.Close();

                            subsidioNomina.cantidad = cantidad;
                        }
                        else
                            subsidioNomina.cantidad = 0;
                        
                        subsidioNomina.guardada = false;
                        subsidioNomina.tiponomina = tipoNomina;
                        subsidioNomina.modificado = false;
                        lstValoresNomina.Add(subsidioNomina);
                        break;
                    #endregion

                    #region OTRAS DEDUCCIONES
                    default:
                        double sueldoDeducciones = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);

                        CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                        vn.idtrabajador = lstDatosNomina[i].idtrabajador;
                        vn.idempresa = GLOBALES.IDEMPRESA;
                        vn.idconcepto = lstDatosNomina[i].id;
                        vn.noconcepto = lstDatosNomina[i].noconcepto;
                        vn.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                        vn.fechainicio = inicio.Date;
                        vn.fechafin = fin.Date;
                        vn.guardada = false;
                        vn.tiponomina = tipoNomina;
                        vn.modificado = false;

                        #region SUELDO DIFERENTE DE CERO
                        if (sueldoDeducciones != 0)
                        {
                            lstDatoTrabajador = new List<CalculoNomina.Core.Nomina>();
                            CalculoNomina.Core.Nomina nt = new CalculoNomina.Core.Nomina();
                            nt.idtrabajador = lstDatosNomina[i].idtrabajador;
                            nt.dias = lstDatosNomina[i].dias;
                            nt.salariominimo = lstDatosNomina[i].salariominimo;
                            nt.antiguedadmod = lstDatosNomina[i].antiguedadmod;
                            nt.sdi = lstDatosNomina[i].sdi;
                            nt.sd = lstDatosNomina[i].sd;
                            nt.id = lstDatosNomina[i].id;
                            nt.noconcepto = lstDatosNomina[i].noconcepto;
                            nt.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                            nt.formula = lstDatosNomina[i].formula;
                            nt.formulaexento = lstDatosNomina[i].formulaexento;
                            lstDatoTrabajador.Add(nt);

                            Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                            infh.Command = cmd;

                            Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                            inf.idtrabajador = lstDatosNomina[i].idtrabajador;
                            inf.idempresa = GLOBALES.IDEMPRESA;

                            if (lstDatosNomina[i].noconcepto == 10 || lstDatosNomina[i].noconcepto == 11 || lstDatosNomina[i].noconcepto == 12)
                            {
                                cnx.Open();
                                activoInfonavit = (bool)infh.activoInfonavit(inf);
                                cnx.Close();
                            }

                            FormulasValores f = new FormulasValores(lstDatoTrabajador, inicio.Date, fin.Date);
                            vn.cantidad = double.Parse(f.calcularFormula().ToString());
                            vn.exento = double.Parse(f.calcularFormulaExento().ToString());
                            vn.gravado = double.Parse(f.calcularFormula().ToString()) - double.Parse(f.calcularFormulaExento().ToString());

                            switch (lstDatosNomina[i].noconcepto)
                            {
                                case 10:

                                    if (!activoInfonavit)
                                    {
                                        vn.cantidad = 0;
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    else
                                    {
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    lstValoresNomina.Add(vn);
                                    break;
                                case 11:
                                    if (!activoInfonavit)
                                    {
                                        vn.cantidad = 0;
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    else
                                    {
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    lstValoresNomina.Add(vn);
                                    break;
                                case 12:
                                    if (!activoInfonavit)
                                    {
                                        vn.cantidad = 0;
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    else
                                    {
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    lstValoresNomina.Add(vn);
                                    break;
                                default:
                                    lstValoresNomina.Add(vn);
                                    break;
                            }
                        }
                        #endregion
                        else
                        {
                            vn.cantidad = 0;
                            vn.exento = 0;
                            vn.gravado = 0;
                            lstValoresNomina.Add(vn);
                        }
                        break;
                    #endregion
                }
            }
            #endregion

            return lstValoresNomina;
        }


        public static void RECALCULO_PERCEPCIONES(List<CalculoNomina.Core.NominaRecalculo> lstDatosNomina,
           DateTime inicio, DateTime fin, int tipoNomina)
        {
            #region VARIABLES
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            CalculoNomina.Core.NominaHelper nh;
            #endregion

            #region LISTA PARA DATOS DEL TRABAJADOR
            List<CalculoNomina.Core.NominaRecalculo> lstDatoTrabajador;
            #endregion

            #region CALCULO
            for (int i = 0; i < lstDatosNomina.Count; i++)
            {
                if (!lstDatosNomina[i].modificado)
                {
                    nh = new CalculoNomina.Core.NominaHelper();
                    nh.Command = cmd;

                    CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                    vn.id = lstDatosNomina[i].id;
                    vn.idtrabajador = lstDatosNomina[i].idtrabajador;
                    vn.idempresa = GLOBALES.IDEMPRESA;
                    vn.idconcepto = lstDatosNomina[i].idconcepto;
                    vn.noconcepto = lstDatosNomina[i].noconcepto;
                    vn.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                    vn.fechainicio = inicio.Date;
                    vn.fechafin = fin.Date;
                    vn.guardada = false;
                    vn.tiponomina = tipoNomina;
                    vn.modificado = false;

                    lstDatoTrabajador = new List<CalculoNomina.Core.NominaRecalculo>();
                    CalculoNomina.Core.NominaRecalculo nt = new CalculoNomina.Core.NominaRecalculo();
                    nt.idtrabajador = lstDatosNomina[i].idtrabajador;
                    nt.dias = lstDatosNomina[i].dias;
                    nt.salariominimo = lstDatosNomina[i].salariominimo;
                    nt.antiguedadmod = lstDatosNomina[i].antiguedadmod;
                    nt.sdi = lstDatosNomina[i].sdi;
                    nt.sd = lstDatosNomina[i].sd;
                    nt.idconcepto = lstDatosNomina[i].idconcepto;
                    nt.noconcepto = lstDatosNomina[i].noconcepto;
                    nt.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                    nt.formula = lstDatosNomina[i].formula;
                    nt.formulaexento = lstDatosNomina[i].formulaexento;
                    lstDatoTrabajador.Add(nt);

                    FormulasValores f = new FormulasValores(lstDatoTrabajador, inicio.Date, fin.Date);
                    vn.cantidad = double.Parse(f.recalcularFormula().ToString());
                    vn.exento = double.Parse(f.recalcularFormulaExento().ToString());
                    vn.gravado = double.Parse(f.recalcularFormula().ToString()) - double.Parse(f.recalcularFormulaExento().ToString());

                    switch (lstDatosNomina[i].noconcepto)
                    {
                        case 1:
                            if (vn.cantidad == 0)
                            {
                                i++;
                                vn.gravado = 0;

                                cnx.Open();
                                nh.actualizaConcepto(vn);
                                cnx.Close();

                                int contadorDatosNomina = i;
                                for (int j = i; j < lstDatosNomina.Count; j++)
                                {
                                    contadorDatosNomina = j;
                                    if (lstDatosNomina[j].idtrabajador == vn.idtrabajador)
                                    {
                                        CalculoNomina.Core.tmpPagoNomina vnCero = new CalculoNomina.Core.tmpPagoNomina();
                                        vnCero.id = lstDatosNomina[j].id;
                                        vnCero.idtrabajador = lstDatosNomina[j].idtrabajador;
                                        vnCero.idempresa = GLOBALES.IDEMPRESA;
                                        vnCero.idconcepto = lstDatosNomina[j].idconcepto;
                                        vnCero.noconcepto = lstDatosNomina[j].noconcepto;
                                        vnCero.tipoconcepto = lstDatosNomina[j].tipoconcepto;
                                        vnCero.fechainicio = inicio.Date;
                                        vnCero.fechafin = fin.Date;
                                        vnCero.guardada = false;
                                        vnCero.tiponomina = tipoNomina;
                                        vnCero.modificado = false;
                                        vnCero.cantidad = 0;
                                        vnCero.exento = 0;
                                        vnCero.gravado = 0;

                                        cnx.Open();
                                        nh.actualizaConcepto(vnCero);
                                        cnx.Close();
                                    }
                                    else
                                    {
                                        --contadorDatosNomina;
                                        break;
                                    }
                                }
                                i = contadorDatosNomina;
                            }
                            else
                            {
                                cnx.Open();
                                nh.actualizaConcepto(vn);
                                cnx.Close();
                            }
                            break;
                        case 2: // HORAS EXTRAS DOBLES
                            if (vn.cantidad <= vn.exento)
                            {
                                vn.exento = vn.cantidad;
                                vn.gravado = 0;
                            }
                            cnx.Open();
                            nh.actualizaConcepto(vn);
                            cnx.Close();
                            break;
                        case 3: // PREMIO DE ASISTENCIA
                            if (vn.cantidad <= vn.exento)
                            {
                                vn.exento = vn.cantidad;
                                vn.gravado = 0;
                            }
                            cnx.Open();
                            nh.actualizaConcepto(vn);
                            cnx.Close();
                            break;
                        case 5: // PREMIO DE PUNTUALIDAD
                            if (vn.cantidad <= vn.exento)
                            {
                                vn.exento = vn.cantidad;
                                vn.gravado = 0;
                            }
                            cnx.Open();
                            nh.actualizaConcepto(vn);
                            cnx.Close();
                            break;
                        default:
                            cnx.Open();
                            nh.actualizaConcepto(vn);
                            cnx.Close();
                            break;
                    }
                }
            }
            #endregion
        }

        public static void RECALCULO_ISR_SUBSIDIO(List<CalculoNomina.Core.NominaRecalculo> lstDatosNomina,
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
            List<CalculoNomina.Core.Nomina> lstDatoTrabajador;
            #endregion

            #region CALCULO
            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            for (int i = 0; i < lstDatosNomina.Count; i++)
            {
                switch (lstDatosNomina[i].noconcepto)
                {
                    #region CONCEPTO ISR ANTES DE SUBSIDIO
                    case 8:

                        double excedente = 0, ImpMarginal = 0, isr = 0;
                        List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
                        TablaIsr.Core.IsrHelper isrh = new TablaIsr.Core.IsrHelper();
                        isrh.Command = cmd;

                        CalculoNomina.Core.tmpPagoNomina isrAntesSubsidio = new CalculoNomina.Core.tmpPagoNomina();
                        isrAntesSubsidio.id = lstDatosNomina[i].id;
                        isrAntesSubsidio.idtrabajador = lstDatosNomina[i].idtrabajador;
                        isrAntesSubsidio.idempresa = GLOBALES.IDEMPRESA;
                        isrAntesSubsidio.idconcepto = lstDatosNomina[i].id;
                        isrAntesSubsidio.noconcepto = lstDatosNomina[i].noconcepto;
                        isrAntesSubsidio.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                        isrAntesSubsidio.fechainicio = inicio.Date;
                        isrAntesSubsidio.fechafin = fin.Date;
                        isrAntesSubsidio.exento = 0;
                        isrAntesSubsidio.gravado = 0;

                        double sueldo = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);
                        if (sueldo != 0)
                        {
                            double baseGravableIsr = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador).Sum(e => e.gravado);

                            TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
                            _isr.inferior = (baseGravableIsr / lstDatosNomina[i].dias) * 30.4;

                            cnx.Open();
                            lstIsr = isrh.isrCorrespondiente(_isr);
                            cnx.Close();

                            excedente = ((baseGravableIsr / lstDatosNomina[i].dias) * 30.4) - lstIsr[0].inferior;
                            ImpMarginal = excedente * (lstIsr[0].porcentaje / 100);
                            isr = ImpMarginal + lstIsr[0].cuota;

                            isrAntesSubsidio.cantidad = isr;
                        }
                        else
                            isrAntesSubsidio.cantidad = 0;

                        isrAntesSubsidio.guardada = false;
                        isrAntesSubsidio.tiponomina = tipoNomina;
                        isrAntesSubsidio.modificado = false;

                        cnx.Open();
                        nh.actualizaConcepto(isrAntesSubsidio);
                        cnx.Close();
                        break;
                    #endregion

                    #region SUBSIDIO
                    case 15:
                        double sueldoSubsidio = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);

                        CalculoNomina.Core.tmpPagoNomina subsidioNomina = new CalculoNomina.Core.tmpPagoNomina();
                        subsidioNomina.id = lstDatosNomina[i].id;
                        subsidioNomina.idtrabajador = lstDatosNomina[i].idtrabajador;
                        subsidioNomina.idempresa = GLOBALES.IDEMPRESA;
                        subsidioNomina.idconcepto = lstDatosNomina[i].id;
                        subsidioNomina.noconcepto = lstDatosNomina[i].noconcepto;
                        subsidioNomina.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                        subsidioNomina.fechainicio = inicio.Date;
                        subsidioNomina.fechafin = fin.Date;
                        subsidioNomina.exento = 0;
                        subsidioNomina.gravado = 0;

                        if (sueldoSubsidio != 0)
                        {
                            double baseGravableSubsidio = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador).Sum(e => e.gravado);

                            TablaSubsidio.Core.SubsidioHelper ts = new TablaSubsidio.Core.SubsidioHelper();
                            ts.Command = cmd;
                            TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
                            subsidio.desde = (baseGravableSubsidio / lstDatosNomina[i].dias) * 30.4;

                            double cantidad = 0;
                            cnx.Open();
                            cantidad = double.Parse(ts.obtenerCantidadSubsidio(subsidio).ToString());
                            cnx.Close();

                            subsidioNomina.cantidad = cantidad;
                        }
                        else
                            subsidioNomina.cantidad = 0;

                        subsidioNomina.guardada = false;
                        subsidioNomina.tiponomina = tipoNomina;
                        subsidioNomina.modificado = false;
                        cnx.Open();
                        nh.actualizaConcepto(subsidioNomina);
                        cnx.Close();
                        break;
                    #endregion

                    #region OTRAS DEDUCCIONES
                    default:
                        double sueldoDeducciones = lstPercepciones.Where(e => e.idtrabajador == lstDatosNomina[i].idtrabajador && e.noconcepto == 1).Sum(e => e.cantidad);

                        CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                        vn.idtrabajador = lstDatosNomina[i].id;
                        vn.idtrabajador = lstDatosNomina[i].idtrabajador;
                        vn.idempresa = GLOBALES.IDEMPRESA;
                        vn.idconcepto = lstDatosNomina[i].id;
                        vn.noconcepto = lstDatosNomina[i].noconcepto;
                        vn.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                        vn.fechainicio = inicio.Date;
                        vn.fechafin = fin.Date;
                        vn.guardada = false;
                        vn.tiponomina = tipoNomina;
                        vn.modificado = false;

                        #region SUELDO DIFERENTE DE CERO
                        if (sueldoDeducciones != 0)
                        {
                            lstDatoTrabajador = new List<CalculoNomina.Core.Nomina>();
                            CalculoNomina.Core.Nomina nt = new CalculoNomina.Core.Nomina();
                            nt.idtrabajador = lstDatosNomina[i].idtrabajador;
                            nt.dias = lstDatosNomina[i].dias;
                            nt.salariominimo = lstDatosNomina[i].salariominimo;
                            nt.antiguedadmod = lstDatosNomina[i].antiguedadmod;
                            nt.sdi = lstDatosNomina[i].sdi;
                            nt.sd = lstDatosNomina[i].sd;
                            nt.id = lstDatosNomina[i].id;
                            nt.noconcepto = lstDatosNomina[i].noconcepto;
                            nt.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                            nt.formula = lstDatosNomina[i].formula;
                            nt.formulaexento = lstDatosNomina[i].formulaexento;
                            lstDatoTrabajador.Add(nt);

                            Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                            infh.Command = cmd;

                            Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                            inf.idtrabajador = lstDatosNomina[i].idtrabajador;
                            inf.idempresa = GLOBALES.IDEMPRESA;

                            if (lstDatosNomina[i].noconcepto == 10 || lstDatosNomina[i].noconcepto == 11 || lstDatosNomina[i].noconcepto == 12)
                            {
                                cnx.Open();
                                activoInfonavit = (bool)infh.activoInfonavit(inf);
                                cnx.Close();
                            }

                            FormulasValores f = new FormulasValores(lstDatoTrabajador, inicio.Date, fin.Date);
                            vn.cantidad = double.Parse(f.calcularFormula().ToString());
                            vn.exento = double.Parse(f.calcularFormulaExento().ToString());
                            vn.gravado = double.Parse(f.calcularFormula().ToString()) - double.Parse(f.calcularFormulaExento().ToString());

                            switch (lstDatosNomina[i].noconcepto)
                            {
                                case 10:

                                    if (!activoInfonavit)
                                    {
                                        vn.cantidad = 0;
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    else
                                    {
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    cnx.Open();
                                    nh.actualizaConcepto(vn);
                                    cnx.Close();
                                    break;
                                case 11:
                                    if (!activoInfonavit)
                                    {
                                        vn.cantidad = 0;
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    else
                                    {
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    cnx.Open();
                                    nh.actualizaConcepto(vn);
                                    cnx.Close();
                                    break;
                                case 12:
                                    if (!activoInfonavit)
                                    {
                                        vn.cantidad = 0;
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    else
                                    {
                                        vn.exento = 0;
                                        vn.gravado = 0;
                                    }
                                    cnx.Open();
                                    nh.actualizaConcepto(vn);
                                    cnx.Close();
                                    break;
                                default:
                                    cnx.Open();
                                    nh.actualizaConcepto(vn);
                                    cnx.Close();
                                    break;
                            }
                        }
                        #endregion
                        else
                        {
                            vn.cantidad = 0;
                            vn.exento = 0;
                            vn.gravado = 0;
                            cnx.Open();
                            nh.actualizaConcepto(vn);
                            cnx.Close();
                        }
                        break;
                    #endregion
                }
            }
            #endregion
        }
    }
}
