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
    public partial class frmSubsidio : Form
    {
        public frmSubsidio()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        TablaSubsidio.Core.SubsidioHelper sh;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoSubsidio(int edicion);
        public event delOnNuevoSubsidio OnNuevoSubsidio;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idSubsidio;
        public int _tipoOperacion;
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

        private void frmSubsidio_Load(object sender, EventArgs e)
        {
            txtAnio.Text = DateTime.Now.Year.ToString();
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                sh = new TablaSubsidio.Core.SubsidioHelper();
                sh.Command = cmd;

                TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
                subsidio.id = _idSubsidio;

                List<TablaSubsidio.Core.TablaSubsidio> lstSubsidio;

                try
                {
                    cnx.Open();
                    lstSubsidio = sh.obtieneSubsidio(subsidio);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstSubsidio.Count; i++)
                    {
                        txtDesde.Text = lstSubsidio[i].desde.ToString();
                        txtCantidad.Text = lstSubsidio[i].cantidad.ToString();
                        txtDias.Text = lstSubsidio[i].periodo.ToString();
                        txtAnio.Text = lstSubsidio[i].anio.ToString();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Subsidio";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                }
                else
                    toolTitulo.Text = "Edición Subsidio";
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
            sh = new TablaSubsidio.Core.SubsidioHelper();
            sh.Command = cmd;

            TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
            subsidio.desde = decimal.Parse(txtDesde.Text.Trim());
            subsidio.cantidad = decimal.Parse(txtCantidad.Text.Trim());
            subsidio.periodo = int.Parse(txtDias.Text.Trim());
            subsidio.anio = int.Parse(txtAnio.Text.Trim());

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        sh.insertaSubsidio(subsidio);
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
                        subsidio.id =_idSubsidio;
                        cnx.Open();
                        sh.actualizaSubsidio(subsidio);
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
                    if (OnNuevoSubsidio != null)
                        OnNuevoSubsidio(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }
    }
}
