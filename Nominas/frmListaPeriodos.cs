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
    public partial class frmListaPeriodos : Form
    {
        public frmListaPeriodos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        List<Periodos.Core.Periodos> lstPeriodos;
        List<Clientes.Core.Clientes> lstClientes;
        #endregion

        private void ListaPeriodos()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new MySqlConnection(cdn);
            cmd = new MySqlCommand();
            cmd.Connection = cnx;
            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            Clientes.Core.ClientesHelper ch = new Clientes.Core.ClientesHelper();
            ph.Command = cmd;
            ch.Command = cmd;

            Clientes.Core.Clientes c = new Clientes.Core.Clientes();
            c.plaza = GLOBALES.IDPLAZA;

            try
            {
                cnx.Open();
                lstPeriodos = ph.obtenerPeriodos();
                lstClientes = ch.obtenerClientes(c);
                cnx.Close();
                cnx.Dispose();

                var periodo = from p in lstPeriodos
                              join cli in lstClientes
                              on p.idcliente equals cli.idcliente
                              orderby cli.nombre ascending
                              select new
                              {
                                  IdPeriodo = p.idperiodo,
                                  Cliente = cli.nombre,
                                  Pago = p.pago
                              };
                            
                dgvPeriodos.DataSource = periodo.ToList();

                for (int i = 0; i < dgvPeriodos.Columns.Count; i++)
                {
                    dgvPeriodos.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Periodos");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "Periodos":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].consulta);
                        toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].modificar);
                        toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].baja);
                        break;
                }
            }
        }

        private void frmListaPeriodos_Load(object sender, EventArgs e)
        {
            dgvPeriodos.RowHeadersVisible = false;
            CargaPerfil();
            ListaPeriodos();
        }

        private void Seleccion(int edicion)
        {
            frmPeriodos p = new frmPeriodos();
            p.MdiParent = this.MdiParent;
            p.OnNuevoPeriodo += p_OnNuevoPeriodo;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvPeriodos.CurrentCell.RowIndex;
                p._idperiodo = int.Parse(dgvPeriodos.Rows[fila].Cells[0].Value.ToString());
            }
            p._tipoOperacion = edicion;
            p.Show();
        }

        void p_OnNuevoPeriodo(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaPeriodos();
        }

        private void dgvPeriodos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.NUEVO);
        }

        private void toolConsultar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }

        private void toolEditar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.MODIFICAR);
        }

        private void toolBaja_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el periodo?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvPeriodos.CurrentCell.RowIndex;
                int id = int.Parse(dgvPeriodos.Rows[fila].Cells[0].Value.ToString());
                cnx = new MySqlConnection(cdn);
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                ph.Command = cmd;
                Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
                periodo.idperiodo = id;
                periodo.estatus = 0;
                try
                {
                    cnx.Open();
                    ph.bajaPeriodo(periodo);
                    cnx.Close();
                    cnx.Dispose();
                    ListaPeriodos();
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
                    var periodo = from p in lstPeriodos
                                  join cli in lstClientes on p.idcliente equals cli.idcliente
                                  orderby cli.nombre ascending
                                  select new
                                  {
                                      IdPeriodo = p.idperiodo,
                                      Cliente = cli.nombre,
                                      Pago = p.pago
                                  };
                    dgvPeriodos.DataSource = periodo.ToList();
                }
                else
                {
                    var busqueda = from p in lstPeriodos
                                   join cli in lstClientes on p.idcliente equals cli.idcliente where cli.nombre.Contains(txtBuscar.Text.ToUpper())
                                   orderby cli.nombre ascending
                                   select new
                                   {
                                       IdPeriodo = p.idperiodo,
                                       Cliente = cli.nombre,
                                       Pago = p.pago
                                   };
                    dgvPeriodos.DataSource = busqueda.ToList();
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
