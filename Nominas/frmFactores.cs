using System.Data.SqlClient;
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
    public partial class frmFactores : Form
    {
        public frmFactores()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Factores.Core.FactoresHelper fh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idfactor;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoFactor(int edicion);
        public event delOnNuevoFactor OnNuevoFactor;
        #endregion

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
            fh = new Factores.Core.FactoresHelper();
            fh.Command = cmd;

            Factores.Core.Factores factor = new Factores.Core.Factores();
            factor.anio = int.Parse(txtAnio.Text.Trim());
            factor.valor = decimal.Parse(txtValor.Text.Trim());

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        fh.insertaFactor(factor);
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
                        fh.actualizaFactor(factor);
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
                    if (OnNuevoFactor != null)
                        OnNuevoFactor(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void frmFactores_Load(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                fh = new Factores.Core.FactoresHelper();
                fh.Command = cmd;

                Factores.Core.Factores factor = new Factores.Core.Factores();
                factor.idfactor = _idfactor;

                List<Factores.Core.Factores> lstFactor;

                try
                {
                    cnx.Open();
                    lstFactor = fh.obtenerFactor(factor);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstFactor.Count; i++)
                    {
                        txtAnio.Text = lstFactor[i].anio.ToString();
                        txtValor.Text = lstFactor[i].valor.ToString();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta factor de integración";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                }
                else
                    toolTitulo.Text = "Edición factor de integración";
            }
        }
    }
}
