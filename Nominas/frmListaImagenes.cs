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
    public partial class frmListaImagenes : Form
    {
        public frmListaImagenes()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        List<Empresas.Core.Empresas> lstEmpresas;
        List<Empleados.Core.Empleados> lstEmpleados;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        #endregion

        #region VARIABLES PUBLICAS
        public int _EmpresaEmpleado;
        #endregion

        private void Listas()
        {
            cnx = new MySqlConnection(cdn);
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            if (_EmpresaEmpleado == GLOBALES.EMPRESAS)
            {
                Empresas.Core.EmpresasHelper empresah = new Empresas.Core.EmpresasHelper();
                empresah.Command = cmd;

                try
                {
                    cnx.Open();
                    lstEmpresas = empresah.obtenerEmpresas();
                    cnx.Close();
                    cnx.Dispose();

                    var empresa = from a in lstEmpresas select new { IdEmpresa = a.idempresa, Nombre = a.nombre };
                    dgvImagenes.DataSource = empresa.ToList();

                    for (int i = 0; i < dgvImagenes.Columns.Count; i++)
                    {
                        dgvImagenes.AutoResizeColumn(i);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message,"Error");
                }
            }
            else if (_EmpresaEmpleado == GLOBALES.EMPLEADOS)
            {
                Empleados.Core.EmpleadosHelper empleadoh = new Empleados.Core.EmpleadosHelper();
                empleadoh.Command = cmd;
                Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                empleado.idplaza = GLOBALES.IDPLAZA;
                empleado.idempresa = GLOBALES.IDEMPRESA;

                try
                {
                    cnx.Open();
                    lstEmpleados = empleadoh.obtenerEmpleados(empleado);
                    cnx.Close();
                    cnx.Dispose();

                    var trabajador = from b in lstEmpleados select new { IdEmpleado = b.idtrabajador, Nombre = b.nombrecompleto };
                    dgvImagenes.DataSource = trabajador.ToList();

                    for (int i = 0; i < dgvImagenes.Columns.Count; i++)
                    {
                        dgvImagenes.AutoResizeColumn(i);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Imagenes");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "Imagenes":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].consulta);
                        toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].modificar);
                        toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].baja);
                        break;
                }
            }
        }

        private void frmListaImagenes_Load(object sender, EventArgs e)
        {
            dgvImagenes.RowHeadersVisible = false;
            CargaPerfil();
            Listas();
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    if (_EmpresaEmpleado == GLOBALES.EMPRESAS)
                    {
                        var em = from b in lstEmpresas select new { IdEmpresa = b.idempresa, Nombre = b.nombre };
                        dgvImagenes.DataSource = em.ToList();
                    }
                    else if (_EmpresaEmpleado == GLOBALES.EMPLEADOS)
                    {
                        var trabajador = from b in lstEmpleados select new { IdEmpleado = b.idtrabajador, Nombre = b.nombrecompleto };
                        dgvImagenes.DataSource = trabajador.ToList();
                    }
                }
                else
                {
                    if (_EmpresaEmpleado == GLOBALES.EMPRESAS)
                    {
                        var busqueda = from b in lstEmpresas where b.nombre.Contains(txtBuscar.Text.ToUpper()) select new { IdEmpresa = b.idempresa, Nombre = b.nombre };
                        dgvImagenes.DataSource = busqueda.ToList();
                    }
                    else if (_EmpresaEmpleado == GLOBALES.EMPLEADOS)
                    {
                        var busqueda = from b in lstEmpleados where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper()) select new { IdEmpleado = b.idtrabajador, Nombre = b.nombrecompleto };
                        dgvImagenes.DataSource = busqueda.ToList();
                    }
                }
            }
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {

        }

        private void toolConsultar_Click(object sender, EventArgs e)
        {

        }

        private void toolEditar_Click(object sender, EventArgs e)
        {

        }

        private void toolBaja_Click(object sender, EventArgs e)
        {

        }
    }
}
