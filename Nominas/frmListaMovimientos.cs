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
    public partial class frmListaMovimientos : Form
    {
        public frmListaMovimientos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Empleados.Core.Empleados> lstEmpleados;
        List<Movimientos.Core.Movimientos> lstMovimientos;
        List<Conceptos.Core.Conceptos> lstConceptos;
        Empleados.Core.EmpleadosHelper eh;
        Movimientos.Core.MovimientosHelper mh;
        Conceptos.Core.ConceptosHelper ch;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNomina;
        #endregion

        private void ListaMovimientos()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            mh = new Movimientos.Core.MovimientosHelper();
            ch = new Conceptos.Core.ConceptosHelper();
            eh.Command = cmd;
            mh.Command = cmd;
            ch.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;

            Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
            mov.idempresa = GLOBALES.IDEMPRESA;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.idempresa = GLOBALES.IDEMPRESA;

            if (_tipoNomina == GLOBALES.NORMAL)
                empleado.estatus = GLOBALES.ACTIVO;
            if (_tipoNomina == GLOBALES.ESPECIAL)
                empleado.estatus = GLOBALES.INACTIVO;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                lstMovimientos = mh.obtenerMovimientos(mov);
                lstConceptos = ch.obtenerConceptos(concepto);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var datos = from e in lstEmpleados
                        join m in lstMovimientos on e.idtrabajador equals m.idtrabajador
                        join c in lstConceptos on m.idconcepto equals c.id
                        orderby e.nombrecompleto ascending
                        select new
                        {
                            Id = m.id,
                            IdTrabajador = e.idtrabajador,
                            NoEmpleado = e.noempleado,
                            Nombre = e.nombrecompleto,
                            Concepto = c.concepto,
                            Cantidad = m.cantidad,
                            FechaInicio = m.fechainicio,
                            FechaFin = m.fechafin
                        };
            dgvMovimientos.DataSource = datos.ToList();
            dgvMovimientos.Columns["Id"].Visible = false;
            dgvMovimientos.Columns["IdTrabajador"].Visible = false;

            for (int i = 0; i < dgvMovimientos.Columns.Count; i++)
            {
                dgvMovimientos.AutoResizeColumn(i);
            }
        }

        private void frmListaMovimientos_Load(object sender, EventArgs e)
        {
            CargaPerfil();
            ListaMovimientos();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Historial de movimientos");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Eliminar": toolEliminar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;

                }
            }
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmMovimientos m = new frmMovimientos();
            m.OnMovimientoNuevo +=m_OnMovimientoNuevo;
            m._tipoNomina = _tipoNomina;
            m._ventana = "Movimiento";
            m.Show();
        }

        void m_OnMovimientoNuevo()
        {
            ListaMovimientos();
        }

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            int fila = 0;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            mh = new Movimientos.Core.MovimientosHelper();
            mh.Command = cmd;

            fila = dgvMovimientos.CurrentCell.RowIndex;
            Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
            mov.id = int.Parse(dgvMovimientos.Rows[fila].Cells[0].Value.ToString());

            try
            {
                cnx.Open();
                mh.eliminaMovimiento(mov);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
            ListaMovimientos();
        }
    }
}
