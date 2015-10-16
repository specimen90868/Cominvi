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
    public partial class frmListaProgramacionConceptos : Form
    {
        public frmListaProgramacionConceptos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<ProgramacionConcepto.Core.ProgramacionConcepto> lstProgramacion;
        List<Empleados.Core.Empleados> lstEmpleados;
        ProgramacionConcepto.Core.ProgramacionHelper pch;
        Empleados.Core.EmpleadosHelper eh;
        #endregion

        private void frmListaProgramacionConceptos_Load(object sender, EventArgs e)
        {
            ListaEmpleados();
        }

        private void ListaEmpleados()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            
            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            pch = new ProgramacionConcepto.Core.ProgramacionHelper();
            pch.Command = cmd;            

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            ProgramacionConcepto.Core.ProgramacionConcepto programacion = new ProgramacionConcepto.Core.ProgramacionConcepto();
            programacion.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                lstProgramacion = pch.obtenerProgramaciones(programacion);
                cnx.Close();
                cnx.Dispose(); 
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            var program = from e in lstEmpleados
                          join p in lstProgramacion on e.idtrabajador equals p.idtrabajador
                          select new { 
                              IdTrabajador = e.idtrabajador,
                              NoEmpleado = e.noempleado,
                              Nombre = e.nombrecompleto,
                              Concepto = p.concepto,
                              Cantidad = p.cantidad,
                              FechaTermino = p.fechafin.ToShortDateString(),
                          };

            dgvProgramacionConcepto.DataSource = program.ToList();

            for (int i = 0; i < dgvProgramacionConcepto.Columns.Count; i++)
            {
                dgvProgramacionConcepto.AutoResizeColumn(i);
            }

            dgvProgramacionConcepto.Columns["IdTrabajador"].Visible = false;
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("ProgramacionConcepto");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "ProgramacionConcepto":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].consulta);
                        toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].modificar);
                        break;
                }
            }
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmProgramacionConcepto pc = new frmProgramacionConcepto();
            pc.OnNuevaProgramacion += pc_OnNuevaProgramacion;
            pc.MdiParent = this.MdiParent;
            if (edicion != GLOBALES.NUEVO)
            {
                fila = dgvProgramacionConcepto.CurrentCell.RowIndex;
                pc._idEmpleado = int.Parse(dgvProgramacionConcepto.Rows[fila].Cells[0].Value.ToString());
                pc._nombreEmpleado = dgvProgramacionConcepto.Rows[fila].Cells[2].Value.ToString();
            }
            pc._tipoOperacion = edicion;
            pc.Show();
        }

        void pc_OnNuevaProgramacion(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaEmpleados();
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    var program = from em in lstEmpleados
                                  join p in lstProgramacion on em.idtrabajador equals p.idtrabajador
                                  select new
                                  {
                                      IdTrabajador = em.idtrabajador,
                                      NoEmpleado = em.noempleado,
                                      Nombre = em.nombrecompleto,
                                      Concepto = p.concepto,
                                      Cantidad = p.cantidad,
                                      FechaTermino = p.fechafin,
                                  };
                    dgvProgramacionConcepto.DataSource = program.ToList();
                }
                else
                {
                    var busqueda = from b in lstEmpleados
                                   join p in lstProgramacion on b.idtrabajador equals p.idtrabajador
                                   where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       IdTrabajador = b.idtrabajador,
                                       NoEmpleado = b.noempleado,
                                       Nombre = b.nombrecompleto,
                                       Concepto = p.concepto,
                                       Cantidad = p.cantidad,
                                       FechaTermino = p.fechafin,
                                   };
                    dgvProgramacionConcepto.DataSource = busqueda.ToList();
                }
                dgvProgramacionConcepto.Columns["IdTrabajador"].Visible = false;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.NUEVO);
        }

        private void toolConsultar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }

        private void toolEditar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.MODIFICAR);
        }

        private void dgvProgramacionConcepto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }
    }
}
