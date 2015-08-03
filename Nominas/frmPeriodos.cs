using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmPeriodos : Form
    {
        public frmPeriodos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Periodos.Core.PeriodosHelper ph;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idperiodo;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoPeriodo(int edicion);
        public event delOnNuevoPeriodo OnNuevoPeriodo;
        #endregion

        private void frmPeriodos_Load(object sender, EventArgs e)
        {
            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;
            Clientes.Core.ClientesHelper ch = new Clientes.Core.ClientesHelper();
            ch.Command = cmd;

            //DataTable dtClientes = new DataTable();
            List<Clientes.Core.Clientes> lstClientes = new List<Clientes.Core.Clientes>();
            Clientes.Core.Clientes cliente = new Clientes.Core.Clientes();
            cliente.plaza = GLOBALES.IDPLAZA;

            try
            {
                cnx.Open();
                lstClientes = ch.obtenerClientes(cliente);
                //dtClientes = ch.dtObtenerClientes(cliente);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            cmbCliente.DataSource = lstClientes.ToList();
            cmbCliente.ValueMember = "idcliente";
            cmbCliente.DisplayMember = "nombre";

            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                ph = new Periodos.Core.PeriodosHelper();
                ph.Command = cmd;

                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                p.idperiodo = _idperiodo;

                List<Periodos.Core.Periodos> lstPeriodo;

                try
                {
                    cnx.Open();
                    lstPeriodo = ph.obtenerPeriodo(p);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstPeriodo.Count; i++)
                    {
                        cmbCliente.SelectedValue = lstPeriodo[i].idperiodo;
                        cmbPago.SelectedText = lstPeriodo[i].pago;
                        txtDias.Text = lstPeriodo[i].dias.ToString();
                        cmbDiaInicio.SelectedText = lstPeriodo[i].inicio;
                        cmbDiaTermino.SelectedText = lstPeriodo[i].termino;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Periodo";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(ComboBox));
                }
                else
                    toolTitulo.Text = "Edición Periodo";
            }

            cmbPago.SelectedIndex = 0;
            cmbDiaInicio.SelectedIndex = 0;
            cmbDiaTermino.SelectedIndex = 6;
        }

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            guardar(0);
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void guardar(int tipoGuardar)
        {
            //SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;
            ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.idcliente = int.Parse(cmbCliente.SelectedValue.ToString());
            periodo.pago = cmbPago.SelectedText;
            periodo.dias = cmbPago.SelectedText.Equals("SEMANAL") ? 7 : 15;
            periodo.inicio = cmbDiaInicio.SelectedText;
            periodo.termino = cmbDiaTermino.SelectedText;
            periodo.estatus = 1;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ph.insertaPeriodo(periodo);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el periodo. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        periodo.idperiodo = _idperiodo;
                        cnx.Open();
                        ph.actualizaPeriodo(periodo);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el periodo. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    GLOBALES.REFRESCAR(this, typeof(ComboBox));
                    break;
                case 1:
                    if (OnNuevoPeriodo != null)
                        OnNuevoPeriodo(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void cmbPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPago.SelectedIndex.Equals(1))
            {
                cmbDiaInicio.Enabled = false;
                txtDias.Text = "15";
            }
            else
            {
                cmbDiaInicio.Enabled = true;
                txtDias.Text = "7";
            }
        }

        private void cmbDiaInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dia = cmbDiaInicio.SelectedIndex;
            switch (dia)
            {
                case 0:
                    cmbDiaTermino.SelectedIndex = 6;
                    break;
                case 1:
                    cmbDiaTermino.SelectedIndex = 0;
                    break;
                case 2:
                    cmbDiaTermino.SelectedIndex = 1;
                    break;
                case 3:
                    cmbDiaTermino.SelectedIndex = 2;
                    break;
                case 4:
                    cmbDiaTermino.SelectedIndex = 3;
                    break;
                case 5:
                    cmbDiaTermino.SelectedIndex = 4;
                    break;
                case 6:
                    cmbDiaTermino.SelectedIndex = 5;
                    break;
            }
        }
    }
}
