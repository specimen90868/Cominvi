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
    public partial class frmFaltas : Form
    {
        public frmFaltas()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado = 0;
        public string _nombreEmpleado;
        public int _idFalta;
        public int _tipoOperacion;
        public int _tipoForma;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Periodos.Core.PeriodosHelper ph;
        Faltas.Core.FaltasHelper fh;
        int periodo, idperiodo;
        #endregion

        #region DELEGADOS
        public delegate void delOnFaltas(string noempleado, string nombre, string paterno, string materno, int faltas, DateTime fechainicio, DateTime fechafin);
        public event delOnFaltas OnFaltas;
        #endregion

        private void frmFaltas_Load(object sender, EventArgs e)
        {
            dtpFin.Enabled = false;
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                fh = new Faltas.Core.FaltasHelper();
                fh.Command = cmd;

                Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                falta.id = _idFalta;
                List<Faltas.Core.Faltas> lstFalta;

                try
                {
                    cnx.Open();
                    lstFalta = fh.obtenerFalta(falta);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstFalta.Count; i++)
                    {
                        txtFaltas.Text = lstFalta[i].faltas.ToString();
                        dtpInicio.Value = lstFalta[i].fechainicio;
                        dtpFin.Value = lstFalta[i].fechafin;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolVentana.Text = "Consulta Falta";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(DateTimePicker));
                    toolGuardar.Enabled = false;
                    toolBuscar.Enabled = false;
                }
                else
                {
                    toolVentana.Text = "Edición Falta";
                    lblEmpleado.Text = _nombreEmpleado;
                    toolBuscar.Enabled = false;
                }
            }
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b._catalogo = GLOBALES.EMPLEADOS;

            if (rbtnNormal.Checked)
                b._tipoNomina = GLOBALES.NORMAL;
            if (rbtnEspecial.Checked)
                b._tipoNomina = GLOBALES.ESPECIAL;

            b.OnBuscar += b_OnBuscar;
            b.ShowDialog();
        }

        void b_OnBuscar(int id, string nombre)
        {
            _idEmpleado = id;
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
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error");
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

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            //SE VALIDA SI TODOS LOS CAMPOS HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;
            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idtrabajador = _idEmpleado;
            falta.idempresa = GLOBALES.IDEMPRESA;
            falta.periodo = idperiodo;
            falta.faltas = int.Parse(txtFaltas.Text.Trim());
            falta.fechainicio = dtpInicio.Value;
            falta.fechafin = dtpFin.Value;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idEmpleado;

            List<Empleados.Core.Empleados> lstEmpleado;

            switch (_tipoOperacion)
            {
                case 0:
                    switch (_tipoForma)
                    {
                        case 0://ALTA EN BASE DE DATOS
                            try
                            {
                                cnx.Open();
                                fh.insertaFalta(falta);
                                cnx.Close();
                                cnx.Dispose();
                                if (OnFaltas != null)
                                    OnFaltas(null, null, null, null, 0, DateTime.Now, DateTime.Now);
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show("Error al ingresar la falta. \r\n \r\n Error: " + error.Message, "Error");
                                this.Dispose();
                            }
                            break;
                        case 1://ALTA EN DATAGRIDVIEW
                            try
                            {
                                cnx.Open();
                                lstEmpleado = eh.obtenerEmpleado(empleado);
                                cnx.Close();
                                cnx.Dispose();
                                if (OnFaltas != null)
                                    OnFaltas(lstEmpleado[0].noempleado, lstEmpleado[0].nombres, lstEmpleado[0].paterno, lstEmpleado[0].materno, int.Parse(txtFaltas.Text), dtpInicio.Value, dtpInicio.Value);
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show("Error al ingresar la falta. \r\n \r\n Error: " + error.Message, "Error");
                                this.Dispose();
                            }
                            break;
                    }
                    break;
                case 2:
                    try
                    {
                        falta.id = _idFalta;
                        cnx.Open();
                        fh.actualizaFalta(falta);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar la falta. \r\n \r\n Error: " + error.Message, "Error");
                        this.Dispose();
                    }
                    break;
            }
            this.Dispose();
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                if (dtpInicio.Value.Day <= 15)
                {
                    dtpInicio.Value = new DateTime(dtpInicio.Value.Year, dtpInicio.Value.Month, 1);
                    dtpFin.Value = new DateTime(dtpInicio.Value.Year, dtpInicio.Value.Month, 15);
                }
                else
                {
                    dtpInicio.Value = new DateTime(dtpInicio.Value.Year, dtpInicio.Value.Month, 16);
                    dtpFin.Value = new DateTime(dtpInicio.Value.Year, dtpInicio.Value.Month, DateTime.DaysInMonth(dtpInicio.Value.Year, dtpInicio.Value.Month));
                }
            }
        }
    }
}


