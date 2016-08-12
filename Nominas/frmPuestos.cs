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
    public partial class frmPuestos : Form
    {
        public frmPuestos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Puestos.Core.PuestosHelper ph;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idPuesto;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoPuesto(int edicion);
        public event delOnNuevoPuesto OnNuevoPuesto;
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
            ph = new Puestos.Core.PuestosHelper();
            ph.Command = cmd;

            Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
            puesto.nombre = txtDescripcion.Text;
            puesto.estatus = 1;
            puesto.idempresa = GLOBALES.IDEMPRESA;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ph.insertaPuesto(puesto);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el puesto. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        puesto.idpuesto = _idPuesto;
                        puesto.nombre = txtDescripcion.Text;
                        cnx.Open();
                        ph.actualizaPuesto(puesto);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el puesto. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    //limpiar(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoPuesto != null)
                        OnNuevoPuesto(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void frmPuestos_Load(object sender, EventArgs e)
        {
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ph = new Puestos.Core.PuestosHelper();
                ph.Command = cmd;

                Puestos.Core.Puestos p = new Puestos.Core.Puestos();
                p.idpuesto = _idPuesto;

                List<Puestos.Core.Puestos> lstPuesto;

                try
                {
                    cnx.Open();
                    lstPuesto = ph.obtenerPuesto(p);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstPuesto.Count; i++)
                    {
                        txtDescripcion.Text = lstPuesto[i].nombre;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Puesto";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                }
                else
                    toolTitulo.Text = "Edición Puesto";
            }
        }
    }
}
