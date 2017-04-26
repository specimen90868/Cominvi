using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Nominas
{
    public partial class frmListaEmpresas : Form
    {
        public frmListaEmpresas()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Empresas.Core.Empresas> lstEmpresas;
        #endregion

        private void frmListaEmpresas_Load(object sender, EventArgs e)
        {
            dgvEmpresas.RowHeadersVisible = false;
            CargaPerfil();
            ListaEmpresas();
        }

        private void ListaEmpresas()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;
            try
            {
                cnx.Open();
                lstEmpresas = eh.obtenerEmpresas();
                cnx.Close();
                cnx.Dispose();

                var em = from e in lstEmpresas 
                         select new 
                         { IdEmpresa = e.idempresa, Nombre = e.nombre, RFC = e.rfc, Registro = e.registro + e.digitoverificador,
                         Representante = e.representante };
                dgvEmpresas.DataSource = em.ToList();

                for (int i = 0; i < dgvEmpresas.Columns.Count; i++)
                {
                    dgvEmpresas.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Empresas");
            
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

        #region TEXBOX BUSCAR
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
                    var em = from b in lstEmpresas
                             select new
                             {
                                 IdEmpresa = b.idempresa,
                                 Nombre = b.nombre,
                                 RFC = b.rfc,
                                 Registro = b.registro + b.digitoverificador,
                                 Representante = b.representante
                             };
                    dgvEmpresas.DataSource = em.ToList();
                }
                else 
                {
                    var busqueda = from b in lstEmpresas
                                   where b.nombre.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       IdEmpresa = b.idempresa,
                                       Nombre = b.nombre,
                                       RFC = b.rfc,
                                       Registro = b.registro + b.digitoverificador,
                                       Representante = b.representante
                                   };
                    dgvEmpresas.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empresa...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }
        #endregion

        private void dgvEmpresas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(1);
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar la empresa?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvEmpresas.CurrentCell.RowIndex;
                int idempresa = int.Parse(dgvEmpresas.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
                eh.Command = cmd;
                Empresas.Core.Empresas em = new Empresas.Core.Empresas();
                em.idempresa = idempresa;
                try
                {
                    cnx.Open();
                    eh.bajaEmpresa(em);
                    cnx.Close();
                    cnx.Dispose();
                    ListaEmpresas();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmEmpresas e = new frmEmpresas();
            e.MdiParent = this.MdiParent;
            e.OnNuevaEmpresa += e_OnNuevaEmpresa;
            e._lista = true;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvEmpresas.CurrentCell.RowIndex;
                e._idempresa = int.Parse(dgvEmpresas.Rows[fila].Cells[0].Value.ToString());
            }
            e._tipoOperacion = edicion;
            e.Show();
        }

        void e_OnNuevaEmpresa(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
            {
                ListaEmpresas();
            }
        }

    }
}
