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
    public partial class frmPerfiles : Form
    {
        public frmPerfiles()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idperfil;
        #endregion

        #region VARIABLES GLOBALES
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        SqlConnection cnx;
        SqlCommand cmd;
        Perfil.Core.PerfilesHelper ph;
        Autorizaciones.Core.AutorizacionHelper auth;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoPerfil(int edicion);
        public event delOnNuevoPerfil OnNuevoPerfil;
        #endregion

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            Guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            Guardar(0);
        }

        private void Guardar(int tipoGuardar)
        {
            //SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            //string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            //if (!control.Equals(""))
            //{
            //    MessageBox.Show("Falta el campo: " + control, "Información");
            //    return;
            //}

            int idperfil;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ph = new Perfil.Core.PerfilesHelper();
            ph.Command = cmd;

            auth = new Autorizaciones.Core.AutorizacionHelper();
            auth.Command = cmd;

            ///ASIGNACION DEL NOMBRE DEL PERFIL A LA CLASE PERFILES
            Perfil.Core.Perfiles p = new Perfil.Core.Perfiles();
            p.nombre = txtNombre.Text;

            ///ASIGNACION DE LOS CHECKBOXES A LA CLASE AUTORIZACION
            List<Autorizaciones.Core.Autorizacion> lstAutorizacion = new List<Autorizaciones.Core.Autorizacion>();
            
            var controls = this.Controls.Cast<Control>();
            foreach (Control c in controls.Where(c => c.GetType() == typeof(CheckBox)))
            {
                Autorizaciones.Core.Autorizacion a = new Autorizaciones.Core.Autorizacion();
                switch ((c as CheckBox).Text)
                {
                    case "Recursos Humanos":
                        a.idacceso = 1;
                        a.acceso = chkRecursosHumanos.Checked;
                        break;
                    case "Seguro Social":
                        a.idacceso = 2;
                        a.acceso = chkSeguroSocial.Checked;
                        break;
                    case "Nominas":
                        a.idacceso = 3;
                        a.acceso = chkNominas.Checked;
                        break;
                    case "Catálogos":
                        a.idacceso = 4;
                        a.acceso = chkCatalogos.Checked;
                        break;
                    case "Configuración":
                        a.idacceso = 5;
                        a.acceso = chkConfiguracion.Checked;
                        break;
                }
                lstAutorizacion.Add(a);
            }

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ph.insertaPerfil(p);
                        idperfil = (int)ph.obtenerIdPerfil(p);
                        auth.insertaAutorizacion(idperfil, lstAutorizacion);
                        cnx.Close();
                        cnx.Dispose();

                        if (OnNuevoPerfil != null)
                            OnNuevoPerfil(0);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
                case 2:
                    try
                    {
                        cnx.Open();
                        p.idperfil = _idperfil;
                        ph.actualizaPerfil(p);
                        auth.actualizaAutorizacion(_idperfil, lstAutorizacion);
                        cnx.Close();
                        cnx.Dispose();

                        if (OnNuevoPerfil != null)
                            OnNuevoPerfil(2);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    limpiar(this, typeof(TextBox));
                    break;
                case 1:
                    this.Dispose();
                    break;
            }
        }

        #region FUNCION LIMPIAR TEXTBOX E INHABILITAR CONTROLES
        private void limpiar(Control control, Type tipo)
        {
            var controls = control.Controls.Cast<Control>();
            foreach (Control c in controls.Where(c => c.GetType() == tipo))
            {
                (c as TextBox).Clear();
            }
        }

        private void inhabilitar(Control control, Type tipo)
        {
            var controls = control.Controls.Cast<Control>();
            foreach (Control c in controls.Where(c => c.GetType() == tipo))
            {
                (c as TextBox).Enabled = false;
            }
            toolGuardarCerrar.Enabled = false;
            toolGuardarNuevo.Enabled = false;
        }
        #endregion

        private void frmPerfiles_Load(object sender, EventArgs e)
        {
            CargaCombos();
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ph = new Perfil.Core.PerfilesHelper();
                ph.Command = cmd;

                Perfil.Core.Perfiles p = new Perfil.Core.Perfiles();
                p.idperfil = _idperfil;
                List<Perfil.Core.Perfiles> lstPerfil;

                Autorizaciones.Core.AutorizacionHelper ah = new Autorizaciones.Core.AutorizacionHelper();
                ah.Command = cmd;

                List<Autorizaciones.Core.Autorizacion> lstAuth = new List<Autorizaciones.Core.Autorizacion>();
                List<Autorizaciones.Core.Permisos> lstPermisos = new List<Autorizaciones.Core.Permisos>();

                try
                {
                    cnx.Open();
                    lstPerfil = ph.obtenerPerfile(p);
                    lstAuth = ah.obtenerModulos(_idperfil);
                    lstPermisos = ah.obtenerPermisos(_idperfil);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstPerfil.Count; i++)
                    {
                        txtNombre.Text = lstPerfil[i].nombre;
                    }

                    for (int i = 0; i < lstAuth.Count; i++)
                    {
                        switch (lstAuth[i].idacceso)
                        {
                            case 1:
                                chkRecursosHumanos.Checked = lstAuth[i].acceso;
                                break; 
                            case 2:
                                chkSeguroSocial.Checked = lstAuth[i].acceso;
                                break;
                            case 3:
                                chkNominas.Checked = lstAuth[i].acceso;
                                break;
                            case 4:
                                chkCatalogos.Checked = lstAuth[i].acceso;
                                break;
                            case 5:
                                chkConfiguracion.Checked = lstAuth[i].acceso;
                                break;
                        } 
                    }

                    var a = from b in lstPermisos
                            select new
                            {
                                b.id,
                                b.idperfil,
                                Nombre = b.nombre,
                                Permiso = b.permiso,
                                Accion = (b.accion ? "HABILITADO" : "DESHABILITADO")
                            };

                    dgvPermisos.DataSource = a.ToList();
                    dgvPermisos.Columns[0].Visible = false;
                    dgvPermisos.Columns[1].Visible = false;

                    for (int i = 0; i < dgvPermisos.Columns.Count; i++)
                    {
                        dgvPermisos.AutoResizeColumn(i);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Perfil";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(ComboBox));
                    GLOBALES.INHABILITAR(this, typeof(CheckBox));
                    btnAgregar.Enabled = false;
                    btnQuitar.Enabled = false;
                    toolGuardarCerrar.Enabled = false;
                    toolGuardarNuevo.Enabled = false;
                }
                else
                    toolTitulo.Text = "Edición Perfil";
            }
            else
            {
                cmbMenus.Enabled = false;
                cmbPermiso.Enabled = false;
                chkAccion.Enabled = false;
                btnAgregar.Enabled = false;
                btnQuitar.Enabled = false;
            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CargaCombos()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Autorizaciones.Core.AutorizacionHelper ah = new Autorizaciones.Core.AutorizacionHelper();
            ah.Command = cmd;

            List<Autorizaciones.Core.CatalogoMenu> lstMenus = new List<Autorizaciones.Core.CatalogoMenu>();
            List<Autorizaciones.Core.CatalogoPermisos> lstPermisos = new List<Autorizaciones.Core.CatalogoPermisos>();
            try {
                cnx.Open();
                lstMenus = ah.obtenerCatalogoMenu();
                lstPermisos = ah.obtenerCatalogoPermisos();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener catalogo de menus. \r\n \r\n" + error.Message,"Error");
            }

            cmbMenus.DataSource = lstMenus.ToList();
            cmbMenus.DisplayMember = "nombre";
            cmbMenus.ValueMember = "idmenu";

            cmbPermiso.DataSource = lstPermisos.ToList();
            cmbPermiso.DisplayMember = "permiso";
            cmbPermiso.ValueMember = "id";

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int idperfil = 0;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Autorizaciones.Core.AutorizacionHelper ah = new Autorizaciones.Core.AutorizacionHelper();
            ah.Command = cmd;

            Perfil.Core.PerfilesHelper ph = new Perfil.Core.PerfilesHelper();
            ph.Command = cmd;

            Perfil.Core.Perfiles p = new Perfil.Core.Perfiles();
            p.nombre = txtNombre.Text;

            Autorizaciones.Core.PermisosOperaciones permisos = new Autorizaciones.Core.PermisosOperaciones();
            permisos.idmenu = int.Parse(cmbMenus.SelectedValue.ToString());
            permisos.idcatpermiso = int.Parse(cmbPermiso.SelectedValue.ToString());
            permisos.accion = chkAccion.Checked;

            try {
                cnx.Open();
                idperfil = (int)ph.obtenerIdPerfil(p);
                permisos.idperfil = idperfil;
                ah.insertarPermiso(permisos);
                cnx.Close();
            }
            catch {
                MessageBox.Show("Error: Al ingresar el permiso.", "Error");
            }

            List<Autorizaciones.Core.Permisos> lstPermisos = new List<Autorizaciones.Core.Permisos>();

            try
            {
                cnx.Open();
                lstPermisos = ah.obtenerPermisos(idperfil);
                cnx.Close();
                cnx.Dispose();
            }
            catch {
                MessageBox.Show("Error: Al cargar los permisos en el Grid.", "Error");
            }

            var a = from b in lstPermisos select new 
            { 
                b.id,
                b.idperfil,
                Nombre = b.nombre,
                Permiso = b.permiso,
                Accion = (b.accion ? "HABILITADO" : "DESHABILITADO")
            };

            dgvPermisos.DataSource = a.ToList();
            dgvPermisos.Columns[0].Visible = false;
            dgvPermisos.Columns[1].Visible = false;
            
            for (int i = 0; i < dgvPermisos.Columns.Count; i++)
            {
                dgvPermisos.AutoResizeColumn(i);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            int fila = dgvPermisos.CurrentCell.RowIndex;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Autorizaciones.Core.AutorizacionHelper ah = new Autorizaciones.Core.AutorizacionHelper();
            ah.Command = cmd;

            Autorizaciones.Core.PermisosOperaciones op = new Autorizaciones.Core.PermisosOperaciones();
            op.id = int.Parse(dgvPermisos.Rows[fila].Cells["id"].Value.ToString());
            op.idperfil = int.Parse(dgvPermisos.Rows[fila].Cells["idperfil"].Value.ToString());

            try {
                cnx.Open();
                ah.eliminarPermiso(op);
                cnx.Close();
                cnx.Dispose();
            }
            catch 
            {
                MessageBox.Show("Error: Al eliminar el permiso.", "Error");
            }
        }
    }
}
