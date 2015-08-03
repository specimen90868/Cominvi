using MySql.Data.MySqlClient;
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
        MySqlConnection cnx;
        MySqlCommand cmd;
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

            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
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
                    antiguedad = lstEmpleado[i].antiguedad;
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
                cnx = new MySqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new MySqlCommand();
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
            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            LayoutMovimientos.Core.LayoutHelper lh = new LayoutMovimientos.Core.LayoutHelper();
            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            lh.Command = cmd;
            eh.Command = cmd;

            LayoutMovimientos.Core.LayoutMovimientos lm = new LayoutMovimientos.Core.LayoutMovimientos();

            List<Empresas.Core.Empresas> lstEmpresa = new List<Empresas.Core.Empresas>();
            

            for (int i = 0; i < lstEmpleado.Count; i++)
            {
                lm.idtrabajador = lstEmpleado[i].idtrabajador;
                lm.idempresa = lstEmpleado[i].idempresa;
                lm.idcliente = lstEmpleado[i].idcliente;
                lm.movimiento = 2;
                lm.nombres = lstEmpleado[i].nombres;
                lm.paterno = lstEmpleado[i].paterno;
                lm.materno = lstEmpleado[i].materno;
                lm.sdi = lstEmpleado[i].sdi;
                lm.sdinuevo = decimal.Parse(txtSDI.Text);
                lm.nss = lstEmpleado[i].nss;
                lm.digitonss = lstEmpleado[i].digitoverificador;
                lm.fecha_ingreso = lstEmpleado[i].fechaingreso;
                lm.fecha_sistema = DateTime.Now;
                lm.curp = lstEmpleado[i].curp;
                lm.generado = 0;

                try
                {
                    cnx.Open();
                    lstEmpresa = eh.obtenerEmpresa(lm.idempresa);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                for (int j = 0; j < lstEmpresa.Count; j++)
                {
                    lm.registro = lstEmpresa[j].registro;
                    lm.digitoregistro = lstEmpresa[j].digitoverificador;
                }
            }

            try
            {
                cnx.Open();
                lh.insertaLayoutMovimiento(lm);
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
