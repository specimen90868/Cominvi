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
    public partial class frmPeriodos : Form
    {
        public frmPeriodos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Periodos.Core.PeriodosHelper ph;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idperiodo;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoPeriodo(int edicion);
        public event delOnNuevoPeriodo OnNuevoPeriodo;
        #endregion

        private void frmPeriodos_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                ph = new Periodos.Core.PeriodosHelper();
                ph.Command = cmd;

                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                p.idperiodo = _idperiodo;

                List<Periodos.Core.Periodos> lstPeriodo;

                try
                {
                    cnx.Open();
                    lstPeriodo = ph.obtenerPeriodo(p);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstPeriodo.Count; i++)
                    {
                        cmbPago.SelectedText = lstPeriodo[i].pago;
                        txtDias.Text = lstPeriodo[i].dias.ToString();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Periodo";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                }
                else
                    toolTitulo.Text = "Edición Periodo";
            }

            cmbPago.SelectedIndex = 0;
        }

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            guardar(0);
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.pago = cmbPago.SelectedText;
            periodo.dias = cmbPago.SelectedText.Equals("SEMANAL") ? 7 : 15;
            periodo.estatus = 1;
            periodo.idempresa = GLOBALES.IDEMPRESA;

            int existe = 0;
            try
            {
                cnx.Open();
                existe = (int)ph.obtenerPeriodo(GLOBALES.IDEMPRESA, periodo.dias);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener si existe el periodo.\r\n\r\n" + error.Message, "Error");
                cnx.Dispose();
            }

            if (existe != 0)
            {
                MessageBox.Show("La empresa ya cuenta con el periodo elegido.", "Información");
                return;
            }

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ph.insertaPeriodo(periodo);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el periodo. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        periodo.idperiodo = _idperiodo;
                        cnx.Open();
                        ph.actualizaPeriodo(periodo);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el periodo. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoPeriodo != null)
                        OnNuevoPeriodo(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void cmbPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPago.SelectedIndex.Equals(1))
            {
                txtDias.Text = "15";
            }
            else
            {
                txtDias.Text = "7";
            }
        }
    }
}
