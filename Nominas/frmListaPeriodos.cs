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
    public partial class frmListaPeriodos : Form
    {
        public frmListaPeriodos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Periodos.Core.Periodos> lstPeriodos;
        #endregion

        private void ListaPeriodos()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;
            Periodos.Core.Periodos periodos = new Periodos.Core.Periodos();
            periodos.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstPeriodos = ph.obtenerPeriodos(periodos);
                cnx.Close();
                cnx.Dispose();

                var periodo = from p in lstPeriodos
                              select new
                              {
                                  IdPeriodo = p.idperiodo,
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
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Consultar": toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Editar": toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Eliminar": toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
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
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                ph.Command = cmd;
                Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
                periodo.idperiodo = id;
                
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
                                  select new
                                  {
                                      IdPeriodo = p.idperiodo,
                                      Pago = p.pago
                                  };
                    dgvPeriodos.DataSource = periodo.ToList();
                }
                else
                {
                    var busqueda = from p in lstPeriodos
                                   where p.pago.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       IdPeriodo = p.idperiodo,
                                       Pago = p.pago
                                   };
                    dgvPeriodos.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar periodo...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }
    }
}
