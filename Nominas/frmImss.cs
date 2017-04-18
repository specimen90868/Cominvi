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
    public partial class frmImss : Form
    {
        public frmImss()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Imss.Core.ImssHelper ih;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoImss(int edicion);
        public event delOnNuevoImss OnNuevoImss;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idImss;
        public int _tipoOperacion;
        #endregion

        private void frmImss_Load(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ih = new Imss.Core.ImssHelper();
                ih.Command = cmd;

                Imss.Core.Imss imss = new Imss.Core.Imss();
                imss.id = _idImss;

                List<Imss.Core.Imss> lstImss;

                try
                {
                    cnx.Open();
                    lstImss = ih.ObtenerImss(imss);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstImss.Count; i++)
                    {
                        txtPrestacion.Text = lstImss[i].prestacion;
                        txtPorcentaje.Text = lstImss[i].porcentaje.ToString();
                        chkSeCalcula.Checked = lstImss[i].calculo;
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
            ih = new Imss.Core.ImssHelper();
            ih.Command = cmd;

            Imss.Core.Imss imss = new Imss.Core.Imss();
            imss.prestacion = txtPrestacion.Text;
            imss.porcentaje = decimal.Parse(txtPorcentaje.Text);
            imss.calculo = chkSeCalcula.Checked;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ih.insertaImss(imss);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar la prestación del IMSS. \r\n \r\n Error: " + error.Message,"Error");
                    }
                    break;
                case 2:
                    try
                    {
                        imss.id = _idImss;
                        cnx.Open();
                        ih.actualizaImss(imss);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar la prestación del IMSS. \r\n \r\n Error: " + error.Message, "Error");
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoImss != null)
                        OnNuevoImss(_tipoOperacion);
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
