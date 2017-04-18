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
    public partial class frmFactorDescuento : Form
    {
        public frmFactorDescuento()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        FactorDescuento.Core.FactorDescuentoHelper fdh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idfactor;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoFactorDescuento(int edicion);
        public event delOnNuevoFactorDescuento OnNuevoFactorDescuento;
        #endregion

        private void frmFactorDescuento_Load(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                fdh = new FactorDescuento.Core.FactorDescuentoHelper();
                fdh.Command = cmd;

                FactorDescuento.Core.FactorDescuento factor = new FactorDescuento.Core.FactorDescuento();
                factor.idfactor = _idfactor;

                List<FactorDescuento.Core.FactorDescuento> lstFactor;

                try
                {
                    cnx.Open();
                    lstFactor = fdh.obtenerFactor(factor);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstFactor.Count; i++)
                    {
                        txtFactor.Text = lstFactor[i].factor.ToString();
                        dtpFecha.Value = lstFactor[i].fecha;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta factor de descuento";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(DateTimePicker));
                }
                else
                    toolTitulo.Text = "Edición factor de descuento";
            }
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

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            fdh = new FactorDescuento.Core.FactorDescuentoHelper();
            fdh.Command = cmd;

            FactorDescuento.Core.FactorDescuento factor = new FactorDescuento.Core.FactorDescuento();
            factor.factor = decimal.Parse(txtFactor.Text.Trim());
            factor.fecha = dtpFecha.Value;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        fdh.insertaFactor(factor);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el factor. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        factor.idfactor = _idfactor;
                        cnx.Open();
                        fdh.actualizaFactor(factor);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el factor. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoFactorDescuento != null)
                        OnNuevoFactorDescuento(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }
    }
}
