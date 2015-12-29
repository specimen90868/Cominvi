using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmVacaciones : Form
    {
        public frmVacaciones()
        {
            InitializeComponent();
        }

        #region DELEGADOS
        public delegate void delOnVacacion(string noempleado, string nombre, string paterno, string materno, bool prima, bool pago, int diaspagopv, 
            bool vacacion, int diaspago, DateTime fechainicio, DateTime fechafin);
        public event delOnVacacion OnVacacion;

        public delegate void delOnVacacionNueva();
        public event delOnVacacionNueva OnVacacionNueva;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Periodos.Core.PeriodosHelper ph;
        Conceptos.Core.ConceptosHelper ch;
        Vacaciones.Core.VacacionesHelper vh;
        TablaIsr.Core.IsrHelper ih;
        int periodo, idperiodo, idempleado = 0;
        List<Empleados.Core.Empleados> lstEmpleado;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNomina;
        public string _ventana;
        #endregion

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            if (_ventana == "Carga")
            {
                if (idempleado != 0)
                    if (OnVacacion != null)
                        OnVacacion(
                            lstEmpleado[0].noempleado,
                            lstEmpleado[0].nombres,
                            lstEmpleado[0].paterno,
                            lstEmpleado[0].materno,
                            chkPrimaVacacional.Checked,
                            chkPagoTotal.Checked,
                            int.Parse(txtDiasPagoPV.Text),
                            chkVacaciones.Checked,
                            int.Parse(txtDiasPago.Text),
                            dtpInicio.Value,
                            dtpFin.Value);
            }
            else
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;


                #region CALCULO DE PRIMA VACACIONAL
                if (chkPrimaVacacional.Checked)
                {
                    #region DATOS DEL EMPLEADO
                    eh = new Empleados.Core.EmpleadosHelper();
                    eh.Command = cmd;
                    Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                    empleado.idtrabajador = idempleado;

                    List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
                    try
                    {
                        cnx.Open();
                        lstEmpleado = eh.obtenerEmpleado(empleado);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    }
                    #endregion

                    #region CALCULO PRIMA VACACIONAL
                    ch = new Conceptos.Core.ConceptosHelper();
                    ch.Command = cmd;
                    Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                    concepto.idempresa = GLOBALES.IDEMPRESA;

                    concepto.noconcepto = 4; //PRIMA VACACIONAL
                    string formulaPrimaVacacional = "";
                    string formulaExentoPrimaVacacional = "";
                    double exento = 0;
                    
                    try
                    {
                        cnx.Open();
                        formulaPrimaVacacional = (string)ch.obtenerFormula(concepto);
                        formulaExentoPrimaVacacional = (string)ch.obtenerFormulaExento(concepto);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al obtener formula de Prima Vacacional. \r\n \r\n" + error.Message, "Error");
                    }

                    List<Empleados.Core.Empleados> lstEmpleadoCalculo;
                    
                    lstEmpleadoCalculo = new List<Empleados.Core.Empleados>();
                    Empleados.Core.Empleados emp = new Empleados.Core.Empleados();
                    emp.idtrabajador = lstEmpleado[0].idtrabajador;
                    emp.idsalario = lstEmpleado[0].idsalario;
                    emp.idperiodo = lstEmpleado[0].idperiodo;
                    emp.antiguedadmod = lstEmpleado[0].antiguedadmod;
                    emp.sdi = lstEmpleado[0].sdi;
                    emp.sd = lstEmpleado[0].sd;
                    emp.fechaantiguedad = lstEmpleado[0].fechaantiguedad;
                    emp.sueldo = lstEmpleado[0].sueldo;
                    lstEmpleadoCalculo.Add(emp);

                    Vacaciones.Core.Vacaciones prima = new Vacaciones.Core.Vacaciones();
                    prima.idtrabajador = lstEmpleado[0].idtrabajador;
                    prima.idempresa = GLOBALES.IDEMPRESA;
                    prima.fechaingreso = lstEmpleado[0].fechaantiguedad;
                    prima.inicio = dtpInicio.Value.Date;
                    prima.fin = dtpFin.Value.Date;
                    prima.sd = lstEmpleado[0].sd;
                    prima.diasapagar = 0;
                    prima.diaspendientes = 0;
                    prima.pagovacaciones = 0;
                    prima.fechapago = DateTime.Now;
                    prima.pagada = false;
                    prima.pvpagada = true;

                    vh = new Vacaciones.Core.VacacionesHelper();
                    vh.Command = cmd;
                    Vacaciones.Core.DiasDerecho dias = new Vacaciones.Core.DiasDerecho();
                    dias.anio = lstEmpleado[0].antiguedadmod;

                    try
                    {
                        cnx.Open();
                        prima.diasderecho = (int)vh.diasDerecho(dias);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error asl obtener los dias por derecho. \r\n \r\n" + error.Message, "Error");
                    }

                    //if (chkPagoTotal.Checked)
                    //{
                    //    FormulasValores f = new FormulasValores(formulaPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now);
                    //    prima.pv = (double)f.calcularFormulaVacaciones();
                    //    prima.diasapagar = prima.diasderecho;

                    //    f = new FormulasValores(formulaExentoPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now);
                    //    exento = (double)f.calcularFormulaVacacionesExento();
                    //}
                    //else
                    //{
                    //    FormulasValores f = new FormulasValores(formulaPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now, int.Parse(txtDiasPagoPV.Text));
                    //    prima.pv = (double)f.calcularFormulaVacaciones();
                    //    prima.diasapagar = int.Parse(txtDiasPagoPV.Text);
                    //    prima.diaspendientes = prima.diasderecho - prima.diasapagar;

                    //    f = new FormulasValores(formulaExentoPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now, int.Parse(txtDiasPagoPV.Text));
                    //    exento = (double)f.calcularFormulaVacacionesExento();
                    //}

                    if (prima.pv > exento)
                    {
                        prima.pexenta = exento;
                        prima.pgravada = prima.pv - exento;
                    }
                    else
                    {
                        prima.pexenta = prima.pv;
                        prima.pgravada = 0;
                    }

                    prima.isrgravada = isr(prima.pgravada, lstEmpleado[0].idperiodo, lstEmpleado[0].sd);
                    prima.totalprima = prima.pv - prima.isrgravada;
                    prima.total = prima.pv - prima.isrgravada;

                    try
                    {
                        cnx.Open();
                        vh.insertaVacacion(prima);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: Inserción de Prima Vacacional. \r\n \r\n" + error.Message, "Error");
                    }
                    
                    #endregion

                }
                #endregion

                #region CALCULO DE VACACIONES
                if (chkVacaciones.Checked)
                {
                    #region DATOS DEL EMPLEADO
                    eh = new Empleados.Core.EmpleadosHelper();
                    eh.Command = cmd;

                    Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                    empleado.idtrabajador = idempleado;

                    List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
                    try
                    {
                        cnx.Open();
                        lstEmpleado = eh.obtenerEmpleado(empleado);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    }
                    #endregion

                    #region CALCULO DE VACACIONES
                    Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
                    vacacion.idtrabajador = lstEmpleado[0].idtrabajador;
                    vacacion.idempresa = GLOBALES.IDEMPRESA;
                    vacacion.fechaingreso = lstEmpleado[0].fechaantiguedad;
                    vacacion.inicio = dtpInicio.Value.Date;
                    vacacion.fin = dtpFin.Value.Date;
                    vacacion.sd = lstEmpleado[0].sd;
                    vacacion.diasapagar = int.Parse(txtDiasPago.Text);

                    vh = new Vacaciones.Core.VacacionesHelper();
                    vh.Command = cmd;
                    Vacaciones.Core.DiasDerecho dias = new Vacaciones.Core.DiasDerecho();
                    dias.anio = lstEmpleado[0].antiguedadmod;
                    
                    try
                    {
                        cnx.Open();
                        vacacion.diasderecho = (int)vh.diasDerecho(dias);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al obtener los dias por derecho (Vacaciones). \r\n \r\n" + error.Message, "Error");
                    }

                    vacacion.diaspendientes = vacacion.diasderecho - vacacion.diasapagar;
                    vacacion.pv = 0;
                    vacacion.pexenta = 0;
                    vacacion.pgravada = 0;
                    vacacion.isrgravada = 0;
                    vacacion.pagovacaciones = lstEmpleado[0].sd * int.Parse(txtDiasPago.Text);
                    vacacion.totalprima = 0;
                    vacacion.total = lstEmpleado[0].sd * int.Parse(txtDiasPago.Text);
                    vacacion.fechapago = DateTime.Now;
                    vacacion.pagada = true;
                    vacacion.pvpagada = false;

                    try
                    {
                        cnx.Open();
                        vh.insertaVacacion(vacacion);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: Inserción de vacaciones. \r\n \r\n" + error.Message, "Error");
                    }

                    #endregion
                }
                #endregion

                if (OnVacacionNueva != null)
                    OnVacacionNueva();
                cnx.Dispose();
                this.Dispose();
            }
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.OnBuscar += b_OnBuscar;
            b._catalogo = GLOBALES.EMPLEADOS;
            b._tipoNomina = _tipoNomina;
            b.MdiParent = this.MdiParent;
            b.Show();
        }

        void b_OnBuscar(int id, string nombre)
        {
            idempleado = id;
            lblEmpleado.Text = nombre;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            ph = new Periodos.Core.PeriodosHelper();
            eh.Command = cmd;
            ph.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idempleado;

            Periodos.Core.Periodos per = new Periodos.Core.Periodos();
            per.idempresa = GLOBALES.IDEMPRESA;

            lstEmpleado = new List<Empleados.Core.Empleados>();
            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                lstPeriodos = ph.obtenerPeriodos(per);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var datos = from e in lstEmpleado
                        join p in lstPeriodos on e.idperiodo equals p.idperiodo
                        select new
                        {
                            p.dias,
                            e.idperiodo
                        };
            foreach (var d in datos)
            {
                periodo = d.dias;
                idperiodo = d.idperiodo;
            }

            if (periodo == 7)
            {
                DateTime dt = DateTime.Now;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicio.Value = dt;
                dtpFin.Value = dt.AddDays(6);
            }
            else
            {
                if (DateTime.Now.Day <= 15)
                {
                    dtpInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtpFin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                }
                else
                {
                    dtpInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16);
                    dtpFin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                }

            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmVacaciones_Load(object sender, EventArgs e)
        {
            dtpFin.Enabled = false;
            txtDiasPago.Text = "0";
            txtDiasPagoPV.Text = "0";
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if (periodo == 7)
            {
                DateTime dt = dtpInicio.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicio.Value = dt;
                dtpFin.Value = dt.AddDays(6);
            }
            else
            {
                if (DateTime.Now.Day <= 15)
                {
                    dtpInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtpFin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                }
                else
                {
                    dtpInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16);
                    dtpFin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                }
            }
        }

        private void chkVacaciones_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkVacaciones.Checked)
                txtDiasPago.Text = "0";
        }

        private void chkPrimaVacacional_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrimaVacacional.Checked)
            {
                chkPagoTotal.Checked = false;
                chkPagoTotal.Enabled = true;
                txtDiasPagoPV.Enabled = true;
            }
            else
            {
                chkPagoTotal.Checked = false;
                txtDiasPagoPV.Text = "0";
                chkPagoTotal.Enabled = false;
                txtDiasPagoPV.Enabled = false;
            }
        }

        private void chkPagoTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPagoTotal.Checked)
            {
                txtDiasPagoPV.Text = "0";
                txtDiasPagoPV.Enabled = false;
            }
            else
            {
                txtDiasPagoPV.Text = "0";
                txtDiasPagoPV.Enabled = true;
            }
        }

        private double isr(double pgravada, int idperiodo, double sd)
        {
            double excedente = 0, ImpMarginal = 0, isrDefinitivo = 0;
            double isr1 = 0, isr2 = 0, valor1 = 0, valor2 = 0, valor3 = 0, valor4 = 0;
            int diasPeriodo = 0;

            ih = new TablaIsr.Core.IsrHelper();
            ph = new Periodos.Core.PeriodosHelper();
            ih.Command = cmd;
            ph.Command = cmd;
            List<TablaIsr.Core.TablaIsr> lstIsr1;
            List<TablaIsr.Core.TablaIsr> lstIsr2;

            if (!pgravada.Equals(0))
            {
                TablaIsr.Core.TablaIsr tablaIsr = new TablaIsr.Core.TablaIsr();
                Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
                periodo.idperiodo = idperiodo;
                try
                {
                    cnx.Open();
                    diasPeriodo = (int)ph.DiasDePago(periodo);
                    cnx.Close();
                }
                catch (Exception error)
                { MessageBox.Show("Error (Periodo): \r\n \r\n" + error.Message, "Error"); cnx.Dispose(); this.Dispose(); }

                valor1 = ((pgravada / sd) * 30.4) +
                    ((new DateTime(DateTime.Now.Year, 12, 31) - new DateTime(DateTime.Now.Year, 1, 1)).TotalDays * 30);
                tablaIsr.periodo = diasPeriodo;
                tablaIsr.inferior = valor1;
                try
                {
                    cnx.Open();
                    lstIsr1 = ih.isrCorrespondiente(tablaIsr);
                    excedente = valor1 - lstIsr1[0].inferior;
                    ImpMarginal = excedente * (lstIsr1[0].porcentaje / 100);
                    isr1 = ImpMarginal + lstIsr1[0].cuota;
                    cnx.Close();
                }
                catch (Exception error)
                { MessageBox.Show("Error (Isr): \r\n \r\n" + error.Message, "Error"); cnx.Dispose(); this.Dispose(); }

                tablaIsr = new TablaIsr.Core.TablaIsr();
                valor2 = ((new DateTime(DateTime.Now.Year, 12, 31) - new DateTime(DateTime.Now.Year, 1, 1)).TotalDays * 30);
                tablaIsr.periodo = diasPeriodo;
                tablaIsr.inferior = valor2;

                try
                {
                    cnx.Open();
                    lstIsr2 = ih.isrCorrespondiente(tablaIsr);
                    cnx.Close();
                    excedente = valor2 - lstIsr2[0].inferior;
                    ImpMarginal = excedente * (lstIsr2[0].porcentaje / 100);
                    isr2 = ImpMarginal + lstIsr2[0].cuota;
                }
                catch (Exception error)
                { MessageBox.Show("Error (Isr): \r\n \r\n" + error.Message, "Error"); cnx.Dispose(); this.Dispose(); }

                valor3 = isr1 - isr2;
                valor4 = valor3 / ((pgravada / sd) * 30.4);
                isrDefinitivo = valor4 * pgravada;
            }
            else
            {
                isrDefinitivo = 0;
            }

            return isrDefinitivo;
        }
    }
}
