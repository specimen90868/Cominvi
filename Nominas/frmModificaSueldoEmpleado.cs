﻿using System.Data.SqlClient;
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
    public partial class frmModificaSueldoEmpleado : Form
    {
        public frmModificaSueldoEmpleado()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        int idempleado = 0;
        #endregion

        private void frmModificaSueldoEmpleado_Load(object sender, EventArgs e)
        {
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            if (idempleado == 0)
            {
                MessageBox.Show("Debe especificar el empleado.", "Información");
                return;
            }
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados em = new Empleados.Core.Empleados();
            em.idtrabajador = idempleado;
            em.sueldo = double.Parse(txtSueldo.Text);
            em.sd = double.Parse(txtSD.Text);
            em.sdi = double.Parse(txtSDI.Text);

            try
            {
                cnx.Open();
                eh.actualizaSueldo(em);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b._catalogo = GLOBALES.EMPLEADOS;
            b.OnBuscar += b_OnBuscar;
            b.MdiParent = this.MdiParent;
            b.Show();
        }

        void b_OnBuscar(int id, string nombre)
        {
            idempleado = id;
            lblEmpleado.Text = nombre;
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (idempleado == 0)
            {
                MessageBox.Show("Debe especificar el empleado.", "Información");
                return;
            }

            if (txtSueldo.Text.Length != 0)
            {
                int DiasDePago = 0;
                double FactorDePago = 0;
                int Periodo = 0;
                int AntiguedadMod = 0;
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                Factores.Core.FactoresHelper fh = new Factores.Core.FactoresHelper();
                Factores.Core.Factores f = new Factores.Core.Factores();
                Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                Empleados.Core.Empleados em = new Empleados.Core.Empleados();

                ph.Command = cmd;
                fh.Command = cmd;
                eh.Command = cmd;

                try
                {
                    em.idtrabajador = idempleado;
                    List<Empleados.Core.Empleados> lstEmpleado = eh.obtenerEmpleado(em);
                    for (int i = 0; i < lstEmpleado.Count; i++)
                    {
                        Periodo = lstEmpleado[i].idperiodo;
                        AntiguedadMod = lstEmpleado[i].antiguedadmod;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    this.Dispose();
                }
                
                p.idperiodo = Periodo;
                f.anio = AntiguedadMod;

                try
                {
                    cnx.Open();
                    DiasDePago = (int)ph.DiasDePago(p);
                    FactorDePago = double.Parse(fh.FactorDePago(f).ToString());
                    cnx.Close();
                    cnx.Dispose();

                    txtSD.Text = (double.Parse(txtSueldo.Text) / DiasDePago).ToString("F6");
                    txtSDI.Text = (double.Parse(txtSD.Text) * FactorDePago).ToString("F6");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    this.Dispose();
                }
            }
        }
    }
}
