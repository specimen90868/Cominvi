﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Nominas
{
    public partial class frmSeleccionarEmpresa : Form
    {
        public frmSeleccionarEmpresa()
        {
            InitializeComponent();
        }

        #region DELEGADOS
        public delegate void delOnAbrirEmpresa();
        public event delOnAbrirEmpresa OnAbrirEmpresa;
        #endregion

        #region VARIBALES GLOBALES
        List<Empresas.Core.Empresas> lstEmpresa;
        #endregion
        

        private void frmSeleccionarEmpresa_Load(object sender, EventArgs e)
        {
            cargaGridEmpresa();
        }

        private void cargaGridEmpresa()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            cnx.ConnectionString = cdn;
            cmd.Connection = cnx;

            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;

            try 
            {
                cnx.Open();

                lstEmpresa = eh.InicioEmpresa();

                cnx.Close();
                cnx.Dispose();

                var e = from em in lstEmpresa
                        select new
                        {
                            IdEmpresa = em.idempresa,
                            Nombre = em.nombre,
                            Registro = em.registro + em.digitoverificador
                        };
                dgvEmpresas.DataSource = e.ToList();
                dgvEmpresas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //dgvEmpresas.Columns[1].Width = 200;
                dgvEmpresas.RowHeadersVisible = false;

                dgvEmpresas.Columns[0].Visible = false;
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void abrirEmpresa()
        {
            int fila = dgvEmpresas.CurrentCell.RowIndex;
            GLOBALES.IDEMPRESA = int.Parse(dgvEmpresas.Rows[fila].Cells[0].Value.ToString());
            GLOBALES.NOMBREEMPRESA = dgvEmpresas.Rows[fila].Cells[1].Value.ToString();

            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            cnx.ConnectionString = cdn;
            cmd.Connection = cnx;

            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;

            Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
            empresa.idempresa = GLOBALES.IDEMPRESA;
            try
            {
                cnx.Open();

                GLOBALES.DIASPERIODO = int.Parse(eh.obtenerDiasEmpresa(empresa).ToString());

                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            if (OnAbrirEmpresa != null)
                OnAbrirEmpresa();
            
            this.Dispose();
        }

        private void dgvEmpresas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            abrirEmpresa();
        }

        private void toolAbrir_Click(object sender, EventArgs e)
        {
            abrirEmpresa();
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmEmpresas em = new frmEmpresas();
            em.OnNuevaEmpresa += em_OnNuevaEmpresa;
            em.ShowDialog();
        }

        void em_OnNuevaEmpresa(int edicion)
        {
            if(edicion == GLOBALES.NUEVO)
                cargaGridEmpresa();
        }
    }
}
