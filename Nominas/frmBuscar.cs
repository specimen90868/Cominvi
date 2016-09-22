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
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        List<Empleados.Core.Empleados> lstEmpleados;
        #endregion

        #region VARIABLES PUBLICAS
        public int _catalogo;
        public int _tipoNomina;
        public bool _obracivil;
        public int _periodo;
        public int _busqueda;
        #endregion

        private void frmBuscar_Load(object sender, EventArgs e)
        {
            dgvCatalogo.RowHeadersVisible = false;

            cnx = new SqlConnection();
            cmd = new SqlCommand();
            cnx.ConnectionString = cdn;
            cmd.Connection = cnx;

            if (_catalogo == GLOBALES.EMPLEADOS)
            {
                eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                Empleados.Core.Empleados em = new Empleados.Core.Empleados();
                em.idempresa = GLOBALES.IDEMPRESA;

                if (_tipoNomina == GLOBALES.NORMAL)
                    em.estatus = GLOBALES.ACTIVO;

                try
                {
                    cnx.Open();
                    if(_busqueda == GLOBALES.NOMINA)
                        lstEmpleados = eh.obtenerEmpleados(em, _periodo);
                    if (_busqueda == GLOBALES.FORMULARIOS)
                        lstEmpleados = eh.obtenerEmpleados(em);
                    cnx.Close();
                    cnx.Dispose();

                    dgvCatalogo.Columns["idtrabajador"].DataPropertyName = "idtrabajador";
                    dgvCatalogo.Columns["noempleado"].DataPropertyName = "noempleado";
                    dgvCatalogo.Columns["nombre"].DataPropertyName = "nombrecompleto";

                    var empleados = from a in lstEmpleados select new { a.idtrabajador, a.noempleado, a.nombrecompleto };
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

            txtBuscar.Select();
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
                                           em.idtrabajador,
                                           em.noempleado,
                                           em.nombrecompleto
                                       };
                        dgvCatalogo.DataSource = empleado.ToList();
                    }
                }
                else
                {
                    if (_catalogo == GLOBALES.EMPLEADOS)
                    {
                        var busqueda = from b in lstEmpleados
                                       where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper()) || b.noempleado.Contains(txtBuscar.Text)
                                       select new
                                       {
                                           b.idtrabajador,
                                           b.noempleado,
                                           b.nombrecompleto
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
                OnBuscar(int.Parse(dgvCatalogo.Rows[fila].Cells[0].Value.ToString()), dgvCatalogo.Rows[fila].Cells[2].Value.ToString());
                this.Dispose();
            }
        }

        private void frmBuscar_Shown(object sender, EventArgs e)
        {
            txtBuscar_Click(sender, e);
            txtBuscar.Focus();
        }
    }
}
