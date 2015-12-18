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
    public partial class frmListaDepartamentos : Form
    {
        public frmListaDepartamentos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Departamento.Core.Depto> lstDepartamentos;
        #endregion

        private void ListaDepartamentos()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;
            Departamento.Core.Depto deptos = new Departamento.Core.Depto();
            deptos.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstDepartamentos = dh.obtenerDepartamentos(deptos);
                cnx.Close();
                cnx.Dispose();

                var depto = from d in lstDepartamentos
                          select new
                          {
                              IdDepto = d.id,
                              Nombre = d.descripcion,
                          };
                dgvDepartamentos.DataSource = depto.ToList();

                for (int i = 0; i < dgvDepartamentos.Columns.Count; i++)
                {
                    dgvDepartamentos.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Departamentos");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Consultar": toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Editar": toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Eliminar": toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                }
            }
        }

        private void frmListaDepartamentos_Load(object sender, EventArgs e)
        {
            dgvDepartamentos.RowHeadersVisible = false;
            CargaPerfil();
            ListaDepartamentos();
        }

        private void Seleccion(int edicion)
        {
            frmDepartamentos d = new frmDepartamentos();
            d.MdiParent = this.MdiParent;
            d.OnNuevoDepto += d_OnNuevoDepto;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvDepartamentos.CurrentCell.RowIndex;
                d._iddepartamento = int.Parse(dgvDepartamentos.Rows[fila].Cells[0].Value.ToString());
            }
            d._tipoOperacion = edicion;
            d.Show();
        }

        void d_OnNuevoDepto(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaDepartamentos();
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el departamento?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvDepartamentos.CurrentCell.RowIndex;
                int id = int.Parse(dgvDepartamentos.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
                dh.Command = cmd;
                Departamento.Core.Depto depto = new Departamento.Core.Depto();
                depto.id = id;
                
                try
                {
                    cnx.Open();
                    dh.bajaDepartamento(depto);
                    cnx.Close();
                    cnx.Dispose();
                    ListaDepartamentos();
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
                    var depto = from d in lstDepartamentos
                              select new
                              {
                                  Id = d.id,
                                  Nombre = d.descripcion
                              };
                    dgvDepartamentos.DataSource = depto.ToList();
                }
                else
                {
                    var busqueda = from b in lstDepartamentos
                                   where b.descripcion.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       Id = b.id,
                                       Nombre = b.descripcion
                                   };
                    dgvDepartamentos.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar departamento...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }
    }
}
