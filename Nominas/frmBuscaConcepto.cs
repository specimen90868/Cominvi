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
    public partial class frmBuscaConcepto : Form
    {
        public frmBuscaConcepto()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        public string _nombreEmpleado;
        #endregion

        #region DELEGADOS
        public delegate void delOnConcepto(int id);
        public event delOnConcepto OnConcepto;
        #endregion

        private void frmBuscaConcepto_Load(object sender, EventArgs e)
        {
            lblNombreEmpleado.Text = _nombreEmpleado;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;
            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;
            
            List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();

            try 
            {
                int periodo = 0; 
                cnx.Open();
                periodo = int.Parse(eh.obtenerDiasPeriodo(_idEmpleado).ToString());
                lstConceptos = ch.obtenerConceptos(concepto, periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            cmbConcepto.DataSource = lstConceptos.ToList();
            cmbConcepto.DisplayMember = "concepto";
            cmbConcepto.ValueMember = "id";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int existe = 0;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;
            Conceptos.Core.ConceptoTrabajador ct = new Conceptos.Core.ConceptoTrabajador();
            ct.idempleado = _idEmpleado;
            ct.idconcepto = int.Parse(cmbConcepto.SelectedValue.ToString());

            try 
            {
                cnx.Open();
                existe = (int)ch.existeConceptoTrabajador(ct);
                if (existe.Equals(0))
                {
                    ch.insertaConceptoTrabajador(ct);
                }
                cnx.Close();
                cnx.Dispose();

                if (!existe.Equals(0))
                {
                    MessageBox.Show("Concepto ya esta relacionado con el trabajador.", "Información");
                }

                if (OnConcepto != null)
                    OnConcepto(_idEmpleado);
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmBuscaConcepto_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
