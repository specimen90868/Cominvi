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
    public partial class frmListaHistorial : Form
    {
        public frmListaHistorial()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Historial.Core.HistorialHelper hh;
        Departamento.Core.DeptoHelper dh;
        Puestos.Core.PuestosHelper ph;
        List<Empleados.Core.Empleados> lstEmpleados;
        List<Historial.Core.Historial> lstHistorial;
        List<Departamento.Core.Depto> lstDepto;
        List<Puestos.Core.Puestos> lstPuesto;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idempleado;
        #endregion

        private void frmListaHistorial_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            hh = new Historial.Core.HistorialHelper();
            dh = new Departamento.Core.DeptoHelper();
            ph = new Puestos.Core.PuestosHelper();

            eh.Command = cmd;
            hh.Command = cmd;
            dh.Command = cmd;
            ph.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idempleado;
            Historial.Core.Historial historial = new Historial.Core.Historial();
            historial.idtrabajador = _idempleado;
            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            depto.idempresa = GLOBALES.IDEMPRESA;
            Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
            puesto.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleado(empleado);
                lstHistorial = hh.obtenerHistoriales(historial);
                lstDepto = dh.obtenerDepartamentos(depto);
                lstPuesto = ph.obtenerPuestos(puesto);
                cnx.Close();
                cnx.Dispose();

                var lista = from emp in lstEmpleados join his in lstHistorial on emp.idtrabajador equals his.idtrabajador
                            join d in lstDepto on his.iddepartamento equals d.id
                            join p in lstPuesto on his.idpuesto equals p.idpuesto orderby his.fecha_imss ascending
                         select new
                         {
                             IdTrabajador = emp.idtrabajador,
                             NoEmpleado = emp.noempleado,
                             Nombre = emp.nombrecompleto,
                             Movimiento = 
                                his.tipomovimiento == GLOBALES.mALTA ? "ALTA" :
                                his.tipomovimiento == GLOBALES.mMODIFICACIONSALARIO ? "MODIFICACION" :
                                his.tipomovimiento == GLOBALES.mREINGRESO ? "REINGRESO" :
                                his.tipomovimiento == GLOBALES.mBAJA ? "BAJA" :
                                his.tipomovimiento == GLOBALES.mCAMBIODEPARTAMENTO ? "CAMBIO DE DEPARTAMENTO" : "CAMBIO DE PUESTO",
                             SDI = his.valor,
                             FechaAplicacion = his.fecha_imss,
                             FechaSistema = his.fecha_sistema,
                             Depto = d.descripcion,
                             Puesto = p.nombre
                         };
                dgvHistorial.DataSource = lista.ToList();

                for (int i = 0; i < dgvHistorial.Columns.Count; i++)
                {
                    dgvHistorial.AutoResizeColumn(i);
                }
                dgvHistorial.Columns["IdTrabajador"].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

        }

        private void toolExportar_Click(object sender, EventArgs e)
        {

        }
    }
}
