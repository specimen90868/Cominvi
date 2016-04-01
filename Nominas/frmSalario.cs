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
    public partial class frmSalario : Form
    {
        public frmSalario()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Salario.Core.SalariosHelper sh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idsalario;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoSalario(int edicion);
        public event delOnNuevoSalario OnNuevoSalario;
        #endregion

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
            sh = new Salario.Core.SalariosHelper();
            sh.Command = cmd;

            Salario.Core.Salarios salario = new Salario.Core.Salarios();
            salario.periodo = dtpPeriodo.Value;
            salario.valor = decimal.Parse(txtValor.Text.Trim());
            salario.zona = cmbZona.Text;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        sh.insertaSalario(salario);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el salario. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        salario.idsalario = _idsalario;
                        cnx.Open();
                        sh.actualizaSalario(salario);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el salario. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    GLOBALES.REFRESCAR(this, typeof(DateTimePicker));
                    GLOBALES.REFRESCAR(this, typeof(ComboBox));
                    //limpiar(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoSalario != null)
                        OnNuevoSalario(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void frmSalario_Load(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                sh = new Salario.Core.SalariosHelper();
                sh.Command = cmd;

                Salario.Core.Salarios salario = new Salario.Core.Salarios();
                salario.idsalario = _idsalario;

                List<Salario.Core.Salarios> lstSalario;

                try
                {
                    cnx.Open();
                    lstSalario = sh.obtenerSalario(salario);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstSalario.Count; i++)
                    {
                        dtpPeriodo.Value = lstSalario[i].periodo;
                        txtValor.Text = lstSalario[i].valor.ToString();
                        cmbZona.SelectedIndex = (lstSalario[i].zona == "A") ? 0 : (lstSalario[i].zona == "B") ? 1 : 2;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta salario mínimo";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(ComboBox));
                    GLOBALES.INHABILITAR(this, typeof(DateTimePicker));
                }
                else
                    toolTitulo.Text = "Edición salario mínimo";
            }
        }
    }
}
