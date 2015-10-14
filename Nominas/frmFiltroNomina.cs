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
    public partial class frmFiltroNomina : Form
    {
        public frmFiltroNomina()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Departamento.Core.DeptoHelper dh;
        Puestos.Core.PuestosHelper ph;
        #endregion

        #region VARIABLES PUBLICAS
        public int _filtro;
        #endregion

        #region DELEGADOS
        public delegate void delOnFiltro(int filtro, int de, int hasta);
        public event delOnFiltro OnFiltro;
        #endregion

        private void frmFiltroNomina_Load(object sender, EventArgs e)
        {
            cmbDe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbDe.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbHasta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbHasta.AutoCompleteSource = AutoCompleteSource.ListItems;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            switch (_filtro)
            {
                case 0:
                    dh = new Departamento.Core.DeptoHelper();
                    dh.Command = cmd;
                    Departamento.Core.Depto depto = new Departamento.Core.Depto();
                    depto.idempresa = GLOBALES.IDEMPRESA;
                    depto.estatus = GLOBALES.ACTIVO;
                    List<Departamento.Core.Depto> lstDeptosDe = new List<Departamento.Core.Depto>();
                    List<Departamento.Core.Depto> lstDeptosHasta = new List<Departamento.Core.Depto>();
                    try {
                        cnx.Open();
                        lstDeptosDe = dh.obtenerDepartamentos(depto);
                        lstDeptosHasta = dh.obtenerDepartamentos(depto);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error"); }

                    cmbDe.DataSource = lstDeptosDe;
                    cmbDe.DisplayMember = "descripcion";
                    cmbDe.ValueMember = "id";

                    cmbHasta.DataSource = lstDeptosHasta;
                    cmbHasta.DisplayMember = "descripcion";
                    cmbHasta.ValueMember = "id";

                    break;

                case 1:
                    ph = new Puestos.Core.PuestosHelper();
                    ph.Command = cmd;
                    Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
                    puesto.idempresa = GLOBALES.IDEMPRESA;
                    puesto.estatus = GLOBALES.ACTIVO;
                    List<Puestos.Core.Puestos> lstPuestosDe = new List<Puestos.Core.Puestos>();
                    List<Puestos.Core.Puestos> lstPuestosHasta = new List<Puestos.Core.Puestos>();
                    try
                    {
                        cnx.Open();
                        lstPuestosDe = ph.obtenerPuestos(puesto);
                        lstPuestosHasta = ph.obtenerPuestos(puesto);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

                    cmbDe.DataSource = lstPuestosDe;
                    cmbDe.DisplayMember = "descripcion";
                    cmbDe.ValueMember = "id";

                    cmbHasta.DataSource = lstPuestosHasta;
                    cmbHasta.DisplayMember = "descripcion";
                    cmbHasta.ValueMember = "id";
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (OnFiltro != null)
                OnFiltro(_filtro, int.Parse(cmbDe.SelectedValue.ToString()), int.Parse(cmbHasta.SelectedValue.ToString()));
            this.Dispose();
        }

        private void cmbDe_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
