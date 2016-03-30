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
    public partial class frmIsr : Form
    {
        public frmIsr()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        TablaIsr.Core.IsrHelper ih;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoIsr(int edicion);
        public event delOnNuevoIsr OnNuevoIsr;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idIsr;
        public int _tipoOperacion;
        #endregion

        private void frmIsr_Load(object sender, EventArgs e)
        {
            txtAnio.Text = DateTime.Now.Year.ToString();
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ih = new TablaIsr.Core.IsrHelper();
                ih.Command = cmd;

                TablaIsr.Core.TablaIsr isr = new TablaIsr.Core.TablaIsr();
                isr.id = _idIsr;

                List<TablaIsr.Core.TablaIsr> lstIsr;

                try
                {
                    cnx.Open();
                    lstIsr = ih.obtenerIsr(isr);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstIsr.Count; i++)
                    {
                        txtInferior.Text = lstIsr[i].inferior.ToString();
                        txtCuota.Text = lstIsr[i].cuota.ToString();
                        txtPorcentaje.Text = lstIsr[i].porcentaje.ToString();
                        txtDias.Text = lstIsr[i].periodo.ToString();
                        txtAnio.Text = lstIsr[i].anio.ToString();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta ISR";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                }
                else
                    toolTitulo.Text = "Edición ISR";
            }
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
            ih = new TablaIsr.Core.IsrHelper();
            ih.Command = cmd;

            TablaIsr.Core.TablaIsr isr = new TablaIsr.Core.TablaIsr();
            isr.inferior = decimal.Parse(txtInferior.Text.Trim());
            isr.cuota = decimal.Parse(txtCuota.Text.Trim());
            isr.porcentaje = decimal.Parse(txtPorcentaje.Text.Trim());
            isr.periodo = int.Parse(txtDias.Text.Trim());
            isr.anio = int.Parse(txtAnio.Text.Trim());

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ih.insertaIsr(isr);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el isr. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        isr.id = _idIsr;
                        cnx.Open();
                        ih.actualizaIsr(isr);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el isr. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    txtAnio.Text = DateTime.Now.Year.ToString();
                    break;
                case 1:
                    if (OnNuevoIsr != null)
                        OnNuevoIsr(_tipoOperacion);
                    this.Dispose();
                    break;
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
    }
}
