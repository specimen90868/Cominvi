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
    public partial class frmListaCargaAguinaldo : Form
    {
        public frmListaCargaAguinaldo()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        SqlBulkCopy bulk;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string ruta, nombreEmpresa;
        string ExcelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;'";
        int idEmpresa;
        Empresas.Core.EmpresasHelper eh;
        Empleados.Core.EmpleadosHelper emph;
        Vacaciones.Core.VacacionesHelper vh;
        Conceptos.Core.ConceptosHelper ch;
        TablaIsr.Core.IsrHelper ih;
        Periodos.Core.PeriodosHelper ph;
        string noempleados = "";
        #endregion

        private void toolNuevo_Click(object sender, EventArgs e)
        {

        }

        private void toolCargar_Click(object sender, EventArgs e)
        {

        }

        private void toolLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void toolAplicar_Click(object sender, EventArgs e)
        {

        }
    }
}
