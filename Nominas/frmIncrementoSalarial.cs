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
        Aplicaciones.Core.AplicacionesHelper aplih;
        int idperiodo, antiguedad;
        string nss, rp;
        bool departamento = false, puesto = false;
        int iddepto = 0, idpuesto = 0;
        DateTime inicioPeriodo, finPeriodo;
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
            cmbDepartamento.Enabled = false;
            cmbPuesto.Enabled = false;

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

            cmbDepartamento.DataSource = lstDepartamento.ToList();
            cmbDepartamento.DisplayMember = "descripcion";
            cmbDepartamento.ValueMember = "id";

            cmbPuesto.DataSource = lstPuesto.ToList();
            cmbPuesto.DisplayMember = "nombre";
            cmbPuesto.ValueMember = "idpuesto";

            var dato = from emp in lstEmpleado
                       join depto in lstDepartamento on emp.iddepartamento equals depto.id
                       join pto in lstPuesto on emp.idpuesto equals pto.idpuesto
                       select new
                       {
                           emp.noempleado,
                           emp.nombrecompleto,
                           depto.id,
                           depto.descripcion,
                           pto.idpuesto,
                           pto.nombre
                       };
            foreach (var inf in dato)
            {
                iddepto = inf.id;
                idpuesto = inf.id;
                cmbDepartamento.SelectedValue = inf.id;
                cmbPuesto.SelectedValue = inf.idpuesto;
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
            empleado.sdi = decimal.Parse(txtSDI.Text);
            empleado.sd = decimal.Parse(txtSD.Text);
            empleado.sueldo = decimal.Parse(txtSueldo.Text);

            Historial.Core.Historial historia = new Historial.Core.Historial();
            historia.idtrabajador = _idempleado;
            historia.idempresa = GLOBALES.IDEMPRESA;
            historia.tipomovimiento = GLOBALES.mMODIFICACIONSALARIO;
            historia.valor = decimal.Parse(txtSDI.Text);
            historia.fecha_imss = dtpFecha.Value;
            historia.fecha_sistema = DateTime.Now;
            historia.motivobaja = 0;

            if (departamento)
                historia.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            else
                historia.iddepartamento = iddepto;

            if (puesto)
                historia.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());
            else
                historia.idpuesto = idpuesto;

            Modificaciones.Core.Modificaciones mod = new Modificaciones.Core.Modificaciones();
            mod.idtrabajador = _idempleado;
            mod.idempresa = GLOBALES.IDEMPRESA;
            mod.registropatronal = rp;
            mod.nss = nss;
            mod.fecha = dtpFecha.Value;
            mod.sdi = decimal.Parse(txtSDI.Text);

            aplih = new Aplicaciones.Core.AplicacionesHelper();
            aplih.Command = cmd;
            Aplicaciones.Core.Aplicaciones aDepto = new Aplicaciones.Core.Aplicaciones();
            Aplicaciones.Core.Aplicaciones aPuesto = new Aplicaciones.Core.Aplicaciones();

            try {
                cnx.Open();
                eh.actualizaSueldo(empleado);
                hh.insertarHistorial(historia);
                mh.insertaModificacion(mod);

                if (departamento)
                {
                    Historial.Core.Historial historiaDepto = new Historial.Core.Historial();
                    historiaDepto.idtrabajador = _idempleado;
                    historiaDepto.idempresa = GLOBALES.IDEMPRESA;
                    historiaDepto.tipomovimiento = GLOBALES.mCAMBIODEPARTAMENTO;
                    historiaDepto.valor = decimal.Parse(txtSDI.Text);
                    historiaDepto.fecha_imss = dtpFecha.Value;
                    historiaDepto.fecha_sistema = DateTime.Now;
                    historiaDepto.motivobaja = 0;
                    historiaDepto.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
                    historiaDepto.idpuesto = idpuesto;
                    hh.insertarHistorial(historiaDepto);
                    
                    aDepto = new Aplicaciones.Core.Aplicaciones();
                    aDepto.idtrabajador = _idempleado;
                    aDepto.idempresa = GLOBALES.IDEMPRESA;
                    aDepto.iddeptopuesto = int.Parse(cmbDepartamento.SelectedValue.ToString());
                    aDepto.deptopuesto = "D";
                    aDepto.fecha = dtpFecha.Value.Date;
                    aDepto.registro = DateTime.Now.Date;
                    aDepto.idusuario = GLOBALES.IDUSUARIO;
                    aDepto.periodoinicio = inicioPeriodo;
                    aDepto.periodofin = finPeriodo;
                    aplih.insertaAplicacion(aDepto);
                }

                if (puesto)
                {
                    Historial.Core.Historial historiaPuesto = new Historial.Core.Historial();
                    historiaPuesto.idtrabajador = _idempleado;
                    historiaPuesto.idempresa = GLOBALES.IDEMPRESA;
                    historiaPuesto.tipomovimiento = GLOBALES.mCAMBIOPUESTO;
                    historiaPuesto.valor = decimal.Parse(txtSDI.Text);
                    historiaPuesto.fecha_imss = dtpFecha.Value;
                    historiaPuesto.fecha_sistema = DateTime.Now;
                    historiaPuesto.motivobaja = 0;
                    historiaPuesto.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
                    historiaPuesto.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());
                    hh.insertarHistorial(historiaPuesto);

                    aPuesto = new Aplicaciones.Core.Aplicaciones();
                    aPuesto.idtrabajador = _idempleado;
                    aPuesto.idempresa = GLOBALES.IDEMPRESA;
                    aPuesto.iddeptopuesto = int.Parse(cmbPuesto.SelectedValue.ToString());
                    aPuesto.deptopuesto = "P";
                    aPuesto.fecha = dtpFecha.Value.Date;
                    aPuesto.registro = DateTime.Now.Date;
                    aPuesto.idusuario = GLOBALES.IDUSUARIO;
                    aPuesto.periodoinicio = inicioPeriodo;
                    aPuesto.periodofin = finPeriodo;
                    aplih.insertaAplicacion(aPuesto);
                }

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

        private void chkCambioDeptoPto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCambioDeptoPto.Checked)
            {
                cmbDepartamento.Enabled = true;
                departamento = true;
            }
            else
            {
                cmbDepartamento.Enabled = false;
                departamento = false;
            }
        }

        private void chkModificaPuesto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkModificaPuesto.Checked)
            {
                puesto = true;
                cmbPuesto.Enabled = true;
            }
            else
            {
                puesto = false;
                cmbPuesto.Enabled = false;
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            List<CalculoNomina.Core.tmpPagoNomina> lstUltimaNomina = new List<CalculoNomina.Core.tmpPagoNomina>();

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;
            
            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();

            int dias = 0;
            try
            {
                cnx.Open();
                periodo.idperiodo = (int)eh.obtenerIdPeriodo(_idempleado);
                dias = int.Parse(ph.DiasDePago(periodo).ToString());
                lstUltimaNomina = nh.obtenerUltimaNominaTrabajador(GLOBALES.IDEMPRESA, _idempleado, dias);
                cnx.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener la ultima nómina del trabajador.", "Error");
                cnx.Dispose();
            }

            if (lstUltimaNomina.Count != 0)
                if (dtpFecha.Value.Date <= lstUltimaNomina[0].fechafin)
                {
                    MessageBox.Show("La fecha de aplicación seleccionada no es valida.\r\n\r\n" +
                                    "Se empalma con la ultima nomina del trabajador, por favor verifique.", "Información");
                }


            if (dias == 7)
            {
                DateTime dt = dtpFecha.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                inicioPeriodo = dt;
                finPeriodo = dt.AddDays(6);
            }
            else
            {
                if (dtpFecha.Value.Day <= 15)
                {
                    inicioPeriodo = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, 1);
                    finPeriodo = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, 15);
                }
                else
                {
                    inicioPeriodo = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, 16);
                    finPeriodo = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, DateTime.DaysInMonth(dtpFecha.Value.Year, dtpFecha.Value.Month));
                }
            }
        }
    }
}
