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
        List<Empleados.Core.Empleados> lstEmpleados;
        List<Historial.Core.Historial> lstHistorial;
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

            eh.Command = cmd;
            hh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idempleado;
            Historial.Core.Historial historial = new Historial.Core.Historial();
            historial.idtrabajador = _idempleado;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleado(empleado);
                lstHistorial = hh.obtenerHistoriales(historial);
                cnx.Close();
                cnx.Dispose();

                var lista = from emp in lstEmpleados join his in lstHistorial on emp.idtrabajador equals his.idtrabajador
                         select new
                         {
                             IdTrabajador = emp.idtrabajador,
                             Nombre = emp.nombrecompleto,
                             Movimiento = 
                                his.tipomovimiento == GLOBALES.mALTA ? "ALTA" :
                                his.tipomovimiento == GLOBALES.mMODIFICACIONSALARIO ? "MODIFICACION" :
                                his.tipomovimiento == GLOBALES.mREINGRESO ? "REINGRESO" : "BAJA",
                             SDI = his.valor,
                             FechaImss = his.fecha_imss,
                             FechaSistema = his.fecha_sistema
                         };
                dgvHistorial.DataSource = lista.ToList();

                for (int i = 0; i < dgvHistorial.Columns.Count; i++)
                {
                    dgvHistorial.AutoResizeColumn(i);
                }
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
