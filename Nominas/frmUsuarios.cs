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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Usuarios.Core.UsuariosHelper uh;
        Perfil.Core.PerfilesHelper ph;
        CheckBox ck;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoUsuario(int edicion);
        public event delOnNuevoUsuario OnNuevoUsuario;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idusuario;
        #endregion

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            guardar(0);
        }

        private void guardar(int tipoGuardar)
        {
            //SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            uh = new Usuarios.Core.UsuariosHelper();
            uh.Command = cmd;

            Usuarios.Core.Usuarios u = new Usuarios.Core.Usuarios();
            u.nombre = txtNombre.Text;
            u.usuario = txtUsuario.Text;
            u.activo = true;
            u.idperfil = int.Parse(cmbPerfil.SelectedValue.ToString());

            string empresas = "";
            if (ck.Checked)
                u.empresas = "0";
            else {
                foreach (DataGridViewRow row in dgvEmpresas.Rows)
                {
                    if (row.Cells[2].Value != null)
                        if (bool.Parse(row.Cells[2].Value.ToString()))
                            empresas += row.Cells[0].Value.ToString() + ",";
                }
                u.empresas = empresas.Substring(0, empresas.Length - 1);
            }
            
            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        u.password = txtPassword.Text;
                        u.fecharegistro = DateTime.Now;
                        uh.insertaUsuario(u);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el usuario. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        u.idusuario = _idusuario;
                        cnx.Open();
                        uh.modificaUsuario(u);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el usuario. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    //limpiar(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoUsuario != null)
                        OnNuevoUsuario(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            CargaCombos();
            CargaEmpresas();
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                uh = new Usuarios.Core.UsuariosHelper();
                uh.Command = cmd;

                DataTable dtUsuario = new DataTable();
                string empresas = "";

                lblPassword.Visible = false;
                lblPassword2.Visible = false;
                txtPassword.Visible = false;
                txtPassword2.Visible = false;

                txtPassword.Text = "1";
                txtPassword2.Text = "1";

                try
                {
                    cnx.Open();
                    dtUsuario = uh.obtenerUsuario(_idusuario);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < dtUsuario.Rows.Count; i++)
                    {
                        txtNombre.Text = dtUsuario.Rows[i]["nombre"].ToString();
                        txtUsuario.Text = dtUsuario.Rows[i]["usuario"].ToString();
                        cmbPerfil.SelectedValue = int.Parse(dtUsuario.Rows[i]["idperfil"].ToString());
                        empresas = dtUsuario.Rows[i]["empresas"].ToString();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolNombreVentana.Text = "Consulta Usuario";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(DataGridView));
                }
                else
                    toolNombreVentana.Text = "Edición Usuario";

                if (empresas == "0")
                {
                    foreach (DataGridViewRow row in dgvEmpresas.Rows)
                    {
                        row.Cells[2].Value = true;
                        dgvEmpresas.EndEdit();
                    }
                }
                else {
                    string[] emp = empresas.Split(',');
                    foreach (DataGridViewRow row in dgvEmpresas.Rows)
                    {
                        for (int i = 0; i < emp.Length; i++)
                        {
                            if (row.Cells[0].Value.ToString() == emp[i].ToString())
                            {
                                row.Cells[2].Value = true;
                                dgvEmpresas.EndEdit();
                            }
                        }
                    }
                }
            }
        }

        private void CargaCombos()
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ph = new Perfil.Core.PerfilesHelper();
            ph.Command = cmd;

            List<Perfil.Core.Perfiles> lstPerfiles = new List<Perfil.Core.Perfiles>();

            try
            {
                cnx.Open();
                lstPerfiles = ph.obtenerPerfiles();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener lista de perfiles.\r\n \r\n" + error.Message, "Error");
            }

            cmbPerfil.DataSource = lstPerfiles.ToList();
            cmbPerfil.DisplayMember = "nombre";
            cmbPerfil.ValueMember = "idperfil";
        }

        private void CargaEmpresas() 
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;

            List<Empresas.Core.Empresas> lstEmpresas = new List<Empresas.Core.Empresas>();

            try
            {
                cnx.Open();
                lstEmpresas = eh.obtenerEmpresas();
                cnx.Close();
            }
            catch (Exception)
            {
                return;               
            }

            var empresas = from emp in lstEmpresas
                           select new
                           {
                               emp.idempresa,
                               emp.nombre
                           };

            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("nombre", typeof(String));
            dt.Columns.Add("seleccion", typeof(Boolean));

            foreach (var b in empresas)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = b.idempresa;
                dtFila["nombre"] = b.nombre;
                dtFila["seleccion"] = false;
                dt.Rows.Add(dtFila);
            }

            dgvEmpresas.Columns["id"].DataPropertyName = "id";
            dgvEmpresas.Columns["nombre"].DataPropertyName = "nombre";
            dgvEmpresas.Columns["nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvEmpresas.Columns["seleccion"].DataPropertyName = "seleccion";

            ck = new CheckBox();
            Rectangle rect = dgvEmpresas.GetCellDisplayRectangle(2, -1, true);
            ck.Size = new Size(18, 18);
            ck.Location = rect.Location;
            ck.CheckedChanged += ck_CheckedChanged;
            dgvEmpresas.Controls.Add(ck);
            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEmpresas.Columns["seleccion"].HeaderCell.Style = estilo;

            dgvEmpresas.DataSource = dt;
        }

        void ck_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEmpresas.Rows)
                if (ck.Checked)
                {
                    row.Cells[2].Value = true;
                }
                else
                    row.Cells[2].Value = false;
            dgvEmpresas.EndEdit();
        }

        private void dgvEmpresas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {   
                bool value = (bool)dgvEmpresas.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                if (value)
                    dgvEmpresas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                else
                    dgvEmpresas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            }
            dgvEmpresas.EndEdit();
        }
    }
}
