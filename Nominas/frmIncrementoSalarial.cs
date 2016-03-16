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
    public partial class frmIncrementoSalarial : Form
    {
        public frmIncrementoSalarial()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Historial.Core.HistorialHelper hh;
        Modificaciones.Core.ModificacionesHelper mh;
        Empresas.Core.EmpresasHelper ph;
        Departamento.Core.DeptoHelper dh;
        Puestos.Core.PuestosHelper puestoh;
        int idperiodo, antiguedad;
        string nss, rp;
        #endregion

        #region DELEGADOS
        public delegate void delOnIncrementoSalarial();
        public event delOnIncrementoSalarial OnIncrementoSalarial;
        #endregion

        #region VARIABLES PUBLICAS
        public string _nombreEmpleado;
        public int _idempleado;
        #endregion

        private void frmIncrementoSalarial_Load(object sender, EventArgs e)
        {
            lblEmpleado.Text = _nombreEmpleado;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            puestoh = new Puestos.Core.PuestosHelper();
            puestoh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idempleado;
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

            List<Departamento.Core.Depto> lstDepartamento = new List<Departamento.Core.Depto>();
            List<Puestos.Core.Puestos> lstPuesto = new List<Puestos.Core.Puestos>();

            ph = new Empresas.Core.EmpresasHelper();
            ph.Command = cmd;
            Empresas.Core.Empresas p = new Empresas.Core.Empresas();
            p.idempresa = GLOBALES.IDEMPRESA;

            Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
            puesto.idempresa = GLOBALES.IDEMPRESA;

            Departamento.Core.Depto d = new Departamento.Core.Depto();
            d.idempresa = GLOBALES.IDEMPRESA;

            try {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                rp = (string)ph.obtenerRegistroPatronal(p);
                lstDepartamento = dh.obtenerDepartamentos(d);
                lstPuesto = puestoh.obtenerPuestos(puesto);
                cnx.Close();
                cnx.Dispose();

                for (int i = 0; i < lstEmpleado.Count; i++)
                {
                    idperiodo = lstEmpleado[i].idperiodo;
                    antiguedad = lstEmpleado[i].antiguedad;
                    nss = lstEmpleado[i].nss + lstEmpleado[i].digitoverificador;
                }
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var dato = from emp in lstEmpleado
                       join depto in lstDepartamento on emp.iddepartamento equals depto.id
                       join pto in lstPuesto on emp.idpuesto equals pto.id
                       select new
                       {
                           emp.noempleado,
                           emp.nombrecompleto,
                           depto.descripcion,
                           pto.nombre
                       };
            foreach (var inf in dato)
            {
                mtxtNoEmpleado.Text = inf.noempleado;
                txtDepartamento.Text = inf.descripcion;
                txtPuesto.Text = inf.nombre;
            }
        }

        private void txtSDI_Leave(object sender, EventArgs e)
        {
            if (txtSDI.Text.Length != 0)
                calculo(double.Parse(txtSDI.Text), 0);
            else
                txtSD.Clear();
        }

        private void txtSueldo_Leave(object sender, EventArgs e)
        {
            if (txtSueldo.Text.Length != 0)
                calculo(double.Parse(txtSueldo.Text), 1);
            else
                txtSD.Clear();
        }

        private void calculo(double valor, int tipo)
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
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                this.Dispose();
            }

            switch (tipo)
            {
                case 0: 
                    txtSD.Text = (double.Parse(txtSDI.Text) / FactorDePago).ToString("F6");
                    txtSueldo.Text = (double.Parse(txtSD.Text) * DiasDePago).ToString("F6");
                    break;
                case 1:
                    txtSD.Text = (double.Parse(txtSueldo.Text) / DiasDePago).ToString("F6");
                    txtSDI.Text = (double.Parse(txtSD.Text) * FactorDePago).ToString("F6");
                    break;
            }
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            hh = new Historial.Core.HistorialHelper();
            mh = new Modificaciones.Core.ModificacionesHelper();
            eh.Command = cmd;
            hh.Command = cmd;
            mh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idempleado;
            empleado.sdi = double.Parse(txtSDI.Text);
            empleado.sd = double.Parse(txtSD.Text);
            empleado.sueldo = double.Parse(txtSueldo.Text);

            Historial.Core.Historial historia = new Historial.Core.Historial();
            historia.idtrabajador = _idempleado;
            historia.idempresa = GLOBALES.IDEMPRESA;
            historia.tipomovimiento = GLOBALES.mMODIFICACIONSALARIO;
            historia.valor = double.Parse(txtSDI.Text);
            historia.fecha_imss = dtpFecha.Value;
            historia.fecha_sistema = DateTime.Now;
            historia.motivobaja = 0;

            Modificaciones.Core.Modificaciones mod = new Modificaciones.Core.Modificaciones();
            mod.idtrabajador = _idempleado;
            mod.idempresa = GLOBALES.IDEMPRESA;
            mod.registropatronal = rp;
            mod.nss = nss;
            mod.fecha = dtpFecha.Value;
            mod.sdi = double.Parse(txtSDI.Text);

            try {
                cnx.Open();
                eh.actualizaSueldo(empleado);
                hh.insertarHistorial(historia);
                mh.insertaModificacion(mod);
                cnx.Close();
                cnx.Dispose();

                MessageBox.Show("Incremento aplicado.", "Confirmación");

                if (OnIncrementoSalarial != null)
                    OnIncrementoSalarial();
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
            this.Dispose();
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
