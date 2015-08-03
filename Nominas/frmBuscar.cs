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
    public partial class frmBuscar : Form
    {
        public frmBuscar()
        {
            InitializeComponent();
        }

        #region DELEGADOS
        public delegate void delOnBuscar(int id, string nombre);
        public event delOnBuscar OnBuscar;
        #endregion

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        List<Empleados.Core.Empleados> lstEmpleados;

        Clientes.Core.ClientesHelper ch;
        List<Clientes.Core.Clientes> lstClientes;
        #endregion

        #region VARIABLES PUBLICAS
        public int _catalogo;
        #endregion

        private void frmBuscar_Load(object sender, EventArgs e)
        {
            dgvCatalogo.RowHeadersVisible = false;

            cnx = new MySqlConnection();
            cmd = new MySqlCommand();
            cnx.ConnectionString = cdn;
            cmd.Connection = cnx;

            if (_catalogo == GLOBALES.EMPLEADOS)
            {
                eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                Empleados.Core.Empleados em = new Empleados.Core.Empleados();
                em.idplaza = GLOBALES.IDPLAZA;
                em.idempresa = GLOBALES.IDEMPRESA;

                try
                {
                    cnx.Open();
                    lstEmpleados = eh.obtenerEmpleados(em);
                    cnx.Close();
                    cnx.Dispose();

                    var empleados = from a in lstEmpleados select new { Id = a.idtrabajador, Nombre = a.nombrecompleto };
                    dgvCatalogo.DataSource = empleados.ToList();

                    for (int i = 0; i < dgvCatalogo.Columns.Count; i++)
                    {
                        dgvCatalogo.AutoResizeColumn(i);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
            else if (_catalogo == GLOBALES.CLIENTES)
            {
                ch = new Clientes.Core.ClientesHelper();
                ch.Command = cmd;

                Clientes.Core.Clientes clientes = new Clientes.Core.Clientes();
                clientes.plaza = GLOBALES.IDPLAZA;

                try
                {
                    cnx.Open();
                    lstClientes = ch.obtenerClientes(clientes);
                    cnx.Close();
                    cnx.Dispose();

                    var cliente = from c in lstClientes select new { Id = c.idcliente, Nombre = c.nombre };
                    dgvCatalogo.DataSource = cliente.ToList();

                    for (int i = 0; i < dgvCatalogo.Columns.Count; i++)
                    {
                        dgvCatalogo.AutoResizeColumn(i);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
            
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    if (_catalogo == GLOBALES.EMPLEADOS)
                    {
                        var empleado = from em in lstEmpleados
                                       select new
                                       {
                                           Id = em.idtrabajador,
                                           Nombre = em.nombrecompleto
                                       };
                        dgvCatalogo.DataSource = empleado.ToList();
                    }
                    else if (_catalogo == GLOBALES.CLIENTES)
                    {
                        var cliente = from c in lstClientes
                                       select new
                                       {
                                           Id = c.idcliente,
                                           Nombre = c.nombre
                                       };
                        dgvCatalogo.DataSource = cliente.ToList();
                    }
                    
                }
                else
                {
                    if (_catalogo == GLOBALES.EMPLEADOS)
                    {
                        var busqueda = from b in lstEmpleados
                                       where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper())
                                       select new
                                       {
                                           Id = b.idtrabajador,
                                           Nombre = b.nombrecompleto
                                       };
                        dgvCatalogo.DataSource = busqueda.ToList();
                    }
                    else if (_catalogo == GLOBALES.CLIENTES)
                    {
                        var busqueda = from b in lstClientes
                                       where b.nombre.Contains(txtBuscar.Text.ToUpper())
                                       select new
                                       {
                                           Id = b.idcliente,
                                           Nombre = b.nombre
                                       };
                        dgvCatalogo.DataSource = busqueda.ToList();
                    }
                }
            }
        }

        private void toolAceptar_Click(object sender, EventArgs e)
        {
            Seleccion();
        }

        private void dgvEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion();
        }

        private void Seleccion()
        {
            if (OnBuscar != null)
            {
                int fila = dgvCatalogo.CurrentCell.RowIndex;
                OnBuscar(int.Parse(dgvCatalogo.Rows[fila].Cells[0].Value.ToString()), dgvCatalogo.Rows[fila].Cells[1].Value.ToString());
                this.Dispose();
            }
        }
    }
}
