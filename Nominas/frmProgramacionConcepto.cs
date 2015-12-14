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
    public partial class frmProgramacionConcepto : Form
    {
        public frmProgramacionConcepto()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado = 0;
        public string _nombreEmpleado;
        public int _tipoOperacion;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        ProgramacionConcepto.Core.ProgramacionHelper pch;
        Conceptos.Core.ConceptosHelper ch;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevaProgramacion(int edicion);
        public event delOnNuevaProgramacion OnNuevaProgramacion;
        #endregion

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.OnBuscar += b_OnBuscar;
            b._catalogo = GLOBALES.EMPLEADOS;
            b.Show();
        }

        void b_OnBuscar(int id, string nombre)
        {
            _idEmpleado = id;
            lblEmpleado.Text = nombre;
        }

        private void frmProgramacionConcepto_Load(object sender, EventArgs e)
        {
            cargaCombo();

            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                lblEmpleado.Text = _nombreEmpleado;
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                pch = new ProgramacionConcepto.Core.ProgramacionHelper();
                pch.Command = cmd;

                ProgramacionConcepto.Core.ProgramacionConcepto programacion = new ProgramacionConcepto.Core.ProgramacionConcepto();
                programacion.idtrabajador = _idEmpleado;

                List<ProgramacionConcepto.Core.ProgramacionConcepto> lstProgramacion;

                try
                {
                    cnx.Open();
                    lstProgramacion = pch.obtenerProgramacion(programacion);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstProgramacion.Count; i++)
                    {
                        cmbConcepto.SelectedValue = lstProgramacion[i].idconcepto;
                        txtCantidad.Text = lstProgramacion[i].cantidad.ToString();
                        dtpFecha.Value = DateTime.Parse(lstProgramacion[i].fechafin.ToString());
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta programación";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(ComboBox));
                    GLOBALES.INHABILITAR(this, typeof(DateTimePicker));
                    toolBuscar.Enabled = false;
                    toolGuardar.Enabled = false;
                }
                else
                    toolTitulo.Text = "Edición programación";
            }
        }

        private void cargaCombo()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.idempresa = GLOBALES.IDEMPRESA;
            concepto.tipoconcepto = "D";
            List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();

            try
            {
                cnx.Open();
                lstConceptos = ch.obtenerConceptosDeducciones(concepto);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            cmbConcepto.DataSource = lstConceptos;
            cmbConcepto.DisplayMember = "concepto";
            cmbConcepto.ValueMember = "id";
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            //SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            control = GLOBALES.VALIDAR(this, typeof(ComboBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            pch = new ProgramacionConcepto.Core.ProgramacionHelper();
            pch.Command = cmd;

            ProgramacionConcepto.Core.ProgramacionConcepto programacion = new ProgramacionConcepto.Core.ProgramacionConcepto();
            programacion.idtrabajador = _idEmpleado;
            programacion.idconcepto = int.Parse(cmbConcepto.SelectedValue.ToString());
            programacion.cantidad = double.Parse(txtCantidad.Text);
            programacion.fechafin = dtpFecha.Value;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        if (_idEmpleado == 0)
                        {
                            MessageBox.Show("No ha seleccionado algún empleado.", "Información");
                            return;
                        }
                        programacion.idempresa = GLOBALES.IDEMPRESA;
                        cnx.Open();
                        pch.insertaProgramacion(programacion);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el concepto programado. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        cnx.Open();
                        pch.actualizaProgramacion(programacion);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el concepto programado. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            if (OnNuevaProgramacion != null)
                OnNuevaProgramacion(_tipoOperacion);
            this.Dispose();
        }
    }
}
