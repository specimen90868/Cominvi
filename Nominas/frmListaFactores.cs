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
    public partial class frmListaFactores : Form
    {
        public frmListaFactores()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Factores.Core.Factores> lstFactores;
        #endregion

        private void ListaFactores()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Factores.Core.FactoresHelper fh = new Factores.Core.FactoresHelper();
            fh.Command = cmd;

            try
            {
                cnx.Open();
                lstFactores = fh.obtenerFactores();
                cnx.Close();
                cnx.Dispose();

                var factor = from f in lstFactores
                             select new
                             {
                                 Id = f.idfactor,
                                 Año = f.anio,
                                 Valor = f.valor
                             };
                dgvFactores.DataSource = factor.ToList();

                for (int i = 0; i < dgvFactores.Columns.Count; i++)
                {
                    dgvFactores.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Factores");

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

        private void frmListaFactores_Load(object sender, EventArgs e)
        {
            dgvFactores.RowHeadersVisible = false;
            CargaPerfil();
            ListaFactores();
        }

        private void Seleccion(int edicion)
        {
            frmFactores f = new frmFactores();
            f.MdiParent = this.MdiParent;
            f.OnNuevoFactor += f_OnNuevoFactor;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvFactores.CurrentCell.RowIndex;
                f._idfactor = int.Parse(dgvFactores.Rows[fila].Cells[0].Value.ToString());
            }
            f._tipoOperacion = edicion;
            f.Show();
        }

        void f_OnNuevoFactor(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaFactores();
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el factor?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvFactores.CurrentCell.RowIndex;
                int id = int.Parse(dgvFactores.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                Factores.Core.FactoresHelper fh = new Factores.Core.FactoresHelper();
                fh.Command = cmd;
                Factores.Core.Factores factor = new Factores.Core.Factores();
                factor.idfactor = id;
                
                try
                {
                    cnx.Open();
                    fh.bajaFactor(factor);
                    cnx.Close();
                    cnx.Dispose();
                    ListaFactores();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }
    }
}
