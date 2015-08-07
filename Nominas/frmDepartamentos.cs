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
    public partial class frmDepartamentos : Form
    {
        public frmDepartamentos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Departamento.Core.DeptoHelper dh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _iddepartamento;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoDepto(int edicion);
        public event delOnNuevoDepto OnNuevoDepto;
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
            dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            depto.descripcion = txtDescripcion.Text;
            depto.estatus = 1;
            depto.idempresa = GLOBALES.IDEMPRESA;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        dh.insertaDepartamento(depto);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el departamento. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        depto.id = _iddepartamento;
                        depto.descripcion = txtDescripcion.Text;
                        cnx.Open();
                        dh.actualizaDepartamento(depto);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el departamento. \r\n \r\n Error: " + error.Message);
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
                    if (OnNuevoDepto != null)
                        OnNuevoDepto(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void frmDepartamentos_Load(object sender, EventArgs e)
        {
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                dh = new Departamento.Core.DeptoHelper();
                dh.Command = cmd;

                Departamento.Core.Depto d = new Departamento.Core.Depto();
                d.id = _iddepartamento;

                List<Departamento.Core.Depto> lstDepto;

                try
                {
                    cnx.Open();
                    lstDepto = dh.obtenerDepartamento(d);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstDepto.Count; i++)
                    {
                        txtDescripcion.Text = lstDepto[i].descripcion;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Departamento";
                    GLOBALES.INHABILITAR(this,typeof(TextBox));
                }
                else
                    toolTitulo.Text = "Edición Departamento";
            }
        }
    }
}
