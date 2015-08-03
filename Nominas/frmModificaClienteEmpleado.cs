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
    public partial class frmModificaClienteEmpleado : Form
    {
        public frmModificaClienteEmpleado()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        int idempleado = 0;
        #endregion

        private void frmModificaClienteEmpleado_Load(object sender, EventArgs e)
        {
            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            Clientes.Core.ClientesHelper ch = new Clientes.Core.ClientesHelper();
            ch.Command = cmd;
            
            Clientes.Core.Clientes cli = new Clientes.Core.Clientes();
            cli.plaza = GLOBALES.IDPLAZA;

            List<Clientes.Core.Clientes> lstClientes = new List<Clientes.Core.Clientes>();
            
            try
            {
                cnx.Open();
                lstClientes = ch.obtenerClientes(cli);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                this.Dispose();
            }

            cmbCliente.DataSource = lstClientes.ToList();
            cmbCliente.DisplayMember = "nombre";
            cmbCliente.ValueMember = "idcliente";
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            if (idempleado == 0)
            {
                MessageBox.Show("Debe especificar el empleado.","Información");
                return;
            }

            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;
            Empleados.Core.Empleados em = new Empleados.Core.Empleados();
            em.idcliente = int.Parse(cmbCliente.SelectedValue.ToString());
            em.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
            em.idtrabajador = idempleado;

            try
            {
                cnx.Open();
                eh.actualizaClientePeriodo(em);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.MdiParent = this.MdiParent;
            b._catalogo = GLOBALES.EMPLEADOS;
            b.OnBuscar += b_OnBuscar;
            b.Show();
        }

        void b_OnBuscar(int id, string nombre)
        {
            idempleado = id;
            lblEmpleado.Text = nombre;
        }

        private void cmbCliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            p.idcliente = int.Parse(cmbCliente.SelectedValue.ToString());

            List<Periodos.Core.Periodos> lstPeriodo = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstPeriodo = ph.obtenerPeriodoCliente(p);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                this.Dispose();
            }

            cmbPeriodo.DataSource = lstPeriodo.ToList();
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";
        }
    }
}
