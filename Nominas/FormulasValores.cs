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
        private string variable;
        private List<Empleados.Core.Empleados> empleados;
        private DateTime fechainicio, fechafin;
        private int periodo;

        public FormulasValores(string _dato, List<Empleados.Core.Empleados> _empleados)
        {
            variable = _dato;
            empleados = _empleados;
        }

        public FormulasValores(string _dato, List<Empleados.Core.Empleados> _empleados, int _periodo)
        {
            variable = _dato;
            empleados = _empleados;
            periodo = _periodo;
        }

        public FormulasValores(string _dato, List<Empleados.Core.Empleados> _empleados, DateTime _fechainicio, DateTime _fechafin, int _periodo)
        {
            variable = _dato;
            empleados = _empleados;
            fechainicio = _fechainicio;
            fechafin = _fechafin;
            periodo = _periodo;
        }

        public List<string> ObtenerValorVariable()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            List<string> valores = new List<string>();
            
            switch(variable)
            {
                case "SueldoDiario":
                    List<Empleados.Core.Empleados> lstEmpleado;
                    Empleados.Core.EmpleadosHelper empleadohp = new Empleados.Core.EmpleadosHelper();
                    empleadohp.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                        empleado.idtrabajador = empleados[i].idtrabajador;
                        cnx.Open();
                        lstEmpleado = empleadohp.obtenerEmpleado(empleado);
                        cnx.Close();
                        valores.Add(lstEmpleado[0].sd.ToString());
                    }
                    cnx.Dispose();
                    break;
                case "SBC":
                    Empleados.Core.EmpleadosHelper sbc = new Empleados.Core.EmpleadosHelper();
                    sbc.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                        empleado.idtrabajador = empleados[i].idtrabajador;
                        cnx.Open();
                        valores.Add(sbc.obtenerSalarioDiarioIntegrado(empleado).ToString());
                        cnx.Close();
                    }
                    cnx.Dispose();
                    break;
                case "DiasDerechoV":
                    Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                    vh.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Vacaciones.Core.DiasDerecho diasDerecho = new Vacaciones.Core.DiasDerecho();
                        diasDerecho.anio = empleados[i].antiguedadmod;
                        cnx.Open();
                        valores.Add(vh.diasDerecho(diasDerecho).ToString());
                        cnx.Close();
                    }
                    cnx.Dispose();
                    break;
                case "SalarioMinimo":
                    List<Salario.Core.Salarios> salarios = new List<Salario.Core.Salarios>();
                    Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
                    sh.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        cnx.Open();
                        salarios = sh.obtenerSalario(new DateTime(DateTime.Now.Year, 1, 1), empleados[i].idsalario);
                        valores.Add(salarios[0].valor.ToString());
                        cnx.Close();
                    }
                    cnx.Dispose();
                    break;
                case "Faltas":
                    object noFaltas;
                    Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                    fh.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                        falta.idtrabajador = empleados[i].idtrabajador;
                        falta.fechainicio = fechainicio;
                        falta.fechafin = fechafin;
                        cnx.Open();
                        noFaltas = fh.existeFalta(falta);
                        valores.Add(noFaltas.ToString());
                        cnx.Close();
                    }
                    cnx.Dispose();
                    break;

                case "DiasLaborados":
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        if (periodo == 7)
                            valores.Add((7).ToString());
                        else if (periodo == 15)
                            valores.Add((15).ToString());
                    }
                    break;

                case "DiasIncapacidad":
                    object diasIncapacidad;
                    Incapacidad.Core.IncapacidadHelper ih = new Incapacidad.Core.IncapacidadHelper();
                    ih.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Incapacidad.Core.Incapacidades inc = new Incapacidad.Core.Incapacidades();
                        inc.idtrabajador = empleados[i].idtrabajador;
                        inc.fechainicio = fechainicio;
                        inc.fechafin = fechafin;
                        cnx.Open();
                        diasIncapacidad = ih.existeIncapacidad(inc);
                        valores.Add(diasIncapacidad.ToString());
                        cnx.Close();
                    }
                    cnx.Dispose();
                break;

                case "Infonavit":
                    Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                    infh.Command = cmd;
                    List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();
                    List<string> variables = new List<string>();
                    string formula = "";
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                        inf.idtrabajador = empleados[i].idtrabajador;
                        cnx.Open();
                        lstInfonavit = infh.obtenerInfonavit(inf);
                        cnx.Close();

                        if (!lstInfonavit.Count.Equals(0))
                        {
                            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
                            ch.Command = cmd;
                            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                            if (lstInfonavit[0].descuento == GLOBALES.dPORCENTAJE)
                                concepto.noconcepto = 10; //INFONAVIT PORCENTAJE

                            if (lstInfonavit[0].descuento == GLOBALES.dVSMDF)
                                concepto.noconcepto = 11; //INFONAVIT VSMDF

                            if (lstInfonavit[0].descuento == GLOBALES.dPESOS)
                                concepto.noconcepto = 12; //INFONAVIT FIJO

                            formula = ch.obtenerFormula(concepto).ToString();
                            variables = GLOBALES.EXTRAEVARIABLES(formula, "[", "]");

                            for (int j = 0; j < variables.Count; j++)
                            {
                                if (variables[i].Equals("ValorInfonavit"))
                                    formula = formula.Replace("[" + variables[j] + "]", lstInfonavit[0].valordescuento.ToString());
                                else
                                    formula = formula.Replace("[" + variables[j] + "]", ObtenerValorVariable(variables[j], empleados[i].idtrabajador).ToString());
                            }

                            MathParserTK.MathParser parser = new MathParserTK.MathParser();
                            valores.Add(parser.Parse(formula).ToString());
                        }
                        else
                        {
                            valores.Add("0");
                        }
                    }
                break;
            }
            return valores;
        }

        public object ObtenerValorVariable(string variable, int idempleado, int antiguedad = 0)
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            object valor = null;
            Empleados.Core.EmpleadosHelper empleadohp;
            Empleados.Core.Empleados emp;

            switch (variable)
            {
                case "SueldoDiario":
                    List<Empleados.Core.Empleados> lstEmpleado;
                    empleadohp = new Empleados.Core.EmpleadosHelper();
                    empleadohp.Command = cmd;
                    
                    emp = new Empleados.Core.Empleados();
                    emp.idtrabajador = idempleado;
                    cnx.Open();
                    lstEmpleado = empleadohp.obtenerEmpleado(emp);
                    cnx.Close();
                    valor = lstEmpleado[0].sd;
                    
                    cnx.Dispose();
                    break;

                case "SBC":
                    empleadohp = new Empleados.Core.EmpleadosHelper();
                    empleadohp.Command = cmd;
                    emp = new Empleados.Core.Empleados();
                    emp.idtrabajador = idempleado;
                    cnx.Open();
                    valor = empleadohp.obtenerSalarioDiarioIntegrado(emp).ToString();
                    cnx.Close();
                    cnx.Dispose();
                    break;

                case "DiasDerechoV":
                    Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                    vh.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Vacaciones.Core.DiasDerecho diasDerecho = new Vacaciones.Core.DiasDerecho();
                        diasDerecho.anio = antiguedad;
                        cnx.Open();
                        valor = vh.diasDerecho(diasDerecho).ToString();
                        cnx.Close();
                    }
                    cnx.Dispose();
                    break;

                case "SalarioMinimo":
                    List<Salario.Core.Salarios> salarios = new List<Salario.Core.Salarios>();
                    Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
                    sh.Command = cmd;
                    cnx.Open();
                    salarios = sh.obtenerSalario(new DateTime(DateTime.Now.Year, 1, 1), idempleado);
                    valor = salarios[0].valor.ToString();
                    cnx.Close();
                    cnx.Dispose();
                    break;

                case "Faltas":
                    object noFaltas;
                    Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                    fh.Command = cmd;
                    Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                    falta.idtrabajador = idempleado;
                    falta.fechainicio = fechainicio;
                    falta.fechafin = fechafin;
                    cnx.Open();
                    noFaltas = fh.existeFalta(falta);
                    valor = noFaltas.ToString();
                    cnx.Close();
                    cnx.Dispose();
                    break;

                case "DiasLaborados":
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        if (periodo == 7)
                            valor = (7).ToString();
                        else if (periodo == 15)
                            valor = (15).ToString();
                    }
                    break;

                case "DiasIncapacidad":
                    object diasIncapacidad;
                    Incapacidad.Core.IncapacidadHelper ih = new Incapacidad.Core.IncapacidadHelper();
                    ih.Command = cmd;
                    Incapacidad.Core.Incapacidades inc = new Incapacidad.Core.Incapacidades();
                    inc.idtrabajador = idempleado;
                    inc.fechainicio = fechainicio;
                    inc.fechafin = fechafin;
                    cnx.Open();
                    diasIncapacidad = ih.existeIncapacidad(inc);
                    valor = diasIncapacidad.ToString();
                    cnx.Close();
                    cnx.Dispose();
                    break;

                case "DiasMes":
                    int mes = DateTime.Now.Month;
                    switch (mes)
                    {
                        case 1:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 1) + DateTime.DaysInMonth(DateTime.Now.Year, 2);
                            break;
                        case 2:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 1) + DateTime.DaysInMonth(DateTime.Now.Year, 2);
                            break;
                        case 3:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 3) + DateTime.DaysInMonth(DateTime.Now.Year, 4);
                            break;
                        case 4:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 3) + DateTime.DaysInMonth(DateTime.Now.Year, 4);
                            break;
                        case 5:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 5) + DateTime.DaysInMonth(DateTime.Now.Year, 6);
                            break;
                        case 6:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 5) + DateTime.DaysInMonth(DateTime.Now.Year, 6);
                            break;
                        case 7:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 7) + DateTime.DaysInMonth(DateTime.Now.Year, 8);
                            break;
                        case 8:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 7) + DateTime.DaysInMonth(DateTime.Now.Year, 8);
                            break;
                        case 9:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 9) + DateTime.DaysInMonth(DateTime.Now.Year, 10);
                            break;
                        case 10:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 9) + DateTime.DaysInMonth(DateTime.Now.Year, 10);
                            break;
                        case 11:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 11) + DateTime.DaysInMonth(DateTime.Now.Year, 12);
                            break;
                        case 12:
                            valor = DateTime.DaysInMonth(DateTime.Now.Year, 11) + DateTime.DaysInMonth(DateTime.Now.Year, 12);
                            break;
                    }
                    break;
            }
            return valor;
        }
    }
}
