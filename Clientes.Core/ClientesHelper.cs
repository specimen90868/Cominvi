using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Clientes.Core
{
    public class ClientesHelper : Data.Obj.DataObj
    {
        public List<Clientes> obtenerClientes(Clientes c)
        {
            DataTable dtClientes = new DataTable();
            List<Clientes> lstClientes = new List<Clientes>();
            Command.CommandText = "select idcliente, nombre, rfc from clientes where estatus = 1 and plaza = @idplaza";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idplaza", c.plaza);
            dtClientes = SelectData(Command);
            for (int i = 0; i < dtClientes.Rows.Count; i++)
            {
                Clientes cliente = new Clientes();
                cliente.idcliente = int.Parse(dtClientes.Rows[i]["idcliente"].ToString());
                cliente.nombre = dtClientes.Rows[i]["nombre"].ToString();
                cliente.rfc = dtClientes.Rows[i]["rfc"].ToString();
                lstClientes.Add(cliente);
            }
            return lstClientes;
        }

        public DataTable dtObtenerClientes(Clientes c)
        {
            DataTable dtClientes = new DataTable();
            Command.CommandText = "select idcliente, nombre, rfc from clientes where estatus = 1 and plaza = @idplaza";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idplaza", c.plaza);
            dtClientes = SelectData(Command);
            return dtClientes;
        }

        public List<Clientes> obtenerCliente(Clientes c)
        {
            DataTable dtClientes = new DataTable();
            List<Clientes> lstClientes = new List<Clientes>();
            Command.CommandText = "select idcliente, nombre, rfc, estatus, plaza from clientes where idcliente = @idcliente";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idcliente", c.idcliente);
            dtClientes = SelectData(Command);
            for (int i = 0; i < dtClientes.Rows.Count; i++)
            {
                Clientes cliente = new Clientes();
                cliente.idcliente = int.Parse(dtClientes.Rows[i]["idcliente"].ToString());
                cliente.nombre = dtClientes.Rows[i]["nombre"].ToString();
                cliente.rfc = dtClientes.Rows[i]["rfc"].ToString();
                cliente.estatus = int.Parse(dtClientes.Rows[i]["estatus"].ToString());
                cliente.plaza = int.Parse(dtClientes.Rows[i]["plaza"].ToString());
                lstClientes.Add(cliente);
            }
            return lstClientes;
        }

        public object obtenerIdCliente(Clientes c)
        {
            Command.CommandText = "select idcliente from clientes where rfc = @rfc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("rfc",c.rfc);
            object id = Select(Command);
            return id;
        }

        public int insertaCliente(Clientes c)
        {
            Command.CommandText = "insert into clientes (nombre, rfc, estatus, plaza) values (@nombre, @rfc, @estatus, @plaza)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombre",c.nombre);
            Command.Parameters.AddWithValue("rfc", c.rfc);
            Command.Parameters.AddWithValue("estatus", c.estatus);
            Command.Parameters.AddWithValue("plaza", c.plaza);
            return Command.ExecuteNonQuery();
        }

        public int actualizaCliente(Clientes c)
        {
            Command.CommandText = "update clientes set nombre = @nombre, rfc = @rfc, estatus = @estatus, plaza = @plaza where idcliente = @idcliente";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idcliente",c.idcliente);
            Command.Parameters.AddWithValue("nombre", c.nombre);
            Command.Parameters.AddWithValue("rfc", c.rfc);
            Command.Parameters.AddWithValue("estatus", c.estatus);
            Command.Parameters.AddWithValue("plaza", c.plaza);
            return Command.ExecuteNonQuery();
        }

        public int bajaCliente(Clientes c)
        {
            Command.CommandText = "update clientes set estatus = @estatus where idcliente = @idcliente";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idcliente", c.idcliente);
            Command.Parameters.AddWithValue("estatus", c.estatus);
            return Command.ExecuteNonQuery();
        }

    }
}
