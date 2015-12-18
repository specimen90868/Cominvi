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
    public partial class frmListaSalario : Form
    {
        public frmListaSalario()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Salario.Core.Salarios> lstSalarios;
        #endregion

        private void ListaSalario()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
            sh.Command = cmd;

            try
            {
                cnx.Open();
                lstSalarios = sh.obtenerSalarios();
                cnx.Close();
                cnx.Dispose();

                var salario = from s in lstSalarios
                             select new
                             {
                                 Id = s.idsalario,
                                 Periodo = s.periodo,
                                 Valor = s.valor
                             };
                dgvSalario.DataSource = salario.ToList();

                for (int i = 0; i < dgvSalario.Columns.Count; i++)
                {
                    dgvSalario.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Salario minimo");

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

        private void Seleccion(int edicion)
        {
            frmSalario s = new frmSalario();
            s.MdiParent = this.MdiParent;
            s.OnNuevoSalario += s_OnNuevoSalario;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvSalario.CurrentCell.RowIndex;
                s._idsalario = int.Parse(dgvSalario.Rows[fila].Cells[0].Value.ToString());
            }
            s._tipoOperacion = edicion;
            s.Show();
        }

        void s_OnNuevoSalario(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaSalario();
        }

        private void frmListaSalario_Load(object sender, EventArgs e)
        {
            dgvSalario.RowHeadersVisible = false;
            CargaPerfil();
            ListaSalario();
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el salario mínimo?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvSalario.CurrentCell.RowIndex;
                int id = int.Parse(dgvSalario.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
                sh.Command = cmd;
                Salario.Core.Salarios salario = new Salario.Core.Salarios();
                salario.idsalario = id;
                
                try
                {
                    cnx.Open();
                    sh.bajaSalario(salario);
                    cnx.Close();
                    cnx.Dispose();
                    ListaSalario();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }
    }
}
