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
    public partial class frmListaInfonavit : Form
    {
        public frmListaInfonavit()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Empleados.Core.Empleados> lstEmpleados;
        List<Infonavit.Core.Infonavit> lstInfonavit;
        List<Catalogos.Core.Catalogo> lstCatalogo;
        #endregion

        private void ListaEmpleados()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            Infonavit.Core.InfonavitHelper ih = new Infonavit.Core.InfonavitHelper();
            eh.Command = cmd;
            ih.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
            inf.idempresa = GLOBALES.IDEMPRESA;

            Catalogos.Core.CatalogosHelper ch = new Catalogos.Core.CatalogosHelper();
            ch.Command = cmd;
            Catalogos.Core.Catalogo cat = new Catalogos.Core.Catalogo();
            cat.grupodescripcion = "ESTATUS INFONAVIT";

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                lstInfonavit = ih.obtenerInfonavits(inf);
                lstCatalogo = ch.obtenerGrupo(cat);
                cnx.Close();
                cnx.Dispose();

                var em = from e in lstEmpleados
                         join i in lstInfonavit on e.idtrabajador equals i.idtrabajador
                         join c in lstCatalogo on i.estatus equals c.id
                         select new
                         {
                             IdTrabajador = e.idtrabajador,
                             NoEmpleado = e.noempleado,
                             Nombre = e.nombrecompleto,
                             Credito = i.credito,
                             Descuento = i.descuento == GLOBALES.dPORCENTAJE ? "PORCENTAJE" :
                             i.descuento == GLOBALES.dVSMDF ? "VSMDF" : "PESOS",
                             Valor = i.valordescuento,
                             Activo = i.activo ? "ACTIVO" : "NO ACTIVO",
                             Estatus = c.descripcion
                         };

                dgvInfonavit.DataSource = em.ToList();

                for (int i = 0; i < dgvInfonavit.Columns.Count; i++)
                {
                    dgvInfonavit.AutoResizeColumn(i);
                }
                dgvInfonavit.Columns["IdTrabajador"].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Infonavit");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Consultar": toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Editar": toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Modificacion": toolModificar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                }
            }
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmInfonavit i = new frmInfonavit();
            i.MdiParent = this.MdiParent;

            if (edicion != GLOBALES.NUEVO)
            {
                fila = dgvInfonavit.CurrentCell.RowIndex;
                i._idEmpleado = int.Parse(dgvInfonavit.Rows[fila].Cells[0].Value.ToString());
                i._nombreEmpleado = dgvInfonavit.Rows[fila].Cells[2].Value.ToString();
                i._estatusInfonavit = dgvInfonavit.Rows[fila].Cells[6].Value.ToString() == "ACTIVO" ? true : false;
            }

            i.OnNuevoInfonavit += i_OnNuevoInfonavit;
            i._modificar = 0;
            i._tipoOperacion = edicion;
            i.Show();
        }

        void i_OnNuevoInfonavit(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaEmpleados();
        }

        private void frmListaInfonavit_Load(object sender, EventArgs e)
        {
            dgvInfonavit.RowHeadersVisible = false;
            ListaEmpleados();
            CargaPerfil();
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

        private void toolModificar_Click(object sender, EventArgs e)
        {
            //int fila = 0;
            //fila = dgvInfonavit.CurrentCell.RowIndex;
            //frmModificacionInfonavit mi = new frmModificacionInfonavit();
            //mi.OnInfonavit += mi_OnInfonavit;
            //mi._idEmpleado = int.Parse(dgvInfonavit.Rows[fila].Cells[0].Value.ToString());
            //mi._nombreEmpleado = dgvInfonavit.Rows[fila].Cells[2].Value.ToString();
            //mi._activo = dgvInfonavit.Rows[fila].Cells[7].Value.ToString();
            //mi.MdiParent = this.MdiParent;
            //mi.Show();

            int fila = 0;
            fila = dgvInfonavit.CurrentCell.RowIndex;
            frmInfonavit i = new frmInfonavit();
            i._tipoOperacion = GLOBALES.MODIFICAR;
            i.OnNuevoInfonavit += i_OnNuevoInfonavit;
            i._idEmpleado = int.Parse(dgvInfonavit.Rows[fila].Cells[0].Value.ToString());
            i._nombreEmpleado = dgvInfonavit.Rows[fila].Cells[2].Value.ToString();
            i._estatusInfonavit = dgvInfonavit.Rows[fila].Cells[6].Value.ToString() == "ACTIVO" ? true : false;
            i._modificar = 1;
            i.MdiParent = this.MdiParent;
            i.Show();

            
        }

        void mi_OnInfonavit()
        {
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
                    var em = from emp in lstEmpleados
                             join i in lstInfonavit on emp.idtrabajador equals i.idtrabajador
                             join c in lstCatalogo on i.estatus equals c.id
                             select new
                             {
                                 IdTrabajador = emp.idtrabajador,
                                 NoEmpleado = emp.noempleado,
                                 Nombre = emp.nombrecompleto,
                                 Credito = i.credito,
                                 Descuento = i.descuento == GLOBALES.dPORCENTAJE ? "PORCENTAJE" :
                                 i.descuento == GLOBALES.dVSMDF ? "VSMDF" : "PESOS",
                                 Valor = i.valordescuento,
                                 Activo = i.activo ? "ACTIVO" : "NO ACTIVO",
                                 Estatus = c.descripcion
                             };
                    dgvInfonavit.DataSource = em.ToList();
                }
                else
                {
                    var busqueda = from b in lstEmpleados
                                   join i in lstInfonavit on b.idtrabajador equals i.idtrabajador
                                   join c in lstCatalogo on i.estatus equals c.id
                                   where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper()) || b.noempleado.Contains(txtBuscar.Text)
                                   select new
                                   {
                                       IdTrabajador = b.idtrabajador,
                                       NoEmpleado = b.noempleado,
                                       Nombre = b.nombrecompleto,
                                       Credito = i.credito,
                                       Descuento = i.descuento == GLOBALES.dPORCENTAJE ? "PORCENTAJE" :
                                       i.descuento == GLOBALES.dVSMDF ? "VSMDF" : "PESOS",
                                       Valor = i.valordescuento,
                                       Activo = i.activo ? "ACTIVO" : "NO ACTIVO",
                                       Estatus = c.descripcion
                                   };
                    dgvInfonavit.DataSource = busqueda.ToList();
                }
                dgvInfonavit.Columns["IdTrabajador"].Visible = false;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void dgvInfonavit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = 0;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Infonavit.Core.InfonavitHelper ih = new Infonavit.Core.InfonavitHelper();
            ih.Command = cmd;

            fila = dgvInfonavit.CurrentCell.RowIndex;
            Infonavit.Core.Infonavit i = new Infonavit.Core.Infonavit();
            i.idtrabajador = int.Parse(dgvInfonavit.Rows[fila].Cells[0].Value.ToString());

            try
            {
                cnx.Open();
                int existe = (int)ih.existeInfonavit(i);
                cnx.Close();
                cnx.Dispose();

                if (!existe.Equals(0))
                    toolNuevo.Enabled = false;
                else
                    toolNuevo.Enabled = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }
    }
}
