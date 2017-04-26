using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmGrupoSat : Form
    {
        public frmGrupoSat()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        SatCatalogos.Core.satCatalogosHelper sch;
        #endregion

        #region VARIABLES PUBLICAS
        public string _percepcionDeduccion;
        #endregion

        #region DELEGADOS
        public delegate void delOnSeleccion(string grupo);
        public event delOnSeleccion OnSeleccion;
        #endregion

        private void frmGrupoSat_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            sch = new SatCatalogos.Core.satCatalogosHelper();
            sch.Command = cmd;

            List<SatCatalogos.Core.satTipoDeduccion> lstTipoDeduccion = new List<SatCatalogos.Core.satTipoDeduccion>();
            List<SatCatalogos.Core.satTipoPercepcion> lstTipoPercepcion = new List<SatCatalogos.Core.satTipoPercepcion>();

            try
            {
                if (_percepcionDeduccion == "P") {
                    cnx.Open();
                    lstTipoPercepcion = sch.obtenerTipoPercepcion();
                    cnx.Close();
                }

                if (_percepcionDeduccion == "D") {
                    cnx.Open();
                    lstTipoDeduccion = sch.obtenerTipoDeduccion();
                    cnx.Close();
                }
                cnx.Dispose();
            }
            catch 
            {
                MessageBox.Show("Error: Al obtener los conceptos.", "Error");
                cnx.Dispose();
                this.Dispose();
            }

            dgvGrupos.RowHeadersVisible = false;
            dgvGrupos.ColumnHeadersVisible = false;
            if (_percepcionDeduccion == "P")
                dgvGrupos.DataSource = lstTipoPercepcion.ToList();
            if (_percepcionDeduccion == "D")
                dgvGrupos.DataSource = lstTipoDeduccion.ToList();
            dgvGrupos.Columns[0].Visible = false;
            dgvGrupos.Columns[1].Visible = true;
            dgvGrupos.Columns[2].Visible = true;

            for (int i = 0; i < dgvGrupos.Columns.Count; i++)
            {
                dgvGrupos.AutoResizeColumn(i);
            }
        }

        private void toolCancelar_Click(object sender, EventArgs e)
        {
            if (OnSeleccion != null)
            {
                OnSeleccion("000");
                this.Dispose();
            }
        }

        private void toolAceptar_Click(object sender, EventArgs e)
        {
            if(OnSeleccion != null)
            {
                int fila = dgvGrupos.CurrentCell.RowIndex;
                OnSeleccion(dgvGrupos.Rows[fila].Cells[1].Value.ToString());
                this.Dispose();
            }
        }
    }
}
