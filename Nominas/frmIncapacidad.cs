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
    public partial class frmIncapacidad : Form
    {
        public frmIncapacidad()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado = 0;
        public string _nombreEmpleado;
        public int _idIncapacidad;
        public int _tipoOperacion;
        public int _tipoForma;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Periodos.Core.PeriodosHelper ph;
        Incapacidad.Core.IncapacidadHelper ih;
        Faltas.Core.FaltasHelper fh;
        int periodo, idperiodo;
        #endregion

        #region DELEGADOS
        public delegate void delOnIncapacidad(string noempleado, string nombre, string paterno, string materno, int dias, DateTime fechainicio, DateTime inicio, DateTime fin);
        public event delOnIncapacidad OnIncapacidad;
        #endregion

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.OnBuscar += b_OnBuscar;
            b._catalogo = GLOBALES.EMPLEADOS;
            b.ShowDialog();
        }

        void b_OnBuscar(int id, string nombre)
        {
            _idEmpleado = id;
            _nombreEmpleado = nombre;
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
            empleado.idtrabajador = _idEmpleado;

            Periodos.Core.Periodos per = new Periodos.Core.Periodos();
            per.idempresa = GLOBALES.IDEMPRESA;

            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
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
                dtpInicioPeriodo.Value = dt;
                dtpFinPeriodo.Value = dt.AddDays(6);
            }
            else
            {
                if (DateTime.Now.Day <= 15)
                {
                    dtpInicioPeriodo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtpFinPeriodo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                }
                else
                {
                    dtpInicioPeriodo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16);
                    dtpFinPeriodo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                }

            }
        }

        private void frmIncapacidad_Load(object sender, EventArgs e)
        {
            dtpFinPeriodo.Enabled = false;
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            if (_idEmpleado == 0)
            {
                MessageBox.Show("No ha seleccionado al Empleado.", "Información");
                return;
            }
            //SE VALIDA SI TODOS LOS CAMPOS HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            double diastomados = 0, diasrestantes = 0, diasapagar = 0;
            string fechaII = dtpFechaInicio.Value.ToShortDateString();
            DateTime fechaInicioIncapacidad = DateTime.Parse(fechaII);
            DateTime fechaFinIncapacidad = fechaInicioIncapacidad.AddDays(double.Parse(txtDiasIncapacidad.Text) - 1);
            if (fechaFinIncapacidad <= dtpFinPeriodo.Value)
                diastomados = double.Parse(txtDiasIncapacidad.Text);
            else
            {
                diastomados = (dtpFinPeriodo.Value - DateTime.Parse(fechaII)).TotalDays + 1;
                diasrestantes = double.Parse(txtDiasIncapacidad.Text) - diastomados;
            }

            if (double.Parse(txtDiasIncapacidad.Text) > 3)
            {
                if (diastomados <= 3)
                    diasapagar = 3 - diastomados;
                else
                    diasapagar = 0;
            }
            else 
            {
                diasapagar = 3 - diastomados;
            }

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ih = new Incapacidad.Core.IncapacidadHelper();
            ih.Command = cmd;
            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Incapacidad.Core.Incapacidades incapacidad = new Incapacidad.Core.Incapacidades();
            incapacidad.idtrabajador = _idEmpleado;
            incapacidad.idempresa = GLOBALES.IDEMPRESA;
            incapacidad.diasincapacidad = int.Parse(txtDiasIncapacidad.Text);
            incapacidad.diastomados = (int)diastomados;
            incapacidad.diasrestantes = (int)diasrestantes;
            incapacidad.diasapagar = (int)diasapagar;
            incapacidad.tipo = 0;
            incapacidad.aplicada = 0;
            incapacidad.consecutiva = 1;
            incapacidad.fechainicio = dtpFechaInicio.Value;
            incapacidad.fechafin = DateTime.Parse(fechaII).AddDays(diastomados - 1);

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idEmpleado;

            List<Empleados.Core.Empleados> lstEmpleado;

            switch (_tipoForma)
            {
                case 0://ALTA EN BASE DE DATOS
                    #region CHEQUEO DE FALTAS
                    int existeFalta = 0;
                    Faltas.Core.Faltas faltas = new Faltas.Core.Faltas();
                    faltas.idtrabajador = _idEmpleado;
                    faltas.fechainicio = dtpInicioPeriodo.Value;
                    faltas.fechafin = dtpFinPeriodo.Value;
                    try
                    {
                        cnx.Open();
                        existeFalta = (int)fh.existeFalta(faltas);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al verificar existencia de faltas. \r\n \r\n Descripcion: " + error.Message, "Error");
                        this.Dispose();
                    }
                    #endregion

                    try
                    {
                        if (!existeFalta.Equals(0))
                        {
                            cnx.Open();
                            fh.eliminaFaltaExistente(faltas);
                            ih.insertaIncapacidad(incapacidad);
                            cnx.Close();
                            cnx.Dispose();
                        }
                        if (OnIncapacidad != null)
                            OnIncapacidad(null, null, null, null, 0, DateTime.Now, DateTime.Now, DateTime.Now);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar la incapacidad. \r\n \r\n Descripcion: " + error.Message, "Error");
                    }
                    break;
                case 1://ALTA EN DATAGRIDVIEW
                    try
                    {
                        cnx.Open();
                        lstEmpleado = eh.obtenerEmpleado(empleado);
                        cnx.Close();
                        cnx.Dispose();

                        if (OnIncapacidad != null)
                            OnIncapacidad(lstEmpleado[0].noempleado, 
                                lstEmpleado[0].nombres, 
                                lstEmpleado[0].paterno, 
                                lstEmpleado[0].materno, 
                                int.Parse(txtDiasIncapacidad.Text), 
                                dtpFechaInicio.Value, 
                                dtpInicioPeriodo.Value, 
                                dtpFinPeriodo.Value);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar la incapacidad. \r\n \r\n Descripcion: " + error.Message, "Error");
                    }
                    break;
            }
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if (periodo == 7)
            {
                DateTime dt = dtpInicioPeriodo.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicioPeriodo.Value = dt;
                dtpFinPeriodo.Value = dt.AddDays(6);
            }
            else
            {
                if (DateTime.Now.Day <= 15)
                {
                    dtpInicioPeriodo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtpFinPeriodo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                }
                else
                {
                    dtpInicioPeriodo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16);
                    dtpFinPeriodo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                }
            }
        }
    }
}
