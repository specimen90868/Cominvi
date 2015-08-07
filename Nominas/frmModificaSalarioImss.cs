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
    public partial class frmModificaSalarioImss : Form
    {
        public frmModificaSalarioImss()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        int idperiodo;
        int antiguedad;
        List<Empleados.Core.Empleados> lstEmpleado;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idempleado = 0;
        public string _nombreCompleto = "";
        #endregion

        private void frmModificaSalarioImss_Load(object sender, EventArgs e)
        {
            lblEmpleado.Text = _nombreCompleto;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados em = new Empleados.Core.Empleados();
            em.idtrabajador = _idempleado;

            try
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(em);
                cnx.Close();
                cnx.Dispose();

                for (int i = 0; i < lstEmpleado.Count; i++)
                {
                    idperiodo = lstEmpleado[i].idperiodo;
                    antiguedad = lstEmpleado[i].antiguedadmod;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message,"Error");
            }

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (txtSueldo.Text.Length != 0)
            {
                int DiasDePago = 0;
                double FactorDePago = 0;
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                Factores.Core.FactoresHelper fh = new Factores.Core.FactoresHelper();
                Factores.Core.Factores f = new Factores.Core.Factores();

                ph.Command = cmd;
                fh.Command = cmd;

                p.idperiodo = idperiodo;
                f.anio = antiguedad;

                try
                {
                    cnx.Open();
                    DiasDePago = (int)ph.DiasDePago(p);
                    FactorDePago = double.Parse(fh.FactorDePago(f).ToString());
                    cnx.Close();
                    cnx.Dispose();

                    txtSD.Text = (double.Parse(txtSueldo.Text) / DiasDePago).ToString("F4");
                    txtSDI.Text = (double.Parse(txtSD.Text) * FactorDePago).ToString("F4");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    this.Dispose();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
         
            try
            {
                cnx.Open();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }
    }
}
