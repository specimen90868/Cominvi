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
    public partial class frmListaClientes : Form
    {
        public frmListaClientes()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        List<Clientes.Core.Clientes> lstClientes;
        #endregion

        private void ListaClientes()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new MySqlConnection(cdn);
            cmd = new MySqlCommand();
            cmd.Connection = cnx;
            Clientes.Core.ClientesHelper ch = new Clientes.Core.ClientesHelper();
            ch.Command = cmd;

            Clientes.Core.Clientes cliente = new Clientes.Core.Clientes();
            cliente.plaza = GLOBALES.IDPLAZA;
            try
            {
                cnx.Open();
                lstClientes = ch.obtenerClientes(cliente);
                cnx.Close();
                cnx.Dispose();

                var cli = from c in lstClientes
                         select new
                         {
                             IdCliente = c.idcliente,
                             Nombre = c.nombre,
                             RFC = c.rfc
                         };
                dgvClientes.DataSource = cli.ToList();

                for (int i = 0; i < dgvClientes.Columns.Count; i++)
                {
                    dgvClientes.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Clientes");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "Clientes":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].consulta);
                        toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].modificar);
                        toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].baja);
                        break;
                }
            }
        }

        private void frmListaClientes_Load(object sender, EventArgs e)
        {
            dgvClientes.RowHeadersVisible = false;
            CargaPerfil();
            ListaClientes();
        }

        private void Selecciona(int edicion)
        {
            frmClientes c = new frmClientes();
            c.MdiParent = this.MdiParent;
            c.OnNuevoCliente += c_OnNuevoCliente;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvClientes.CurrentCell.RowIndex;
                c._idcliente = int.Parse(dgvClientes.Rows[fila].Cells[0].Value.ToString());
            }
            c._tipoOperacion = edicion;
            c.Show();
        }

        void c_OnNuevoCliente(int edicion)
        {
            if (edicion == 0 || edicion == 2)
                ListaClientes();
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Selecciona(1);
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Selecciona(GLOBALES.NUEVO);
        }

        private void toolConsultar_Click(object sender, EventArgs e)
        {
            Selecciona(GLOBALES.CONSULTAR);
        }

        private void toolEditar_Click(object sender, EventArgs e)
        {
            Selecciona(GLOBALES.MODIFICAR);
        }

        private void toolBaja_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el cliente?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvClientes.CurrentCell.RowIndex;
                int idcliente = int.Parse(dgvClientes.Rows[fila].Cells[0].Value.ToString());
                cnx = new MySqlConnection(cdn);
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                Clientes.Core.ClientesHelper ch = new Clientes.Core.ClientesHelper();
                ch.Command = cmd;
                Clientes.Core.Clientes cliente = new Clientes.Core.Clientes();
                cliente.idcliente = idcliente;
                try
                {
                    cnx.Open();
                    ch.bajaCliente(cliente);
                    cnx.Close();
                    cnx.Dispose();
                    ListaClientes();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    var cli = from c in lstClientes
                             select new
                             {
                                 IdCliente = c.idcliente,
                                 Nombre = c.nombre,
                                 RFC = c.rfc
                             };
                    dgvClientes.DataSource = cli.ToList();
                }
                else
                {
                    var busqueda = from b in lstClientes
                                   where b.nombre.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       IdCliente = b.idcliente,
                                       Nombre = b.nombre,
                                       RFC = b.rfc
                                   };
                    dgvClientes.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar cliente...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }
    }
}
