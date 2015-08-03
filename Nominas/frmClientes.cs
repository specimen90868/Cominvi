using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Clientes.Core.ClientesHelper ch;
        Direccion.Core.DireccionesHelper dh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idcliente;
        public int _iddireccion;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoCliente(int edicion);
        public event delOnNuevoCliente OnNuevoCliente;
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

            control = GLOBALES.VALIDAR(this, typeof(MaskedTextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            int idcliente;

            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;
            ch = new Clientes.Core.ClientesHelper();
            ch.Command = cmd;

            Clientes.Core.Clientes cli = new Clientes.Core.Clientes();
            cli.nombre = txtNombre.Text;
            cli.rfc = txtRfc.Text;
            cli.estatus = 1;
            cli.plaza = GLOBALES.IDPLAZA;

            dh = new Direccion.Core.DireccionesHelper();
            dh.Command = cmd;

            Direccion.Core.Direcciones d = new Direccion.Core.Direcciones();
            d.calle = txtCalle.Text;
            d.exterior = txtExterior.Text;
            d.interior = txtInterior.Text;
            d.colonia = txtColonia.Text;
            d.ciudad = txtMunicipio.Text;
            d.estado = txtEstado.Text;
            d.pais = txtPais.Text;
            d.cp = txtCP.Text;
            ///TIPO DIRECCION
            ///0 - Dirección fiscal
            ///1 - Dirección sucursal
            ///2 - Dirección personal
            d.tipodireccion = 0;
            ///TIPO PERSONA
            ///0 - Empresa
            ///1 - Cliente
            ///2 - Empleado
            d.tipopersona = 1;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ch.insertaCliente(cli);
                        idcliente = (int)ch.obtenerIdCliente(cli);
                        d.idpersona = idcliente;
                        dh.insertaDireccion(d);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el cliente. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        cli.idcliente = _idcliente;
                        d.iddireccion = _iddireccion;
                        d.idpersona = _idcliente;
                        cnx.Open();
                        ch.actualizaCliente(cli);
                        dh.actualizaDireccion(d);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el cliente. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this,typeof(TextBox));
                    GLOBALES.LIMPIAR(this, typeof(MaskedTextBox));
                    //limpiar(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoCliente != null)
                        OnNuevoCliente(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new MySqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                ch = new Clientes.Core.ClientesHelper();
                ch.Command = cmd;

                dh = new Direccion.Core.DireccionesHelper();
                dh.Command = cmd;

                Clientes.Core.Clientes cli = new Clientes.Core.Clientes();
                cli.idcliente = _idcliente;

                Direccion.Core.Direcciones d = new Direccion.Core.Direcciones();
                d.idpersona = _idcliente;
                d.tipopersona = 1; ///TIPO PERSONA 1 - Clientes
                List<Clientes.Core.Clientes> lstCliente;
                List<Direccion.Core.Direcciones> lstDireccion;

                try
                {
                    cnx.Open();
                    lstCliente = ch.obtenerCliente(cli);
                    lstDireccion = dh.obtenerDireccion(d);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstCliente.Count; i++)
                    {
                        txtNombre.Text = lstCliente[i].nombre;
                        txtRfc.Text = lstCliente[i].rfc;
                    }

                    for (int i = 0; i < lstDireccion.Count; i++)
                    {
                        _iddireccion = lstDireccion[i].iddireccion;
                        txtCalle.Text = lstDireccion[i].calle;
                        txtExterior.Text = lstDireccion[i].exterior;
                        txtInterior.Text = lstDireccion[i].interior;
                        txtColonia.Text = lstDireccion[i].colonia;
                        txtCP.Text = lstDireccion[i].cp;
                        txtMunicipio.Text = lstDireccion[i].ciudad;
                        txtEstado.Text = lstDireccion[i].estado;
                        txtPais.Text = lstDireccion[i].pais;
                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Cliente";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(MaskedTextBox));
                }
                else
                    toolTitulo.Text = "Edición Cliente";
            }
        }

        private void txtRfc_Leave(object sender, EventArgs e)
        {
            string lsPatron = @"^[A-ZÑ&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9][A-Z,0-9][0-9A]$";
            Regex loRE = new Regex(lsPatron);
            if (!loRE.IsMatch(txtRfc.Text))
                MessageBox.Show("El RFC no es valido. Verifique", "Error");
        }
    }
}
